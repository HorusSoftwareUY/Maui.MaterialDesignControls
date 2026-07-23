# Notas para post: CoreCLR vs Mono en .NET MAUI

> Estas son notas de trabajo para armar un post técnico sobre el impacto del runtime elegido en el peso del APK de release en .NET MAUI Android.

## Referencias

- 📖 [Runtimes and compilation in .NET MAUI (docs oficiales)](https://learn.microsoft.com/en-us/dotnet/maui/deployment/runtimes-compilation?view=net-maui-10.0) — explica Mono, CoreCLR, NativeAOT, ReadyToRun e intérprete Mono. Válido para .NET MAUI 8–11.
- 🐛 [dotnet/android#7389](https://github.com/dotnet/android/issues/7389) — bug conocido: crash de Mono AOT con `AndroidLinkMode=None`
- 📄 [mono-vs-coreclr-maui11-notas.md](./mono-vs-coreclr-maui11-notas.md) — bitácora detallada de debugging: 4 bugs encontrados durante la configuración del build (AOT + linker), causa raíz de cada uno, workarounds y configuración final validada en runtime

---

## 🏗️ Cómo está compilado cada APK (estado actual)

### Mono (`-p:UseMono=true`)

| Propiedad | Valor | Motivo |
|---|---|---|
| `UseMonoRuntime` | `true` | Selecciona el runtime Mono |
| `AndroidLinkMode` | `Full` | **Requerido** para que AOT funcione. `None` crashea (XA1030 / `instance_size` mismatch). `SdkOnly` tiene una regresión activa en .NET 10/11 preview (#33032). |
| `RunAOTCompilation` | `true` | AOT del pipeline Mono — precompila a `.so` nativos. Requiere trimming activo. |
| `PublishReadyToRun` | `false` | R2R es de CoreCLR; si se activa en Mono rompe Mono.Cecil en `_LinkAssembliesNoShrink` |
| `EmbedAssembliesIntoApk` | `true` | Embebe los dlls en el APK |
| `AndroidUseAssemblyStore` | `false` | Deshabilita el formato assembly store |

> ⚠️ **`Full` trimming** implica que el linker elimina también código propio y de NuGets. Cualquier uso de reflexión no anotado (ej: `JsonSerializer` sin `JsonSerializerContext`) lanza `InvalidOperationException` en runtime. Fix aplicado: `SampleJsonContext` (source generation) en el sample + `try/catch NotSupportedException` en el logger de debug de la librería.

### CoreCLR (`-p:UseMono=false` o por defecto)

| Propiedad | Valor | Motivo |
|---|---|---|
| `UseMonoRuntime` | `false` | Selecciona el runtime CoreCLR |
| `AndroidLinkMode` | `SdkOnly` | Default recomendado; no tiene el bug de Mono con `SdkOnly` |
| `RunAOTCompilation` | `false` | El pipeline AOT de Mono no aplica a CoreCLR (build error en MAUI 11 preview) |
| `PublishReadyToRun` | `true` | R2R de CoreCLR: pre-JIT en build-time embebido en los `.dll`. Mejor startup y throughput. |
| `EmbedAssembliesIntoApk` | `true` | Ídem Mono |
| `AndroidUseAssemblyStore` | `false` | Ídem Mono |

> ℹ️ La agresividad de trimming **no es equivalente** entre ambos runtimes: Mono usa `Full` (trimea todo), CoreCLR usa `SdkOnly` (solo BCL/SDK). El tamaño final del APK no es 100% comparable sin igualar el nivel de trimming.

---

## ⚡ TL;DR — Configuración crítica del build (referencia rápida)

### Flags de compilación por runtime

| Flag | Mono | CoreCLR | Por qué |
|------|------|---------|---------|
| `RunAOTCompilation` | `true` | `false` | Es el pipeline AOT de **Mono**; aplicarlo a CoreCLR en MAUI 11 preview causa build errors |
| `PublishReadyToRun` | `false` | `true` | R2R pre-JIT de **CoreCLR**; rompe Mono.Cecil en `_LinkAssembliesNoShrink` de Mono |
| `UseMonoRuntime` | `true` | `false` | Selector de runtime en el toolchain MAUI |

### Versiones (MAUI 11 preview, .NET 11)

| Variable | Valor | Dónde |
|----------|-------|-------|
| `NetVersion` | `net11.0` | `Directory.Build.props` |
| `MauiVersion` | `11.0.0-preview.5.26304.4` | `Directory.Build.props` |
| `Xamarin.AndroidX.AppCompat` | `>= 1.7.1.3` | `Directory.Packages.props` |

> ⚠️ **`MauiVersion` debe coincidir exactamente con el workload instalado.** Si no coinciden, los paquetes NuGet no encuentran los assets de la TFM correcta y el build explota con CS0234 al primer rebuild limpio.

### Breaking changes MAUI 11 en XAML

| Error | Causa | Fix |
|-------|-------|-----|
| `MAUIG1001` | `{x:Reference Name}` en `Style/Setter` dentro de `ResourceDictionary` no resuelve a compile-time | Reemplazar con `{TemplateBinding Prop}` en ControlTemplates |
| `MAUIX2002` | `RelativeSource` como atributo XML en `<Binding>` no es BindableProperty | Usar BindableProperties computadas en code-behind + `{TemplateBinding}` |

---

## Nota 1 — ¿Por qué el APK con CoreCLR pesa más que con Mono?

### Contexto

Al compilar una app .NET MAUI para Android en modo **Release**, se puede elegir entre dos runtimes:

- **Mono** — el runtime histórico de Xamarin/MAUI, optimizado para mobile
- **CoreCLR** — el runtime moderno de .NET (desktop/server), portado a mobile más recientemente

### Razones del mayor tamaño con CoreCLR

1. **Runtime más pesado por diseño**  
   CoreCLR fue construido para desktop/server y luego portado a mobile. Mono, en cambio, fue diseñado desde el inicio para dispositivos embebidos y móviles, siendo mucho más compacto.

2. **AOT compilation genera `.so` más grandes**  
   Con CoreCLR, el compilador AOT (Ahead-of-Time) produce archivos nativos (`.so`) de mayor tamaño. Mono usa un modelo de AOT más compacto y en algunos casos interpreta parte del IL.

3. **Trimming menos maduro en mobile**  
   El linker/trimmer no es igual de agresivo con CoreCLR que con Mono. Mono lleva años con optimizaciones de tamaño específicas para mobile; CoreCLR recién está madurando en ese aspecto.

4. **Mayor porción del BCL empaquetada por defecto**  
   CoreCLR tiende a incluir más del Base Class Library en el bundle, aunque esto se puede reducir con trimming agresivo.

### Cómo reducir el tamaño con CoreCLR

```xml
<!-- Activar trimming agresivo -->
<PublishTrimmed>true</PublishTrimmed>
<TrimMode>link</TrimMode>

<!-- Separar APKs por ABI para no empaquetar todas las arquitecturas en uno -->
<AndroidCreatePackagePerAbi>true</AndroidCreatePackagePerAbi>

<!-- Comprimir recursos con AAPT2 (ya está activo por defecto en MAUI) -->
<AndroidUseAapt2>true</AndroidUseAapt2>

<!-- Habilitar compresión de assemblies -->
<AndroidEnableAssemblyCompression>true</AndroidEnableAssemblyCompression>
```

### Conclusión

CoreCLR **prioriza rendimiento sobre tamaño**. El mayor peso del APK es un trade-off esperado y documentado. Microsoft está trabajando activamente en reducirlo en versiones futuras del SDK.

---

---

## Nota 2 — Benchmark: Lista virtual con 10.000 items

### Qué se implementó

Una página de benchmark (`BenchmarkListPage`) en la app de sample, accesible desde el menú hamburguesa (sección **Benchmarks**, primera en la lista).

El benchmark carga 10.000 items parseados desde un JSON pre-generado (~1.5MB) y mide 4 fases por separado:

| Fase | Operación |
|---|---|
| 1. JSON deserialize | `JsonSerializer.Deserialize<List<BenchmarkRawDto>>()` |
| 2. LINQ map + sort | Proyección DTO → modelo + `OrderByDescending(score)` |
| 3. Date parse + format | `DateTime.Parse` + `.ToString("MMM dd, yyyy")` × 10k |
| 4. Bind collection | `new ObservableCollection<BenchmarkItem>(list)` |

El JSON se pre-genera al entrar a la pantalla (no se cuenta en las métricas). El botón **Reload** repite el ciclo completo para obtener múltiples muestras.

### Por qué estas fases muestran diferencias entre runtimes

- **JSON deserialize**: `System.Text.Json` se beneficia del JIT de CoreCLR en code paths repetitivos (reflection-free source gen aparte).
- **LINQ + proyección**: El compilador JIT de CoreCLR optimiza mejor los iteradores y lambdas en caliente.
- **Date parse/format**: Operaciones de string intensivas donde el GC y la inlining de CoreCLR marcan diferencia.
- **Bind collection**: Mide el overhead del constructor de `ObservableCollection` y el sistema de notificaciones.

### Estructura de cada item (template rico)

- Badge circular con score (color rojo/naranja/verde según valor)
- Nombre + email + tags
- Departamento + fecha formateada + estado activo/inactivo

### Archivos involucrados

- `Models/BenchmarkItem.cs` — modelo de presentación
- `Models/BenchmarkRawDto.cs` — DTO para deserialización JSON
- `ViewModels/BenchmarkListViewModel.cs` — lógica de benchmark
- `Pages/BenchmarkListPage.xaml` + `.xaml.cs` — UI con `CollectionView` sin `ScrollView` anidado (virtualización activa)

> **Nota:** además del bind inicial, esta página también mide el rendimiento durante el **scroll de la lista**. Ver Nota 5 para el detalle de esas métricas.

---

## Nota 3 — CoreCLR es estricto con la coerción de tipos en bindings

### El problema

En .NET MAUI, cuando usás `x:DataType` en un `DataTemplate` (bindings compilados), el compilador XAML genera código con asignaciones directas entre la propiedad del ViewModel y la propiedad del control.

**Ejemplo que falla en CoreCLR pero funciona en Mono:**

```xml
<DataTemplate x:DataType="models:BenchmarkItem">
    <!-- Score es int, Text espera string -->
    <Label Text="{Binding Score}" />
</DataTemplate>
```

- **Mono**: el runtime hace la conversión `int → string` implícitamente a través de `BindingExpression`. No explota.
- **CoreCLR**: el binding compilado intenta asignar el `int` directamente al `string`, lanza `TargetInvocationException` envolviendo un `InvalidCastException`.

### Por qué ocurre

Con bindings compilados, el XAML genera algo parecido a:

```csharp
label.Text = item.Score; // int → string: InvalidCastException en CoreCLR
```

Mono tenía una capa de reflexión más permisiva que coercionaba tipos primitivos. CoreCLR con bindings compilados es estricto: los tipos deben coincidir exactamente.

### Síntoma en logcat

```
android.runtime.JavaProxyThrowable: [System.Reflection.TargetInvocationException]:
  Exception has been thrown by the target of an invocation.
  at BindingExpression+BindingExpressionPart.TryGetValue
  at ...BenchmarkListPage+<>c.<InitializeComponent>b__1_1
```

El inner exception real (`InvalidCastException`) no aparece en el logcat de Android — solo el wrapper. Esto lo hace difícil de diagnosticar.

### Fix

Dos opciones:

```xml
<!-- Opción A: StringFormat en el binding -->
<Label Text="{Binding Score, StringFormat='{0}'}" />

<!-- Opción B (recomendada): exponer la propiedad como string desde el ViewModel -->
<Label Text="{Binding ScoreText}" />
```

La opción B es preferible porque el problema no está solo en XAML — cualquier lugar que use reflexión para asignar el valor podría fallar igual.

### Regla general para CoreCLR

> En CoreCLR con bindings compilados (`x:DataType`), **el tipo de la propiedad del modelo debe coincidir exactamente con el tipo de la propiedad del control** al que se bindea. No asumir coerción implícita de tipos primitivos.

Tipos que suelen generar este problema:
- `int` / `double` / `float` → `string` (ej: `Label.Text`)
- `Color` struct → cualquier propiedad que espere `Color` pero viene de reflexión
- `bool` → `string` en controles que no tienen un `BoolToStringConverter` nativo

---

## Nota 4 — Benchmark: Animación de partículas con SkiaSharp

### Qué se implementó

Una página de benchmark (`BenchmarkSkiaPage`) que renderiza N partículas animadas con trail/estela sobre un `SKCanvasView` fullscreen usando SkiaSharp. Cada frame mide por separado el tiempo de física y el tiempo de draw.

### Configuración del benchmark

- **Trail**: 20 posiciones históricas por partícula, dibujadas con alpha decreciente
- **Niveles de carga**: 50 / 100 / 200 / 500 partículas
- **Draw calls por frame**: N partículas × 21 círculos (20 trail + 1 principal)
  - Ej: 200 partículas → 4.200 draw calls/frame → ~250k draw calls/segundo a 60fps
- **Loop**: `InvalidateSurface()` al final de cada `PaintSurface` → rendering continuo sin timer externo

### Métricas que muestra en tiempo real

| Métrica | Descripción | Por qué importa para la comparación |
|---|---|---|
| **FPS** | Frames por segundo del frame actual (1 / tiempo_del_último_frame) | Referencia instantánea, muy variable |
| **avg FPS** | Promedio móvil de los últimos 120 frames (~2 segundos) | El número más representativo del rendimiento sostenido |
| **min FPS** | Mínimo histórico desde que se inició la animación | Revela GC pauses y JIT stalls — el peor caso que ve el usuario |
| **physics ms** | Tiempo del loop C# por frame: actualizar posiciones, rebotar en bordes, enqueue/dequeue del trail | Mide el rendimiento del JIT sobre código numérico puro |
| **draw ms** | Tiempo de las llamadas a Skia canvas por frame: N × 21 `DrawCircle()` con paint/alpha | Mide el overhead del interop .NET → Skia nativo |

### Cómo comparar CoreCLR vs Mono

Ambas versiones conviven en el mismo dispositivo con app IDs distintos:

```bash
# Compilar CoreCLR (ARM64 — dispositivos físicos modernos)
dotnet build -f net11.0-android -c Release -p:UseMono=false
# APK → bin/coreclr/Release/net11.0-android/android-arm64/

# Compilar Mono (fat APK, todas las ABIs soportadas)
dotnet build -f net11.0-android -c Release -p:UseMono=true
# APK → bin/mono/Release/net11.0-android/
```

> **Arquitecturas Android**
> - **arm64-v8a** — todos los teléfonos Android modernos (2017+). Es la única ABI necesaria para benchmarks en dispositivo físico.
> - **x86_64** — solo emuladores en máquinas Intel/AMD y rarísimas tablets/Chromebooks con Intel. No incluido en este build.
> - CoreCLR solo soporta 64-bit (arm64 o x64); Mono soporta arm64, arm, x86 y x64.

> **Nota:** cada variante usa su propio directorio de build (`obj/coreclr/` y `obj/mono/`,
> `bin/coreclr/` y `bin/mono/`) para evitar colisiones de cache de íconos y assets entre runtimes.

**Protocolo de medición:**
1. Abrir la app, ir a **Benchmarks → Skia particles animation**
2. Seleccionar nivel de carga (50 / 100 / 200 / 500)
3. Tocar **Start** y esperar ~30 segundos para que el JIT se estabilice
4. Anotar: avg FPS, min FPS, physics ms, draw ms
5. Repetir en la otra app (mismo dispositivo, mismas condiciones)

### Tabla de resultados (completar)

#### 100 partículas

| Métrica | CoreCLR | Mono |
|---|---|---|
| avg FPS | — | — |
| min FPS | — | — |
| physics ms | — | — |
| draw ms | — | — |

#### 200 partículas

| Métrica | CoreCLR | Mono |
|---|---|---|
| avg FPS | — | — |
| min FPS | — | — |
| physics ms | — | — |
| draw ms | — | — |

#### 500 partículas

| Métrica | CoreCLR | Mono |
|---|---|---|
| avg FPS | — | — |
| min FPS | — | — |
| physics ms | — | — |
| draw ms | — | — |

### Qué se espera observar

- **physics ms**: CoreCLR más rápido por JIT agresivo en loops numéricos y mejor inlining
- **min FPS**: Mono más estable (GC conservador, menos spikes); CoreCLR puede tener caídas por GC generacional
- **draw ms**: Similar en ambos — Skia corre en código nativo, el interop es equivalente
- **avg FPS**: Depende del dispositivo; a 200+ partículas debería verse diferencia

### Archivos involucrados

- `Models/Particle.cs` — posición, velocidad, radio, color, `Queue<SKPoint>` trail cap 20
- `ViewModels/BenchmarkSkiaViewModel.cs` — loop en `PaintSurface`, stats cada 10 frames
- `Pages/BenchmarkSkiaPage.xaml` + `.xaml.cs` — `SKCanvasView` fullscreen + header stats

---


- [ ] Mencionar el impacto de usar AAB (Android App Bundle) vs APK para distribución en Play Store
- [ ] Comparar rendimiento en runtime: ¿vale la pena el peso extra de CoreCLR?
- [ ] Hablar de cuándo conviene cada runtime según el tipo de app
- [ ] Referencias: docs oficiales de Microsoft sobre runtimes en MAUI

---

## Nota 5 — Benchmark: Scroll FPS en lista virtualizada de 10.000 items

### Qué se mide

Luego de que la lista termina de bindear los 10.000 items, la página dispara automáticamente:

1. **25 gestos de scroll incrementales** (`ScrollTo` con `animate: true`), cada uno avanzando ~400 items, con 200 ms de delay entre gestos — simula swipes continuos de dedo en lugar de un único salto al final
2. Un `IDispatcherTimer` a ~16 ms (60 fps objetivo) activo durante toda la secuencia de gestos (~5 segundos)
3. Captura de heap managed antes y después del scroll

### Métricas nuevas en el stats panel

| # | Métrica | Descripción | Por qué importa |
|---|---|---|---|
| 5 | **Avg FPS (scroll)** | FPS promedio calculado como `1000 / avg_tick_delta` durante los 5s de scroll | Rendimiento sostenido de la UI bajo carga de virtualización |
| 6 | **Min FPS (scroll)** | FPS mínimo calculado como `1000 / max_tick_delta` | Revela drops por GC, JIT stalls o item realization costosa |
| 7 | **Scroll duration** | Duración total de la ventana de medición (ms) | Referencia temporal |
| 8 | **Managed heap Δ** | `GC.GetTotalMemory(false)` después − antes del scroll (KB) | Cuánto heap nuevo se asignó para realizar los item templates durante el scroll |

### Cómo funciona el FPS timer

Se usa `IDispatcherTimer` (main thread) con intervalo de 16 ms. Cada tick registra el timestamp (`Stopwatch.ElapsedMilliseconds`). Al terminar la ventana se calculan los deltas entre ticks consecutivos:

```
avg_fps = 1000 / mean(deltas)
min_fps = 1000 / max(delta)   ← el delta más largo = peor frame
```

Este método mide la **tasa de dispatch del hilo principal** — si el UI thread está congestionado (virtualizando items, ejecutando bindings), los ticks se retrasan y el FPS baja. Es un proxy confiable para comparar CoreCLR vs Mono bajo carga de scroll.

### Protocolo de medición

1. Abrir la app → **Benchmarks → Virtual list (10k items)**
2. Tocar **Reload benchmark** y esperar que cargue
3. Tras ~800 ms, la lista ejecuta 25 gestos de scroll automáticos — **no tocar el dispositivo**
4. Esperar ~5 segundos a que aparezcan los valores de scroll
5. Anotar los 4 valores de la sección "Scroll FPS (auto)"
6. Repetir en la otra app (mismo dispositivo, mismas condiciones)

### Tabla de resultados (completar)

| Métrica | CoreCLR | Mono |
|---|---|---|
| Avg FPS (scroll) | — | — |
| Min FPS (scroll) | — | — |
| Scroll duration | — | — |
| Managed heap Δ | — | — |

### Qué se espera observar

- **Avg FPS**: CoreCLR debería ser igual o mejor — el JIT optimiza mejor los loops de binding
- **Min FPS**: Mono puede ser más estable (GC menos agresivo, menos spikes); CoreCLR puede mostrar drops si el GC generacional barre durante el scroll
- **Managed heap Δ**: Similar en ambos — el delta refleja la realización de item templates, no el runtime en sí; diferencias marcan colecciones temporales más grandes en uno de los dos

### Archivos involucrados (cambios de esta nota)

- `ViewModels/BenchmarkListViewModel.cs` — nuevas propiedades + `StartScrollMeasurement` / `StopScrollMeasurement` / timer FPS
- `Pages/BenchmarkListPage.xaml.cs` — suscripción a `PropertyChanged`, 25 gestos de scroll incrementales (~400 items/gesto, 200 ms entre gestos), ventana con `CancellationToken`
- `Pages/BenchmarkListPage.xaml` — 4 nuevas filas en el stats card + `ActivityIndicator` de estado

---

---

## Nota 6 — Benchmark: Tiempo de navegación entre páginas (Navigation Timing)

### Qué se mide

El tiempo que tarda la app en ir desde que el usuario toca un ítem del menú (Main) hasta que la página de destino está **completamente lista** para mostrarse al usuario. Cubre: ejecución de `Shell.GoToAsync`, construcción del ViewModel y la Page (DI), y — en páginas con datos asíncronos — la carga y binding inicial.

### Cómo funciona

El sistema usa `NavigationTimer` (habilitado con la constante de compilación `ENABLE_NAV_TIMING`):

| Momento | Código | Qué ocurre |
|---|---|---|
| **T0 — arranque** | `BaseViewModel.GoToAsync` | Justo antes de llamar a `Shell.Current.GoToAsync`, se llama a `NavigationTimer.Start(route)` y arranca el `Stopwatch`. |
| **T1 — página lista** | `BaseViewModel.ReportPageReady()` | Llama a `NavigationTimer.Complete(Title)`, detiene el reloj y dispara el evento `NavTimingCompleted`. |

`ReportPageReady()` se invoca de dos formas según la página:
- **Automática**: `BaseContentPage.OnAppearing()` la llama si la página no carga datos async (el caso simple).
- **Manual**: las páginas que cargan datos en forma asíncrona deben llamar a `vm.ReportPageReady()` ellas mismas una vez que el binding esté completo (por ejemplo, después del `await` que trae los datos).

### Cómo se muestra el resultado

Al completarse la navegación, aparece un badge flotante (`NavTimingOverlay`) superpuesto sobre la página destino con el formato:

```
⏱ <Título de la página>  —  <ms> ms
```

El badge desaparece solo a los 10 segundos o al tocarlo. Si se navega antes, se descarta automáticamente.

### Cómo habilitar / deshabilitar

Por defecto está activo en Debug **y** Release. Se puede apagar pasando `-p:EnableNavTiming=false` al compilar:

```bash
dotnet build -f net11.0-android -c Release -p:EnableNavTiming=false
```

### Caso a probar

1. Abrir la app → pantalla **Main**
2. Tocar cualquier ítem del menú lateral (ej: **Button**, **TextField**, **Progress indicator**)
3. Observar el badge ⏱ que aparece en la esquina de la página destino
4. Anotar el tiempo (ms) para cada página
5. Repetir en CoreCLR y Mono para comparar: la diferencia refleja el costo de construcción de la página + bindings en cada runtime

### Qué se espera observar

- Páginas simples (sin carga async): <100 ms en ambos runtimes; CoreCLR puede ser marginalmente más rápido por JIT
- Páginas con listas grandes (ej: **BenchmarkList**): la diferencia se amplifica porque el binding de la colección también está en el camino crítico
- En el primer acceso a cada página, CoreCLR puede tardar más (JIT en frío); en accesos subsiguientes tiende a nivelar o superar a Mono

### Archivos involucrados

- `Utils/NavigationTimer.cs` — lógica de medición (stopwatch + evento `NavTimingCompleted`)
- `ViewModels/BaseViewModel.cs` — `GoToAsync` (T0) y `ReportPageReady` (T1)
- `Pages/BaseContentPage.cs` — llama a `ReportPageReady` en `OnAppearing` para páginas simples
- `Views/NavTimingOverlay.xaml` + `.xaml.cs` — badge flotante que muestra el resultado durante 10 s
- `AppShell.xaml.cs` — `EnsureNavTimingOverlay()` inyecta el overlay en el `Grid` de cada página

---

*Última actualización: 2026-07-09 — Nota 5 actualizada: scroll pasa de un único `ScrollTo` al final a 25 gestos incrementales de 200 ms para simular scroll manual realista*

---

## Nota 7 — Diagnóstico: Por qué CoreCLR rinde peor que Mono en el benchmark actual

### Contexto

Con la configuración de Release actual (`RunAOTCompilation=false`, `PublishReadyToRun=false`) CoreCLR muestra peor rendimiento que Mono en los benchmarks de la app de sample. Esto **no significa que CoreCLR sea más lento en general** — significa que la comparación no es justa. Hay dos categorías de causas: la configuración de compilación y las presiones de GC del código de benchmark.

---

### Causa A — Configuración de compilación (la más importante)

#### `RunAOTCompilation=false` para ambos runtimes

En Release Android, el `.csproj` deshabilita AOT para ambos runtimes:

```xml
<!-- ⚠ Esto deja CoreCLR corriendo con JIT puro — sin precalentamiento -->
<RunAOTCompilation>false</RunAOTCompilation>
```

Sin AOT, CoreCLR arranca cada método en frío con JIT. Los primeros segundos del benchmark Skia o la primera carga de BenchmarkList muestran peor performance porque el JIT está compilando en caliente mientras el benchmark ya está midiendo.

Mono históricamente tenía una estrategia diferente y no tiene el mismo "cold start" tan marcado sin AOT.

#### `PublishReadyToRun=false` para CoreCLR (R2R deshabilitado)

R2R (Ready-to-Run) pre-compila los assemblies a código nativo en tiempo de build, embebidos en el APK. Esto es **distinto** de `RunAOTCompilation` y es la optimización de startup más efectiva en CoreCLR.

Estaba deshabilitado globalmente por un problema real con Mono: cuando R2R está activo, el compilador limpia el flag `ILOnly` de los PE files, y eso rompe Mono.Cecil en el paso `_LinkAssembliesNoShrink` del toolchain de Mono. Pero ese paso **no existe en builds CoreCLR**, por lo que R2R es seguro habilitarlo condicionalmente solo para `UseMono=false`.

#### Fix aplicado al `.csproj`

```xml
<!-- AOT: Mono only. RunAOTCompilation es el pipeline AOT de Mono — aplicarlo a CoreCLR
     en MAUI 11 preview falla (intenta correr el compilador Mono AOT sobre assemblies CoreCLR).
     CoreCLR obtiene pre-compilación vía R2R abajo. -->
<RunAOTCompilation Condition="'$(UseMono)' == 'true'">true</RunAOTCompilation>
<RunAOTCompilation Condition="'$(UseMono)' != 'true'">false</RunAOTCompilation>

<!-- R2R solo para CoreCLR — Mono.Cecil falla con assemblies R2R en _LinkAssembliesNoShrink -->
<PublishReadyToRun Condition="'$(UseMono)' != 'true'">true</PublishReadyToRun>
<PublishReadyToRun Condition="'$(UseMono)' == 'true'">false</PublishReadyToRun>
```

> **Nota:** `RunAOTCompilation=true` para ambos runtimes fue la config original, pero en MAUI 11 preview causa errores de build en CoreCLR porque el toolchain intenta correr el compilador AOT de Mono sobre assemblies CoreCLR. La config correcta es Mono → AOT, CoreCLR → R2R.

> **Nota de build time:** con `AndroidLinkMode=None` (sin trimming), `RunAOTCompilation=true` en Mono compila el BCL completo — el build tarda 3-5× más. Para builds de iteración: `-p:RunAOTCompilation=false`.

---

### Causa B — Presión de GC en el código de benchmark (sin modificar el código)

El benchmark de Skia contiene patrones que generan presión de GC por frame. Estos no se modifican (el objetivo es que el benchmark sea lo que es), pero se documentan para entender por qué afectan más a CoreCLR que a Mono:

| Patrón | Allocations/frame (200 partículas, 60fps) | Por qué afecta más a CoreCLR |
|---|---|---|
| `p.Trail.ToArray()` en el loop de render | 200 arrays × 60fps = 12.000/s (~1.9 MB/s Gen0) | CoreCLR tiene GC generacional → colecciones Gen0 frecuentes → pauses → min FPS bajo |
| `new SKPaint { ... }` por frame | 1 objeto finalizable/frame = 60/s | CoreCLR finalization queue más estricta |
| `Stopwatch.StartNew()` ×2 por frame | 120 objetos/s | Presión adicional de Gen0 |
| 5 strings interpoladas cada 10 frames | 36 strings/s | Acumulación en Gen0 |

Mono usa un GC **conservador non-moving**: no mueve objetos y recolecta menos frecuentemente bajo presión de objetos de corta vida. Esto produce menos pauses (mejor min FPS) a costa de mayor uso de memoria y menor throughput general.

CoreCLR usa un GC **generacional tracing**: diseñado para throughput máximo en server/desktop. Bajo alta tasa de allocaciones Gen0, el GC interviene más frecuentemente → pauses visibles en el FPS.

> **Conclusión:** el benchmark actual mide en parte el GC del runtime, no solo la velocidad de ejecución del JIT. Eso favorece estructuralmente a Mono. Con AOT habilitado, el JIT cold-start desaparece; la diferencia de GC persiste pero se puede medir por separado (min FPS vs avg FPS).

---

### Tabla de modos de compilación recomendados

| Modo | `RunAOTCompilation` | `PublishReadyToRun` | `AndroidLinkMode` | Cuándo usar |
|---|---|---|---|---|
| **Debug Mono** | `false` | `false` | — | Iteración rápida, depuración |
| **Debug CoreCLR** | `false` | `false` | — | Ídem |
| **Release Mono (benchmark)** | `true` | `false` | `None` | Medición justa Mono |
| **Release CoreCLR (benchmark)** | `true` | `true` | `None` | Medición justa CoreCLR |
| **Release CoreCLR (producción)** | `true` | `true` | `SdkOnly` o `Full` | APK de distribución |

> R2R (`PublishReadyToRun`) es distinto de AOT puro (`RunAOTCompilation`): R2R embebe código pre-JITeado que se usa como "hint" y tiene fallback a JIT; AOT puro no tiene fallback. En la práctica, para Android móvil, R2R + AOT juntos dan el mejor tiempo de startup y mejor throughput sostenido.

---

*Agregado: 2026-07-14*

---

## Nota 8 — Historial de configuración del build (qué se cambió y por qué)

### Estado final del build tras la migración a .NET 11

Todo el repositorio quedó unificado en **.NET 11 preview** con la siguiente configuración:

| Archivo | Propiedad | Valor final | Motivo |
|---|---|---|---|
| `Directory.Build.props` | `NetVersion` | `net11.0` | TFM principal de todo el repo |
| `Directory.Build.props` | `MauiVersion` | `11.0.0-preview.5.26304.4` | Debe coincidir con el workload instalado |
| `Directory.Packages.props` | `Microsoft.Maui.Controls` | `$(MauiVersion)` | Resuelve a la versión correcta vía variable |
| `Directory.Packages.props` | `Microsoft.Maui.Core` | `$(MauiVersion)` | Antes estaba hardcodeado en `10.0.10` |
| `samples/Directory.Packages.props` | `Microsoft.Maui.Controls` (Update) | `11.0.0-preview.5.26304.4` | Redundante pero explícito como documentación |
| Sample `.csproj` | `RunAOTCompilation` | `true` Mono / `false` CoreCLR | AOT es pipeline Mono; en CoreCLR causa build error en MAUI 11 preview |
| Sample `.csproj` | `PublishReadyToRun` | `true` CoreCLR / `false` Mono | R2R es pre-compilación de CoreCLR; rompe Mono.Cecil en Mono builds |

### Bug latente en la migración original

El commit `d57173f` ("initial setup, running ok (lib and sample) in .net 11", 7 Jul 2026) cambió `NetVersion` a `net11.0` pero **olvidó actualizar `MauiVersion`**, que quedó en `10.0.10`. El error no era visible porque los builds incrementales usaban caché de compilaciones anteriores. Al habilitar `RunAOTCompilation=true` (que fuerza un rebuild limpio) el CS0234 salió a la luz.

### Por qué no se puede volver a .NET 10 en esta máquina

Solo está instalado el workload `maui/11.0.0-preview.5.26304.4`. Los workload packs de MAUI .NET 10 no están presentes. Sin esos packs, el compilador no puede resolver los assemblies de plataforma (`Microsoft.Maui.Platform`, `Microsoft.Maui.Graphics`, etc.) para `net10.0-android`. Para publicar el NuGet con soporte net10.0, se necesita un pipeline de CI/CD con ambos workloads instalados.

### MAUIG1001 — `x:Reference` en `ResourceDictionary` (breaking change MAUI 11)

**Error:** `MAUIG1001: Name 'InputBase' not found in any NameScope`

**Causa:** MAUI 11 endureció la resolución de `NameScope` en el compilador XAML. `{x:Reference InputBase}` dentro de `<Style>/<Setter>` y `<ControlTemplate>` que viven en `<ContentView.Resources>` (ResourceDictionary) no pueden ver el `x:Name` del ContentView padre en tiempo de compilación. En MAUI 10 esto se resolvía en runtime.

- `MauiEnableXamlCBindingWithSourceCompilation=false` → no aplica (MAUIG1001 viene del paso de parseo XAML, no del source generator de bindings).
- `[XamlCompilation(XamlCompilationOptions.Skip)]` → no aplica (el source generator de MAUI 11 es Roslyn-based y no chequea este atributo).

**Fix real aplicado:** `MaterialInputBase.xaml` usa el patrón `ControlTemplate` (ContentView con templates en Resources). La solución correcta es reemplazar:
- `{Binding Prop, Source={x:Reference InputBase}}` → `{TemplateBinding Prop}` (válido en Style/Setter dentro de ControlTemplate; resuelve al TemplatedParent, que es el propio ContentView)
- `{Binding Prop, Source={x:Reference InputBase}, Converter=X}` → `{TemplateBinding Prop, Converter=X}`
- `<Binding Path="Prop" Source="{x:Reference InputBase}" />` dentro de `<MultiBinding>` → **no usar `RelativeSource` como atributo XML** (MAUIX2002: no es BindableProperty, el source generator no lo acepta)

Se reemplazaron **63 ocurrencias** con `{TemplateBinding}`. Se retuvo `{x:Reference OutlinedHint}` (referencia intra-template, válida en MAUI 11).

### MAUIX2002 — `RelativeSource` no soportado en `<Binding>` element form

**Error:** `MAUIX2002: No accessible property, BindableProperty, or event found for "RelativeSource"`

**Causa:** El source generator de MAUI 11 valida los atributos XML de `<Binding>` contra BindableProperties. `RelativeSource` en `Binding` es una propiedad CLR, no una BindableProperty → el compilador la rechaza cuando está en forma de elemento (`<Binding RelativeSource="..." />`). En la forma inline `{Binding X, Source={RelativeSource TemplatedParent}}` sí funciona, pero no puede usarse dentro de `<MultiBinding>`.

**Fix aplicado:** Para los dos `<MultiBinding>` en `TrailingIcon` Style (ImageSource e IsVisible), se agregaron dos BindableProperties computadas a `MaterialInputBase`:
- `TrailingIconImageSourceProperty` → `hasError ? errorIcon ?? trailingIcon : trailingIcon`
- `TrailingIconVisibleProperty` → `trailingIcon != null || (hasError && errorIcon != null)`

Se actualizan mediante `UpdateTrailingIconComputed()`, hookeado en el `propertyChanged` de `TrailingIconProperty`, `ErrorIconProperty` y `HasErrorProperty`. En el XAML se usan como `{TemplateBinding TrailingIconImageSource}` y `{TemplateBinding TrailingIconVisible}`.

Los converters `TrailingIconSourceConverter` y `TrailingIconIsVisibleConverter` se removieron del ResourceDictionary (la lógica está ahora en el code-behind).

*Agregado: 2026-07-14*

---

## Issue conocido — Crash de Mono AOT con `AndroidLinkMode=None`

**Referencia:** [dotnet/android#7389](https://github.com/dotnet/android/issues/7389)

### Síntoma

La app crashea inmediatamente al iniciar en Android cuando se compila con Mono AOT (`RunAOTCompilation=true`) combinado con `AndroidLinkMode=None`. El crash ocurre en el runtime Mono durante la inicialización de tipos y se manifiesta como:

```
E  * Assertion at class-init.c:2691, condition `klass->instance_size == instance_size' not met
A  Fatal signal 6 (SIGABRT)
```

La app vive menos de 2 segundos: muere antes de llegar a mostrar cualquier pantalla.

### Por qué ocurre

Es un bug de larga data en el toolchain de .NET Android (reportado en 2022, sigue abierto). Cuando el linker está desactivado (`AndroidLinkMode=None`) y se activa AOT, el compilador AOT de Mono genera código nativo (`.so`) con un layout de objetos que no coincide con el que el runtime Mono espera al cargar las DLLs sin trimming. El desajuste de `instance_size` hace que Mono aborte.

**Combinaciones afectadas:**

| `AndroidLinkMode` | `RunAOTCompilation` | Resultado |
|---|---|---|
| `None` | `true` | 💥 Crash al inicio |
| `None` | `false` | ✅ OK |
| `SdkOnly` / `Full` | `true` | ✅ OK |

### Workaround

Deshabilitar AOT cuando el linker está apagado:

```xml
<PropertyGroup Condition="'$(Configuration)' == 'Release' and $(TargetFramework.Contains('-android'))">
    <AndroidLinkMode>None</AndroidLinkMode>
    <RunAOTCompilation Condition="'$(UseMono)' == 'true'">false</RunAOTCompilation>
</PropertyGroup>
```

O bien, activar el linker (que también resuelve el crash pero agrega tiempo de build y puede requerir `[Preserve]` en algunos tipos):

```xml
<AndroidLinkMode>SdkOnly</AndroidLinkMode>
```

### Estado del issue

Abierto desde 2022 en el repo `dotnet/android`. No tiene fecha de fix confirmada. El crash se reproduce tanto en .NET 6/7/8 como en .NET 11 preview con Mono.

*Agregado: 2026-07-14*

---

## 🔍 Profiling de startup — por qué el tiempo no difiere tanto entre Mono y CoreCLR

**Fecha:** 2026-07-23

### Contexto

Después de tener ambos builds funcionando (Mono + AOT y CoreCLR + R2R), los tiempos de startup medidos manualmente resultaron más parecidos de lo esperado. La hipótesis es que el cuello de botella no está en el runtime en sí, sino en fases comunes del startup de MAUI que corren igual en ambos runtimes (inicialización del DI container, reflexión sobre assemblies, inflar XAML, etc.). Para confirmarlo hace falta instrumentación.

### Approach elegido: `StartupProfiler` (Layer 1 — C# puro)

Se evaluaron tres capas de profiling:

| Layer | Qué mide | Requiere código | Cómo ver resultados |
|---|---|---|---|
| **Layer 1** — `StartupProfiler` (✅ implementado) | Tiempo entre fases del startup en C# | Sí — marks en el código | `adb logcat -s STARTUP_PROFILE` |
| Layer 2 — `atrace` / Perfetto | Lo mismo pero en flamegraph con contexto del OS | Sí — `Android.OS.Trace.BeginSection` | `adb shell atrace` + [Perfetto UI](https://ui.perfetto.dev/) |
| Layer 3 — `dotnet-trace` / EventPipe | JIT por método, carga de assemblies, GC | No — tooling externo | `dotnet-trace collect` + PerfView / SpeedScope |

Se optó por **Layer 1** para empezar: es el más rápido de integrar, da resultados inmediatos comparables entre Mono y CoreCLR, y pinpoints exactamente qué bloque C# es el responsable. Las otras capas quedan como follow-up si Layer 1 no da suficiente resolución.

### Implementación

Nueva clase `Utils/StartupProfiler.cs` gateada por `#if ENABLE_STARTUP_PROFILING` (análoga a `NavigationTimer`). Activa por defecto, se apaga con `-p:EnableStartupProfiling=false`.

**Fases instrumentadas (en orden):**

| # | Fase | Archivo | Por qué es relevante |
|---|------|---------|----------------------|
| T0 | `MainApplication.ctor` | `MainApplication.cs` | Primer punto donde corre código .NET en el proceso |
| 1 | `MauiApp.CreateBuilder()` | `MauiProgram.cs` | Baseline de cuánto tarda arrancar el host builder |
| 2 | `UseSkiaSharp` | `MauiProgram.cs` | SkiaSharp registra handlers de plataforma |
| 3 | `UseMauiCommunityToolkit` | `MauiProgram.cs` | CT registra más handlers/behaviours |
| 4 | `UseMaterialDesignControls` | `MauiProgram.cs` | Nuestro plugin — registra fonts, temas, handlers |
| 5 | `AutoConfigureViewModelsAndPages` | `MauiProgram.cs` | ⚠️ Reflexión: escanea todo el assembly buscando tipos |
| 6 | `RegisterServices` | `MauiProgram.cs` | Registro de platform services |
| 7 | `builder.Build()` | `MauiProgram.cs` | ⚠️ Compilación del DI container |
| 8 | `App.InitializeComponent` | `App.xaml.cs` | Carga del XAML raíz |
| 9 | `MaterialDesignControls.InitializeComponents` | `App.xaml.cs` | Init del plugin |
| 10 | `MainPage = new AppShell()` | `App.xaml.cs` | Infla el Shell completo (rutas, tabs) |
| T_final | First `OnAppearing` | `BaseContentPage.cs` | Primer frame visible al usuario → dispara `Dump()` |

**Output esperado en logcat:**

```
adb logcat -s STARTUP_PROFILE

[STARTUP_PROFILE] === Startup timeline ===
[STARTUP_PROFILE]   MainApplication.ctor                              +0 ms
[STARTUP_PROFILE]   MauiApp.CreateBuilder() done                      +12 ms
[STARTUP_PROFILE]   UseSkiaSharp done                                 +45 ms
[STARTUP_PROFILE]   UseMauiCommunityToolkit done                      +80 ms
[STARTUP_PROFILE]   UseMaterialDesignControls done                    +140 ms
[STARTUP_PROFILE]   AutoConfigureViewModelsAndPages done              +230 ms   ← sospechoso
[STARTUP_PROFILE]   RegisterServices done                             +235 ms
[STARTUP_PROFILE]   builder.Build() done                              +410 ms   ← sospechoso
[STARTUP_PROFILE]   App.InitializeComponent done                      +500 ms
[STARTUP_PROFILE]   MaterialDesignControls.InitializeComponents done  +510 ms
[STARTUP_PROFILE]   App.MainPage = new AppShell() done                +590 ms
[STARTUP_PROFILE]   First OnAppearing: HomePage                       +750 ms
[STARTUP_PROFILE]   --- TOTAL STARTUP: 750 ms ---
```

### Cómo capturar los tiempos en el dispositivo

El `StartupProfiler` escribe en logcat. El `Dump()` es idempotente (solo dispara una vez por proceso), así que hay que hacer **Force Stop** antes de cada medición para obtener un cold start limpio.

**Workflow para CoreCLR (dos terminales):**

```bash
# Terminal 1 — dejar escuchando ANTES de abrir la app
adb logcat -s STARTUP_PROFILE

# Terminal 2 — force stop + abrir
adb shell am force-stop com.horusstudio.maui.materialdesigncontrols.sample.coreclr
adb shell am start -n com.horusstudio.maui.materialdesigncontrols.sample.coreclr/com.horusstudio.maui.materialdesigncontrols.sample.MainActivity
```

**Workflow para Mono:**

```bash
# Terminal 1
adb logcat -s STARTUP_PROFILE

# Terminal 2
adb shell am force-stop com.horusstudio.maui.materialdesigncontrols.sample.mono
adb shell am start -n com.horusstudio.maui.materialdesigncontrols.sample.mono/com.horusstudio.maui.materialdesigncontrols.sample.MainActivity
```

**Guardar output a archivo para comparar:**

```bash
# Terminal 1 — redirigir a archivo mientras se captura
adb logcat -c && adb logcat -s STARTUP_PROFILE | tee coreclr.txt
# (en otra sesión, una vez que aparece el output: Ctrl+C)

# Repetir para Mono
adb logcat -c && adb logcat -s STARTUP_PROFILE | tee mono.txt
```

> ⚠️ `adb logcat -c` limpia el buffer previo. Correrlo antes de abrir la app evita capturar tiempos de una ejecución anterior.

### Cómo usarlo

```bash
# Build CoreCLR con profiling (activo por defecto)
dotnet build -p:UseMono=false -f net11.0-android -t:Run

# Build Mono con profiling
dotnet build -p:UseMono=true -f net11.0-android -t:Run

# Filtrar logcat
adb logcat -s STARTUP_PROFILE

# Desactivar profiling
dotnet build -p:UseMono=false -p:EnableStartupProfiling=false -f net11.0-android -t:Run
```

### Próximos pasos para el post

- Correr ambos builds con `adb logcat -s STARTUP_PROFILE` y capturar los números reales
- Comparar fase por fase: Mono vs CoreCLR — ¿dónde gana cada uno?
- Hipótesis a validar: `AutoConfigureViewModelsAndPages` (reflexión) y `builder.Build()` (DI) son iguales en ambos runtimes → explican la similitud en el total
- Si el cuello es `UseMaterialDesignControls` o `UseSkiaSharp`, vale la pena agregar Layer 2 (atrace) para ver si hay I/O o binder calls detrás
- Documentar los números reales y agregar al post como tabla comparativa

### `maui profile startup` — aclaración sobre el nombre

`maui profile startup` es el **comando CLI oficial de MAUI** para medir el startup. El nombre puede confundir:

- **"profile"** acá es el **verbo** (perfilar = medir performance), no el sustantivo. No tiene relación directa con PGO por defecto.
- Internamente usa `dotnet-trace` (EventPipe de CoreCLR) — por eso **solo funciona en CoreCLR**, no en Mono.
- A diferencia de `StartupProfiler` (que es C# puro y mide fases que vos definís), `maui profile startup` captura internals del runtime: qué métodos se JITearon, carga de assemblies, GC, etc.

La conexión con PGO es **opcional**: si se corre con `-p:MauiProfilingHelperEnableRuntimePgo=true`, además de la traza de diagnóstico genera un `.mibc` (Managed IL Block) con datos de PGO. Ese `.mibc` se puede alimentar al compilador R2R en el siguiente build para que precompile exactamente los métodos hot del startup.

```
maui profile startup (solo mide)
    ↓ con MauiProfilingHelperEnableRuntimePgo=true
genera startup.mibc (datos de qué métodos son hot)
    ↓ se pasa a crossgen2 en el siguiente build
R2R precompila los métodos hot → startup más rápido
```

| Herramienta | Propósito | Funciona en Mono |
|---|---|---|
| `StartupProfiler` (nuestro) | Medir fases definidas en C# | ✅ |
| `maui profile startup` | Medir internals del runtime con dotnet-trace | ❌ (CoreCLR only) |
| `MauiProfilingHelperEnableRuntimePgo` | Generar `.mibc` para optimizar builds futuros | ❌ (CoreCLR only) |
| `AndroidEnableProfiledAot` | Equivalente de PGO para Mono — precompila métodos hot | ✅ (Mono only) |

---

## ⚡ Setup de `maui profile startup` + PGO para CoreCLR

**Fecha:** 2026-07-23

### Qué se hizo

Se integró el paquete `Microsoft.Maui.ProfilingHelper` y el CLI `maui` para poder correr sesiones de profiling de startup con `dotnet-trace` y opcionalmente generar datos de PGO (`.mibc`) para optimizar builds futuros de CoreCLR.

### Componentes agregados

| Componente | Dónde | Qué hace |
|---|---|---|
| `Microsoft.Maui.ProfilingHelper` NuGet | `.csproj` (CoreCLR Android only) | Expone `MauiProfilingMarker.Complete()` para señalizar fin del startup a la herramienta |
| `MauiProfilingMarker.Complete()` | `BaseContentPage.OnAppearing` (gated `!USE_MONO`) | Detiene la traza de `maui profile startup` automáticamente al llegar al primer frame |
| `PublishReadyToRunAdditionalArgs` | `.csproj` Release CoreCLR | Pasa `--mibc Profiling/startup.mibc` a `crossgen2` cuando el archivo existe |
| `Profiling/README.md` | `samples/.../Profiling/` | Instrucciones para generar y regenerar el `.mibc` |
| `Microsoft.Maui.Cli` tool | Global (`~/.dotnet/tools`) | Provee el comando `maui profile startup` |

### Instalación del CLI (one-time)

```bash
dotnet tool install -g Microsoft.Maui.Cli --version "0.1.0-preview.12.26368.2"
# Agregar al PATH si no está:
export PATH="$PATH:/Users/$USER/.dotnet/tools"
```

### Workflow completo para generar el `.mibc`

```bash
# Desde el directorio del sample project:
cd samples/HorusStudio.Maui.MaterialDesignControls.Sample

# Correr la sesión de profiling (build + deploy + profiling en un solo paso)
maui profile startup \
  --framework net11.0-android \
  --configuration Release \
  -p:MauiProfilingHelperEnableRuntimePgo=true \
  --format mibc \
  --stopping-event-provider-name Microsoft.Maui.ProfilingHelper \
  --stopping-event-event-name StartupComplete \
  --output Profiling/startup.mibc
```

La herramienta:
1. Hace el build en Release
2. Instala la app en el dispositivo
3. La lanza **suspendida**
4. Conecta `dotnet-trace` inmediatamente
5. La reanuda — el startup corre bajo traza
6. Cuando `MauiProfilingMarker.Complete()` se dispara (first `OnAppearing`), la traza se detiene automáticamente
7. Genera `Profiling/startup.mibc`

### Activación automática en el siguiente build

Una vez que `Profiling/startup.mibc` existe, el `.csproj` lo pasa automáticamente a `crossgen2`:

```xml
<PublishReadyToRunAdditionalArgs Condition="Exists('Profiling\startup.mibc')">
  --mibc:Profiling\startup.mibc
</PublishReadyToRunAdditionalArgs>
```

No hace falta ningún flag extra — simplemente buildear en Release CoreCLR ya usa el perfil.

### Comparar con y sin PGO

```bash
# Con PGO (si startup.mibc existe)
dotnet build -p:UseMono=false -c Release -f net11.0-android

# Sin PGO (forzar ignorar el .mibc)
dotnet build -p:UseMono=false -c Release -f net11.0-android -p:PublishReadyToRunAdditionalArgs=""
```

### Cuándo regenerar el `.mibc`

- Al agregar nuevas páginas o servicios al startup
- Después de un upgrade mayor de MAUI / .NET
- Si el startup regresa a tiempos similares al de Mono inesperadamente

*Agregado: 2026-07-23*
