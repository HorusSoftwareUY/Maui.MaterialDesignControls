# Mono vs CoreCLR en .NET MAUI 11 (Android) — Notas de investigación

Notas de referencia de una sesión de research + debugging real, comparando el runtime Mono contra CoreCLR en Android para .NET MAUI 11 Preview. Pensadas como insumo para armar un post más adelante.

---

## 1. Contexto: el cambio de runtime en .NET 11

A partir de **.NET 11 Preview 4**, CoreCLR pasa a ser el runtime **por defecto** para .NET MAUI en Android, iOS, Mac Catalyst y tvOS, reemplazando a Mono como default después de más de 15 años de linaje Xamarin. Blazor WebAssembly no se ve afectado (sigue en Mono).

Motivos declarados por Microsoft:
- **Unificación de runtime**: mobile pasa a compartir runtime con server/desktop/cloud (mismo JIT, mismo GC, mismas herramientas de diagnóstico).
- **Mejores fundamentos de performance**: tiered JIT, ReadyToRun (R2R), Profile-Guided Optimization (PGO) llegan a mobile.
- **Camino a NativeAOT** en todas las plataformas, con toolchain más unificado.

Se puede volver a Mono explícitamente con `<UseMonoRuntime>true</UseMonoRuntime>` durante todo el ciclo de .NET 11, incluido servicing.

Microsoft reconoce regresiones de startup/tamaño en apps grandes en Android y pide medir cada caso puntual, no asumir mejora universal (`dotnet/android#10588`, `dotnet/android#10914`).

**Referencia principal:** [.NET MAUI Moves to CoreCLR in .NET 11 — .NET Blog](https://devblogs.microsoft.com/dotnet/dotnet-maui-moves-to-coreclr-in-dotnet-11/)

---

## 2. Conceptos: cómo compila cada runtime

| Concepto | Qué es | Runtime |
|---|---|---|
| **JIT** | Compila MSIL a nativo en runtime, método por método, la primera vez que se llama | Ambos (CoreCLR siempre; Mono en Debug/Android) |
| **Mono AOT** | Precompila MSIL a nativo en build-time con el compilador de Mono (`mono-aot-cross`). Genera `.so` separados. Puede convivir con el intérprete de Mono como fallback (AOT parcial) o exigir todo precompilado (**Full AOT**, default en iOS/Mac Catalyst) | Mono |
| **ReadyToRun (R2R)** | Equivalente de CoreCLR: precompila a nativo en build-time con `crossgen2`, pero el nativo queda *adentro* del mismo `.dll` junto con el MSIL original (assembly "mixto"). Si hace falta, CoreCLR puede recompilar desde el MSIL | CoreCLR |
| **NativeAOT** | Runtime mínimo, separado tanto de Mono como de CoreCLR. Compila TODO (app + deps + runtime) a un único binario nativo. Sin JIT ni intérprete. Exige trimming completo | Ninguno de los dos (los reemplaza) |
| **Mono interpreter** | Interpreta MSIL sin generar código dinámicamente. Habilita Hot Reload en Mono. Default en Debug de iOS/Mac Catalyst | Mono |
| **PGO / perfiles MIBC** | Perfiles de "qué métodos son calientes en el arranque" que guían el R2R parcial en CoreCLR | CoreCLR |

Puntos clave de compatibilidad:
- **Mono AOT y R2R no pueden mezclarse en el mismo ensamblado**: R2R apaga el flag `ILOnly` del PE (queda un binario mixto IL+nativo), y el toolchain de Mono (basado en Mono.Cecil, que solo sabe manipular IL puro) no puede post-procesar esos ensamblados. Por eso en un mismo proyecto multi-runtime hay que condicionar `PublishReadyToRun` solo para la rama CoreCLR.
- **`RunAOTCompilation`** (Mono AOT) y **`PublishReadyToRun`** (CoreCLR R2R) son propiedades MSBuild completamente distintas — no hay superposición conceptual real más allá de "ambas precompilan".

**Referencia:** [Runtimes and compilation in .NET MAUI — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/maui/deployment/runtimes-compilation?view=net-maui-10.0)

---

## 3. Setup del proyecto para comparar ambos runtimes

Estructura usada (csproj con switch `UseMono`):

```xml
<UseMono Condition="'$(UseMono)' == ''">true</UseMono>

<!-- Separar obj/bin por runtime para que no colisionen cachés de iconos/assets -->
<_RuntimeSlug Condition="'$(UseMono)' == 'true'">mono</_RuntimeSlug>
<_RuntimeSlug Condition="'$(UseMono)' != 'true'">coreclr</_RuntimeSlug>
<IntermediateOutputPath>obj\$(_RuntimeSlug)\$(Configuration)\$(TargetFramework)\</IntermediateOutputPath>
<OutputPath>bin\$(_RuntimeSlug)\$(Configuration)\$(TargetFramework)\</OutputPath>

<UseMonoRuntime>$(UseMono)</UseMonoRuntime>
```

⚠️ **Ojo con proyectos referenciados (`ProjectReference`)**: si la librería no tiene la misma separación de `obj`/`bin` por runtime, sus artefactos intermedios pueden quedar mezclados entre una build Mono y una build CoreCLR, generando falsos positivos de bugs de caché.

**APK/AAB firmado final**: siempre sale de `bin\...\publish\`, nunca de `obj` (carpeta de trabajo intermedio, no confiable como artefacto final). Usar `dotnet publish`, no solo `dotnet build`, para el artefacto que se sube a la store.

---

## 4. Bitácora de bugs encontrados (orden cronológico real)

### Bug 1 — `AndroidLinkMode=None` + `RunAOTCompilation=true` (Mono) → `SIGABRT`

**Síntoma:**
```
Assertion at .../class-init.c:2691, condition `klass->instance_size == instance_size' not met
```

**Causa raíz:** con el linker apagado (`None`), el compilador AOT de Mono lee los ensamblados de entrada desde su ubicación original (packs/NuGet) en vez de `obj/.../assets` (donde queda la versión final, post-procesada). Si algún paso posterior reescribe el ensamblado (p. ej. marshal methods), el AOT queda compilado contra una versión vieja mientras se empaqueta la nueva → mismatch de layout de clase → abort.

**Nota importante que se confirmó después (ver Bug 4):** esto no es un bug aislado de marshal methods — es la manifestación de una regla arquitectónica más profunda (ver XA1030 más abajo). `RunAOTCompilation` **nunca** funciona con el linker apagado, por diseño.

**Referencia:** [Builds with AOT enabled and linking disabled copy assemblies directly from packs/nugets — dotnet/android#7389](https://github.com/dotnet/android/issues/7389)

---

### Bug 2 — `AndroidLinkMode=SdkOnly` (Mono) → `UnsatisfiedLinkError`

Al cambiar a `SdkOnly` (el default recomendado) para sacar a Mono del Bug 1, apareció un crash distinto:

```
java.lang.UnsatisfiedLinkError: No implementation found for void crc6488302ad6e9e4df1a.MauiApplication.n_onCreate()
  - is the library loaded, e.g. System.loadLibrary?
```

**Diagnóstico:** falla el registro JNI de "marshal methods" — el mecanismo que .NET for Android usa para registrar métodos nativos en build-time en vez de por reflexión. El hash `crc64...` es el mismo en cualquier proyecto MAUI (deriva del nombre de la clase base `MauiApplication`), así que no es específico de este proyecto.

**Se descartó como problema de caché**: se hizo clean completo (bin/obj en Sample y en la librería referenciada) + `dotnet nuget locals all --clear` + reinstalación limpia del APK, y el error persistió idéntico.

**Bug confirmado y verificado por el equipo de Microsoft** (etiquetas `s/verified`, `t/bug`, sin fix, milestone Backlog): reproduce incluso en una app MAUI en blanco, y **incluso con `RunAOTCompilation=false` y `AndroidEnableMarshalMethods=false`** — es decir, el bug de `SdkOnly` no depende de AOT ni de marshal methods, es del linker en sí en esta preview.

**Workaround oficial documentado en el issue:** volver a `AndroidLinkMode=None`.

**Referencias:**
- [[.NET 10] AndroidLinkMode=SdkOnly crashes on startup with "only_unmanaged_callers_only" assertion (icall.c:6261) — dotnet/maui#33032](https://github.com/dotnet/maui/issues/33032)
- [Baidu Push SDK: No implementation found for void crc6488302ad6e9e4df1a.MauiApplication.n_onCreate() — dotnet/maui#16142](https://github.com/dotnet/maui/issues/16142)
- [.NET 7 UnsatisfiedLinkError to native OnCreate() coming from Java Binding — dotnet/android#8675](https://github.com/dotnet/android/issues/8675)

---

### Bug 3 — `None` + AOT + `AndroidEnableMarshalMethods=false` → nuevo crash (GUID mismatch)

Intento de "arreglar" el Bug 1 apagando específicamente marshal methods (la causa que señalaba el issue #7389), mientras se mantenía `AndroidLinkMode=None`.

**Resultado:** otro crash distinto, con información mucho más explícita:

```
Assertion at .../aot-runtime.c:3869, condition `is_ok (error)' not met, function:decode_patch,
module 'Microsoft.Maui.Essentials.dll.so' is unusable
(GUID of dependent assembly Xamarin.AndroidX.Core doesn't match
 (expected 'D2C85411-...', got '2F1F5C3A-...'))
```

**Se investigó si era conflicto de versiones de NuGet** con `dotnet list package --include-transitive` — no se encontró ningún conflicto para `Xamarin.AndroidX.Core` específicamente (resuelve limpio a una sola versión, `1.17.0.2`, sin warning `NU1608` para ese paquete puntual). El mismatch de GUID persistió incluso después de un clean nuclear completo (bin/obj + NuGet cache + reinstalación) — mismas GUIDs, exactas, en cada corrida.

**Conclusión:** esto confirmó que el problema no era caché ni conflicto de dependencias — era la manifestación número dos del mismo problema estructural del Bug 1 (AOT sin trimming = inconsistencia garantizada tarde o temprano, sea cual sea la causa puntual del mismatch).

**Casos similares documentados** (mismo patrón, distinto ensamblado):
- [.net Maui Deploy to Android in release produces "module is unusable (GUID of dependent assembly Microsoft.EntityFrameworkCore.Sqlite doesn't match" — Microsoft Q&A](https://learn.microsoft.com/en-gb/answers/questions/1025205/net-maui-deploy-to-android-in-release-produces-mod)
- [[MAUI:Android:Release] Build crash on startup with GUID mismatch errors — dotnet/android#9401](https://github.com/dotnet/android/issues/9401)

---

### Bug 4 (la pieza que cerró todo) — Regla oficial: AOT requiere trimming, por diseño

Se encontró la documentación oficial del error **XA1030**, que Microsoft's SDK debería emitir (como error de build, no crash de runtime) exactamente en el escenario del Bug 1 y Bug 3:

> "The 'RunAOTCompilation' MSBuild property is only supported when trimming is enabled. Edit the project file in a text editor to set 'PublishTrimmed' to 'true' for this build configuration."

Es decir: **`RunAOTCompilation=true` + `AndroidLinkMode=None` (o `PublishTrimmed=false`) nunca es una combinación soportada, por diseño arquitectónico** — no es un bug de esta preview puntual, es una regla general de .NET for Android desde hace años. El AOT necesita que el linker haya corrido antes para dejar los ensamblados en un estado final consistente. Sin eso, el AOT y el empaquetado final terminan viendo versiones distintas de algo, tarde o temprano (a veces se manifiesta como mismatch de `instance_size`, a veces como mismatch de GUID — según qué ensamblado puntual se vea afectado primero).

Lo llamativo de esta preview: el chequeo XA1030 **no está frenando el build** como debería (dejó pasar la combinación inválida hasta un crash en runtime en vez de fallar el build con un mensaje claro) — posible bug puntual de esta build de .NET 11 preview.

**Referencia:** [.NET for Android error XA1030 — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/android/messages/xa1030)

**Casos relacionados históricos (mismo mecanismo, Xamarin.Android):**
- [Apps built with `-c Release -p:PublishTrimmed=false` crash at runtime, AOT + No trimmer — xamarin/xamarin-android#7178](https://github.com/xamarin/xamarin-android/issues/7178)
- [[One .NET] error with XA1030 for AOT and PublishTrimmed=false — xamarin/xamarin-android PR #7406](https://github.com/xamarin/xamarin-android/pull/7406)

---

## 5. Configuración final que funciona (validada en runtime, no solo en build)

Dado que `SdkOnly` está roto para Mono en esta preview (Bug 2) y `None` es arquitectónicamente incompatible con AOT (Bug 4), la única combinación de linking que deja trimming activo **sin** pisar el bug de `SdkOnly` es **`Full`**:

```xml
<PropertyGroup Condition="'$(Configuration)' == 'Release' and $(TargetFramework.Contains('-android'))">
    <!-- Mono: Full trimming + AOT -> funciona -->
    <AndroidLinkMode Condition="'$(UseMono)' == 'true'">Full</AndroidLinkMode>
    <RunAOTCompilation Condition="'$(UseMono)' == 'true'">true</RunAOTCompilation>

    <!-- CoreCLR: SdkOnly + R2R -> funciona sin problemas, es la config de producción estándar -->
    <AndroidLinkMode Condition="'$(UseMono)' != 'true'">SdkOnly</AndroidLinkMode>
    <PublishReadyToRun Condition="'$(UseMono)' != 'true'">true</PublishReadyToRun>

    <EmbedAssembliesIntoApk>true</EmbedAssembliesIntoApk>
    <AndroidUseAssemblyStore>false</AndroidUseAssemblyStore>
</PropertyGroup>
```

**Resultado:** ambos runtimes arrancan y corren con precompilación + trimming activos. Asimetría pendiente de documentar: Mono queda en `Full` (trimea también código propio y NuGets) mientras CoreCLR queda en `SdkOnly` (solo BCL/SDK) — no es la misma agresividad de trimming de los dos lados, así que el tamaño final de la app no es 100% comparable entre ambos sin correr una pasada extra con CoreCLR también en `Full`.

---

## 6. Aprendizajes clave para el post

- **`AOT + sin linker` no es "más simple", es inválido.** Aplica tanto a Mono (regla XA1030) como conceptualmente a CoreCLR (R2R sin trimming compila mucho más código del necesario, aunque ahí no rompe, solo es ineficiente).
- **`SdkOnly` es el default recomendado y el que usa la inmensa mayoría de apps publicadas — pero en .NET 10/11 preview tiene una regresión activa y verificada específica de Mono.** No representa cómo se comporta Mono en versiones estables (.NET 8/9/10 GA).
- **El mismo síntoma raíz (AOT sin trimming) se manifiesta con distintos crashes** según qué ensamblado se vea afectado primero: a veces `instance_size` mismatch, a veces `UnsatisfiedLinkError` en JNI, a veces mismatch de GUID de un ensamblado dependiente. Todos son variantes del mismo problema de fondo.
- **`Full` trimming funciona para Mono en esta preview cuando `SdkOnly` no** — dato interesante en sí mismo, sugiere que el bug de `SdkOnly` (#33032) está en una ruta de código específica de ese modo de linking, no en el linker en general.
- **CoreCLR en Android, en esta misma build de .NET 11 preview, corre su configuración de producción estándar (`SdkOnly` + R2R) sin ningún problema** — mientras Mono estuvo bloqueado de la suya por dos bugs superpuestos. Esto en sí es un dato de comparación legítimo entre ambos runtimes en el estado actual de la preview.
- **Descartar "problema de caché" requiere ser riguroso**: clean de bin/obj en *todos* los proyectos involucrados (incluyendo `ProjectReference`s), `dotnet nuget locals all --clear`, y desinstalación del APK del dispositivo antes de reinstalar. Si el error persiste idéntico (mismos GUIDs, mismo mensaje) después de todo eso, es un bug real, no un artefacto de build.
- Para publicar a las stores, el artefacto final siempre sale de `bin\...\publish\` (nunca de `obj`), generado con `dotnet publish`, no `dotnet build`.

---

## 7. Referencias completas

**Anuncio y documentación oficial de la migración:**
- [.NET MAUI Moves to CoreCLR in .NET 11 — .NET Blog](https://devblogs.microsoft.com/dotnet/dotnet-maui-moves-to-coreclr-in-dotnet-11/)
- [Runtimes and compilation in .NET MAUI — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/maui/deployment/runtimes-compilation?view=net-maui-10.0)
- [What's new in .NET MAUI for .NET 11 — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/maui/whats-new/dotnet-11?view=net-maui-10.0)
- [.NET MAUI in .NET 11 Preview 4 - Release Notes (dotnet/core)](https://github.com/dotnet/core/blob/main/release-notes/11.0/preview/preview4/dotnetmaui.md)
- [.NET for Android error XA1030 — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/android/messages/xa1030)

**Bugs y issues usados para diagnosticar:**
- [dotnet/android#7389 — Builds with AOT enabled and linking disabled copy assemblies directly from packs/nugets](https://github.com/dotnet/android/issues/7389)
- [dotnet/android#11068 — [CoreCLR] Setting RunAOTCompilation in project file runs MonoAOTCompiler even with CoreCLR runtime](https://github.com/dotnet/android/issues/11068)
- [dotnet/android#10062 — [coreclr] Enable R2R builds with marshal methods generation](https://github.com/dotnet/android/issues/10062)
- [dotnet/maui#33032 — [.NET 10] AndroidLinkMode=SdkOnly crashes on startup with "only_unmanaged_callers_only" assertion](https://github.com/dotnet/maui/issues/33032)
- [dotnet/maui#16142 — Baidu Push SDK: No implementation found for void crc6488302ad6e9e4df1a.MauiApplication.n_onCreate()](https://github.com/dotnet/maui/issues/16142)
- [dotnet/android#8675 — .NET 7 UnsatisfiedLinkError to native OnCreate() coming from Java Binding](https://github.com/dotnet/android/issues/8675)
- [dotnet/android#8253 — Known samples for AndroidEnableMarshalMethods=true](https://github.com/dotnet/android/issues/8253)
- [dotnet/android#9401 — [MAUI:Android:Release] Build crash on startup with GUID mismatch errors](https://github.com/dotnet/android/issues/9401)
- [Microsoft Q&A — .net Maui Deploy to Android in release produces "module is unusable (GUID of dependent assembly ... doesn't match"](https://learn.microsoft.com/en-gb/answers/questions/1025205/net-maui-deploy-to-android-in-release-produces-mod)
- [xamarin/xamarin-android#7178 — Apps built with `-c Release -p:PublishTrimmed=false` crash at runtime, AOT + No trimmer](https://github.com/xamarin/xamarin-android/issues/7178)
- [xamarin/xamarin-android PR #7406 — error with XA1030 for AOT and PublishTrimmed=false](https://github.com/xamarin/xamarin-android/pull/7406)
- [dotnet/maui#16074 — Including specific android libraries via nuget causes AOT failures on RELEASE build](https://github.com/dotnet/maui/issues/16074)
- [dotnet/maui#9163 — [Android] AOT not working, app crashes after start](https://github.com/dotnet/maui/issues/9163)

---

*Nota: todo lo anterior corresponde al estado de .NET 11 Preview 4/5 (~julio 2026). Varios de estos bugs son específicos de este ciclo de preview y es esperable que se resuelvan antes del GA de noviembre — vale la pena volver a validar la configuración final contra una preview más nueva antes de publicar el post.*
