# NET 11 Preview 5 — Resumen de cambios

Branch: `feature/net11preview5`

---

## Commit 1 — `fix for release on local`

### `.gitignore`
Se agregaron `google-services.json` y `GoogleService-Info.plist` al gitignore. Son los archivos de configuración de Firebase que contienen secrets/API keys y no deben commitearse.

### `Sample.csproj` — Release Android
Se agregó un bloque de propiedades que aplica solo en Release para Android, necesario para que el build local funcione con CoreCLR:

| Propiedad | Valor | Motivo |
|---|---|---|
| `AndroidLinkMode` | `None` | No stripea assemblies |
| `EmbedAssembliesIntoApk` | `true` | Embebe los dlls en el APK |
| `AndroidUseAssemblyStore` | `false` | Deshabilita el store format de assemblies |
| `RunAOTCompilation` | `false` | Desactiva la compilación AOT |

---

## Commit 2 — `initial setup, running ok (lib and sample) in .net 11`

El grueso del trabajo de migración a .NET 11.

### `Directory.Build.props` (raíz)
- `NetVersion`: `net10.0` → `net11.0`
- Android `SupportedOSPlatformVersion`: `23.0` → `24.0` (Android API 24 es el mínimo requerido por .NET 11 / API 37)

### `global.json` (raíz)
- SDK pinneado a `11.0.100-preview.5.26302.115`
- `allowPrerelease=true` porque .NET 11 todavía es preview

### `samples/Directory.Build.props` *(nuevo)*
Build props separado para la carpeta samples. Permite que la librería y el sample tengan versiones distintas si hace falta. Define `NetVersion=net11.0` y `MauiVersion=11.0.0-preview.5.26304.4`.

### `samples/Directory.Packages.props` *(nuevo)*
Overrides de versiones de paquetes NuGet específicamente para el sample en .NET 11:

| Paquete | Versión |
|---|---|
| `Microsoft.Maui.Controls` | `11.0.0-preview.5.26304.4` |
| `Microsoft.Maui.Controls.Compatibility` | `11.0.0-preview.5.26304.4` |
| `Microsoft.Maui.Core` | `11.0.0-preview.5.26304.4` |
| `CommunityToolkit.Maui` | `14.2.0` *(targets net10, compatible via NuGet fallback)* |
| `Microsoft.Extensions.Logging.Debug` | `11.0.0-preview.5.26302.115` |
| `Xamarin.AndroidX.Lifecycle.LiveData.Ktx` | `2.11.0.1` *(fix de compatibilidad con MAUI 11)* |

### `samples/global.json` *(nuevo)*
SDK pinneado al mismo preview que la raíz, pero scoped a la carpeta samples.

### `Sample.csproj`
- `ApplicationId` renombrado de `...sample` → `...sample.coreclr` para diferenciarlo de la variante Mono
- `RuntimeIdentifiers` reducido a solo 64-bit (`android-arm64;android-x64`) porque **CoreCLR no soporta 32-bit en Android**
- `UseMonoRuntime=false` explícito
- `PublishReadyToRun=false` en Release: CoreCLR activa R2R por defecto en Release, pero eso borra el flag `ILOnly` de los PEs y rompía el linker de MAUI (`Mono.Cecil` en `_LinkAssembliesNoShrink`)

### Librería `csproj`
- Se agregaron targets `net11.0-android/ios/maccatalyst` adicionales cuando `NetVersion != net11.0`, para que la librería pueda compilar tanto desde proyectos .NET 10 como .NET 11
- Se agregó `Xamarin.AndroidX.AppCompat` como dependencia en Android (requerido por .NET 11)

### `MainPage.xaml`
Se agregó el label _"Running with CoreCLR (.NET 11 preview 5)"_ en violeta para que sea visible en runtime qué motor se está usando.

### `appiconfg.svg` + `splash.svg`
Se actualizaron los assets visuales con el texto "CoreCLR", el subtítulo ".NET 11 preview 5" y gradiente violeta.

---

## Commit 3 — `improvements to manage both runtimes`

Sistema para compilar con Mono o CoreCLR mediante un flag MSBuild, sin modificar nada por defecto.

### Uso

```bash
# CoreCLR (default, sin cambios)
dotnet build

# Mono
dotnet build -p:UseMono=true
```

### `Sample.csproj` — flag `UseMono`

| Propiedad | CoreCLR (default) | Mono (`-p:UseMono=true`) |
|---|---|---|
| `ApplicationId` | `...sample.coreclr` | `...sample.mono` |
| `UseMonoRuntime` | `false` | `true` |
| `RuntimeIdentifiers` (Android) | `android-arm64;android-x64` | sin restricción (Mono soporta 32-bit) |
| `DefineConstants` | — | `USE_MONO` |
| Icono foreground | `appiconfg.svg` | `appiconfg_mono.svg` |
| Splash | `splash.svg` | `splash_mono.svg` |

> El `ApplicationId` distinto permite que **ambas variantes coexistan instaladas en el mismo dispositivo**.

### `MainPage.xaml` + `MainPage.xaml.cs`
- Se le dio `x:Name="RuntimeLabel"` al label del runtime
- En el constructor, con `#if USE_MONO` se cambia el texto a _"Running with Mono"_ y el color a celeste en tiempo de compilación

### Assets nuevos

| Archivo | Descripción |
|---|---|
| `Resources/AppIcon/appiconfg_mono.svg` | Icono con gradiente celeste y texto "Mono" |
| `Resources/Splash/splash_mono.svg` | Splash con gradiente celeste, texto "Mono" y subtítulo ".NET Mono runtime" |
