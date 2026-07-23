# Startup PGO en CoreCLR (Android) — Perfil MIBC custom para la sample app

Notas de una sesión real de trabajo (23-jul-2026) para generar e integrar un perfil **MIBC** propio de la app, medir su impacto en cold start y compararlo contra Mono AOT. Incluye los workarounds necesarios porque el flujo oficial (`maui profile startup --format mibc`) está **roto upstream** en este preview.

---

## 1. Resumen ejecutivo

Cold start medido con `adb shell am start -W` (TotalTime), 10 iteraciones por variante, en un **moto g54 5G** (arm64, físico), builds `Release` de la sample app:

| Variante | Mediana | Rango | Δ vs Mono |
|---|---|---|---|
| **Mono + AOT** (aotprofile default del SDK) | ~4.339 ms | 4.292–4.415 | baseline |
| **CoreCLR R2R** (mibc default del workload) | ~4.246 ms | 4.182–4.364 | −2% |
| **CoreCLR R2R + mibc custom** | **~2.893 ms** | 2.872–2.914 | **−33%** |

Conclusiones:
- CoreCLR "pelado" apenas empata a Mono+AOT en startup (consistente con las regresiones que Microsoft reconoce en `dotnet/android#10588`, `dotnet/android#10914`).
- El **mibc custom es lo que destraba la ventaja**: ~1.350 ms menos de cold start (~32% sobre CoreCLR default) y tiempos mucho más consistentes (desvío ~15 ms vs ~60 ms).
- El build R2R queda con **4 perfiles**: los 3 default del workload MAUI + el custom de la app (verificado en el log de crossgen2).

---

## 2. Qué es esto (recordatorio conceptual)

- **PGO estático con MIBC**: un archivo `.mibc` (Managed Intermediate Binary Code) lista los métodos calientes del arranque. En build Release, `crossgen2` (R2R) **precompila solo esos métodos** (*partial R2R*): el hot path no necesita JIT al arrancar y el tamaño del APK no explota.
- En Android con CoreCLR, el workload MAUI **ya incluye perfiles default** (`DotNet_Maui_Android.mibc`, `_SampleContent`, `_Blazor`). El custom es **aditivo**: cubre los hot paths de *tu* app y *tus* librerías (en este caso, MaterialDesignControls, CommunityToolkit, SkiaSharp, etc.).
- El mibc custom generado: **12.535 métodos**, ~176 KB comprimido.
- Equivalente en Mono: el **`*.aotprofile`** (profile-guided AOT). .NET Android aplica uno default con `RunAOTCompilation=true`; grabar uno custom es posible (ver §7).

---

## 3. Estado del tooling (por qué hubo que hacer workaround)

El flujo documentado es `maui profile startup --format mibc` (CLI de [dotnet/maui-labs](https://github.com/dotnet/maui-labs)), pero end-to-end está roto en este preview:

1. **Bug [dotnet/maui#36637](https://github.com/dotnet/maui/issues/36637)** (abierto al 23-jul-2026): la traza `.nettrace` capturada a través de `dotnet-dsrouter` (o con `DOTNET_EventPipeOutputPath` on-device si el proceso muere por señal) queda **truncada** y `dotnet-pgo create-mibc` falla con `Read past end of stream`. Mismo error con `dotnet-trace report/convert`. En nuestras pruebas el archivo termina con el cierre `06 01` cortado a un solo byte `06`.
2. Además la CLI cancelaba la colección al pedir el stop a `dotnet-trace` 9.0 (quedaba colgado >5s y moría con SIGINT/exit 2).
3. **`dotnet-pgo` no está publicado en NuGet**: la CLI lo descarga/compila en `~/.maui/dotnet-pgo` (en esta máquina ya existía, versión `10.0.10-dev`).

**Workaround aplicado** (verificado, 100% reproducible): captura **on-device directa a archivo** (sin dsrouter ni dotnet-trace) + **reparación del trailer** del nettrace + conversión con `dotnet-pgo` filtrando la capa JNI.

---

## 4. Procedimiento completo (reproducible)

### 4.1 Prerequisitos

- SDK .NET 11 preview 5 arm64 funcional. ⚠️ En esta máquina el `dotnet` del PATH (`/usr/local/share/dotnet/dotnet`) quedó **x86_64** pisado por una instalación x64 y no arranca (todos los `hostfxr` modernos son arm64). Se dejó un SDK funcional en `~/dotnet-arm64` con symlinks a los workloads/packs de la instalación principal. Reparar el apphost principal requiere sudo.
- Dispositivo Android **físico arm64** (la config CoreCLR del proyecto es arm64-only; emuladores x64 no sirven).
- `adb` accesible y `~/.maui/dotnet-pgo` presente (la CLI lo instala; si no, se construye desde dotnet/runtime).

### 4.2 Instrumentación (ya commiteable)

- `Microsoft.Maui.ProfilingHelper` referenciado en la sample (versión en `samples/Directory.Packages.props`).
- Marcador one-shot en `Pages/BaseContentPage.cs` → `OnNavigatedTo`: `MauiProfilingMarker.Complete()` despachado tras el primer render (mismo punto donde frena el timer de nav-timing). Es no-op fuera de sesiones de profiling.

### 4.3 Build de profiling (opt-in)

`Profiling/startup-profiling.env` + `Profiling/StartupProfiling.targets` (ambos nuevos) se activan con `-p:EnableStartupProfiling=true`:

- El `.env` configura el runtime para escribir la traza **en el propio dispositivo** y con los knobs de PGO dinámico de la config IBC "known-good" de dotnet-optimization (los mismos que usa `MauiProfilingHelperInjection.targets` de la CLI):
  ```
  DOTNET_EnableEventPipe=1
  DOTNET_EventPipeConfig=Microsoft-Windows-DotNETRuntime:0x1F000080018:5
  DOTNET_EventPipeOutputPath=/data/data/<pkg>/files/startup.nettrace
  DOTNET_TieredPGO=1, DOTNET_ReadyToRun=0, DOTNET_TC_QuickJitForLoops=1, ...
  ```
  (`ReadyToRun=0` fuerza JIT de todo durante la captura → la traza registra los métodos realmente calientes).
- El `.targets` parcha el `AndroidManifest.xml` intermedio con `android:debuggable=true` (mismo hack de la CLI) para poder extraer la traza con `adb run-as`. **Solo aplica al build de profiling, nunca al Release normal.**

```bash
dotnet build -c Release -f net11.0-android -p:EnableStartupProfiling=true -t:Install <sample.csproj>
```

### 4.4 Captura

```bash
PKG=com.horusstudio.maui.materialdesigncontrols.sample.coreclr
adb shell am force-stop $PKG
adb shell am start -n "$PKG/com.horusstudio.maui.materialdesigncontrols.sample.MainActivity"
sleep 25
adb shell run-as $PKG kill -TERM $(adb shell pidof $PKG)   # stop "amable"
adb exec-out run-as $PKG cat files/startup.nettrace > /tmp/startup-device.nettrace
```

La traza capturada cubrió **118.599 eventos / 21,1 s** (todo el startup incluido).

### 4.5 Reparación del trailer (workaround del bug #36637)

El archivo queda truncado en el último bloque (falta el byte final del marcador de cierre `06 01`). Herramienta de diagnóstico/reparación en `tools/tracecheck/` (usa `Microsoft.Diagnostics.Tracing.TraceEvent` 3.1.26; parsea bloque a bloque con los parsers internos vía reflection):

```bash
cp /tmp/startup-device.nettrace /tmp/startup-fix.nettrace
printf '\x01' >> /tmp/startup-fix.nettrace   # completa el marcador de cierre
# validar: dotnet tools/tracecheck/.../tracecheck.dll /tmp/startup-fix.nettrace
# -> OK: 118.599 events, last 21.091 ms
```

> Si en el futuro el trailer falta de otra forma, `tracecheck` reporta cuántos eventos parsea antes de fallar; el corte siempre está al final (problema de finalización, no de formato).

### 4.6 Generación del mibc (con filtro JNI — ¡importante!)

```bash
LINKED=samples/HorusStudio.Maui.MaterialDesignControls.Sample/obj/coreclr/Release/net11.0-android/net11.0-android/android-arm64/linked
~/.maui/dotnet-pgo create-mibc -t /tmp/startup-fix.nettrace \
  -o samples/.../Profiling/android-startup.mibc --compressed \
  -r "$LINKED/*.dll" \
  --exclude-methods "Java\.Interop|Java\.Lang|Mono\.Android|Android\.Runtime"
```

⚠️ **El mibc sin filtrar rompe la app**: con los métodos de la capa JNI precompilados, el arranque crashea con `TypeInitializationException` en `Java.Interop.ManagedPeer..cctor` (`IncompatibleClassChangeError`, nombre de método JNI corrupto — bug de crossgen2/cross-compilación de este preview al precompilar `RegisterNatives`). Excluyendo `Java.Interop|Java.Lang|Mono.Android|Android.Runtime` (918 métodos, 13.453 → 12.535) el crash desaparece y la mejora se mantiene. El regex se aplica sobre el nombre estilo PerfView (`Namespace.Type.Method(...)`), **sin** prefijo `[Module]`.

Inspección del mibc: `~/.maui/dotnet-pgo dump -i android-startup.mibc -o dump.txt`.

### 4.7 Integración en el build

En el `.csproj` de la sample (solo CoreCLR Release Android; escape para A/B con `-p:DisableStartupPgo=true`):

```xml
<ItemGroup Condition="'$(Configuration)' == 'Release' and $(TargetFramework.Contains('-android')) and '$(UseMono)' != 'true' and '$(DisableStartupPgo)' != 'true'">
  <_ReadyToRunPgoFiles Include="Profiling\android-startup.mibc" />
</ItemGroup>
```

Verificación (verbose): crossgen2 recibe `-m:` × 4 (3 default del workload + el custom).

---

## 5. Metodología de medición

```bash
for i in $(seq 1 10); do
  adb shell am force-stop $PKG; sleep 1.5
  adb shell am start -W -n "$PKG/...MainActivity" | grep ^TotalTime
done
```

- Variantes: `Mono+AOT` (`-p:UseMono=true`, paquete `.mono`), `CoreCLR sin custom` (`-p:DisableStartupPgo=true`), `CoreCLR con custom` (default). Los paquetes `.mono` y `.coreclr` conviven instalados (applicationId distinto).
- La primera iteración de cada serie es más fría (cachés de FS); las medianas reportadas consideran las 10.

---

## 6. Mantenimiento

- **Regenerar el mibc** cuando cambie el código del startup path (MauiProgram, App, AppShell, primera página, inicialización de la librería) o al actualizar MAUI/dependencias grandes. Es barato: ~15 min siguiendo §4.
- El mibc es de la **app**, no de la librería: consumidores de MaterialDesignControls deberían generar el suyo.
- Para futuros previews: reintentar el flujo oficial `maui profile startup --format mibc` (el workaround deja de ser necesario cuando se cierre #36637).

---

## 7. ¿Y en Mono?

Mono tiene el mecanismo equivalente: **AOT profile custom** (`*.aotprofile`) aplicado sobre el AOT parcial. El SDK ya usa uno default (por eso el baseline Mono rinde bien), pero se puede grabar uno propio:

```bash
# idea general (toolchain clásica de Xamarin/.NET Android):
adb shell setprop debug.mono.profile "log:aotprofile,output=/data/data/<pkg>/files/custom.aotprofile"
# arrancar la app, recorrer el startup, matar el proceso, extraer el profile
# filtrar con aprofutil, y referenciar en el csproj:
#   <AndroidAotProfiles Include="custom.aotprofile" />   (o $(AndroidAotProfile))
```

No se midió en esta sesión. La ganancia esperada es menor que en CoreCLR (Mono AOT default ya está bastante tuneado y Mono no tiene tiered JIT que "recupere" después), pero el experimento es barato si se quiere completar la tabla.

---

## 8. Notas de entorno (esta máquina)

- **Tamaño del APK Mono (45 MB vs 30 MB)**: el `.csproj` solo fija `RuntimeIdentifiers=android-arm64` para CoreCLR. Un build Mono por CLI sin `-r` empaqueta **arm64 + x86_64** (la slice x86_64 pesa ~15 MB). El APK Mono arm64-only (`-r android-arm64`) pesa **~29 MB**, igual que antes. No tiene relación con el perfilado. Builds de Rider/VS salen arm64-only porque apuntan al dispositivo conectado. Para referencia: CoreCLR arm64 con mibc pesa **~30 MB** — comparable al Mono.
- `dotnet` del PATH roto (apphost x86_64 + hostfxr arm64) → usar `~/dotnet-arm64/dotnet` hasta reparar `/usr/local/share/dotnet/dotnet`.
- Disco: se liberaron ~8,8 GB (AVD Pixel 9 Pro + caché VSCode ShipIt); el restore de NuGet fallaba por `No space left on device`.
- Paquete NuGet corrupto por el disco lleno: `xamarin.androidx.annotation/1.10.0.1` (se limpió y re-restauró).

## Referencias

- [App profiling with `maui profile` — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/maui/developer-tools/cli/profile)
- [Runtimes and compilation in .NET MAUI (PGO/MIBC) — Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/maui/deployment/runtimes-compilation)
- [dotnet/maui-labs (CLI `maui`)](https://github.com/dotnet/maui-labs)
- [dotnet/maui#36637 — nettrace unparseable por dotnet-pgo](https://github.com/dotnet/maui/issues/36637)
- [dotnet/maui#33387 — perfiles PGO en el workload](https://github.com/dotnet/maui/issues/33387)
- `docs/mono-vs-coreclr-maui11-notas.md` (contexto runtimes)
