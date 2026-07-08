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

## Ideas para desarrollar en el post

- [ ] Agregar comparativa real de tamaños (antes/después de cada optimización)
- [ ] Mencionar el impacto de usar AAB (Android App Bundle) vs APK para distribución en Play Store
- [ ] Comparar rendimiento en runtime: ¿vale la pena el peso extra de CoreCLR?
- [ ] Hablar de cuándo conviene cada runtime según el tipo de app
- [ ] Referencias: docs oficiales de Microsoft sobre runtimes en MAUI

---

*Última actualización: 2026-07-08*
