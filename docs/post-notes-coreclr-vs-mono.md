# Notas para post: CoreCLR vs Mono en .NET MAUI

> Estas son notas de trabajo para armar un post técnico sobre el impacto del runtime elegido en el peso del APK de release en .NET MAUI Android.

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
# Compilar CoreCLR
dotnet build -f net11.0-android -c Release -p:UseMono=false

# Compilar Mono
dotnet build -f net11.0-android -c Release -p:UseMono=true
```

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

*Última actualización: 2026-07-08 — agregada Nota 4 (benchmark Skia partículas + protocolo de medición)*
