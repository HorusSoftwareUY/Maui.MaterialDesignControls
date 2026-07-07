# Guía de migración a .NET 11 (MAUI)

Basada en la experiencia real migrando `Maui.MaterialDesignControls` de .NET 10 a .NET 11 preview 5.

---

## 1. Requisitos previos

- Visual Studio 2022 17.14+ o VS Code con extensión MAUI
- .NET 11 SDK preview instalado: https://dotnet.microsoft.com/download/dotnet/11.0
- Workloads actualizados:
  ```bash
  dotnet workload update
  dotnet workload install maui
  ```

---

## 2. Actualizar el SDK (`global.json`)

```json
{
  "sdk": {
    "allowPrerelease": true,
    "version": "11.0.100-preview.5.26302.115"
  }
}
```

> ⚠️ `allowPrerelease: true` es obligatorio mientras .NET 11 sea preview.

---

## 3. Actualizar `Directory.Build.props`

```xml
<PropertyGroup>
  <NetVersion>net11.0</NetVersion>
</PropertyGroup>

<!-- Android API mínimo sube a 24 en .NET 11 -->
<Choose>
  <When Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'android'">
    <PropertyGroup>
      <SupportedOSPlatformVersion>24.0</SupportedOSPlatformVersion>
    </PropertyGroup>
  </When>
</Choose>
```

> ⚠️ `.NET 11 / Android API 37 requiere mínimo API 24`. Si tenías `23.0` o menor, el build fallará.

---

## 4. Actualizar versiones de paquetes NuGet

En `Directory.Packages.props` (o en cada `.csproj`):

```xml
<PackageVersion Update="Microsoft.Maui.Controls"               Version="11.0.0-preview.5.26304.4" />
<PackageVersion Update="Microsoft.Maui.Controls.Compatibility"  Version="11.0.0-preview.5.26304.4" />
<PackageVersion Update="Microsoft.Maui.Core"                    Version="11.0.0-preview.5.26304.4" />
<PackageVersion Update="Microsoft.Extensions.Logging.Debug"     Version="11.0.0-preview.5.26302.115" />

<!-- Fix de compatibilidad con MAUI 11 -->
<PackageVersion Update="Xamarin.AndroidX.Lifecycle.LiveData.Ktx" Version="2.11.0.1" />
```

> 💡 `CommunityToolkit.Maui 14.2.0` (targets net10) funciona via NuGet framework fallback, no requiere actualización todavía.

---

## 5. Actualizar el `.csproj` de la app

### Android: solo 64-bit con CoreCLR

```xml
<PropertyGroup Condition="'$(TargetFramework)' == '$(NetVersion)-android'">
  <!-- CoreCLR NO soporta 32-bit en Android -->
  <RuntimeIdentifiers>android-arm64;android-x64</RuntimeIdentifiers>
  <UseMonoRuntime>false</UseMonoRuntime>
</PropertyGroup>
```

### Release Android: deshabilitar R2R

```xml
<PropertyGroup Condition="'$(Configuration)' == 'Release' and $(TargetFramework.Contains('-android'))">
  <AndroidLinkMode>None</AndroidLinkMode>
  <EmbedAssembliesIntoApk>true</EmbedAssembliesIntoApk>
  <AndroidUseAssemblyStore>false</AndroidUseAssemblyStore>
  <RunAOTCompilation>false</RunAOTCompilation>
  <!-- CoreCLR activa R2R por defecto en Release.
       R2R borra el flag ILOnly de los PEs y rompe Mono.Cecil en _LinkAssembliesNoShrink -->
  <PublishReadyToRun>false</PublishReadyToRun>
</PropertyGroup>
```

---

## 6. Actualizar librería/NuGet multi-target (si aplica)

Si tu proyecto es una librería que debe ser compatible con .NET 10 y .NET 11:

```xml
<TargetFrameworks>$(NetVersion)-android;$(NetVersion)-ios;$(NetVersion)-maccatalyst</TargetFrameworks>

<!-- Agregar targets net11 cuando se compila desde un proyecto net10 -->
<TargetFrameworks Condition="'$(NetVersion)' != 'net11.0'">
  $(TargetFrameworks);net11.0-android;net11.0-ios;net11.0-maccatalyst
</TargetFrameworks>
```

### Dependencia nueva requerida en Android

```xml
<ItemGroup Condition="$([MSBuild]::GetTargetPlatformIdentifier('$(TargetFramework)')) == 'android'">
  <PackageReference Include="Xamarin.AndroidX.AppCompat" />
</ItemGroup>
```

---

## 7. Firebase / archivos de configuración

Si usás Firebase, **nunca commitear** los archivos de config:

```gitignore
# Firebase config files (contain secrets - add real files manually)
google-services.json
GoogleService-Info.plist
```

---

## 8. Bonus: flag para compilar con Mono vs CoreCLR

Útil si querés probar ambos runtimes sin tocar el proyecto:

```xml
<!-- En el csproj -->
<UseMono Condition="'$(UseMono)' == ''">false</UseMono>

<ApplicationId Condition="'$(UseMono)' != 'true'">com.company.app.coreclr</ApplicationId>
<ApplicationId Condition="'$(UseMono)' == 'true'">com.company.app.mono</ApplicationId>

<DefineConstants Condition="'$(UseMono)' == 'true'">$(DefineConstants);USE_MONO</DefineConstants>

<PropertyGroup Condition="'$(TargetFramework)' == '$(NetVersion)-android'">
  <RuntimeIdentifiers Condition="'$(UseMono)' != 'true'">android-arm64;android-x64</RuntimeIdentifiers>
  <UseMonoRuntime>$(UseMono)</UseMonoRuntime>
</PropertyGroup>
```

```bash
dotnet build                  # CoreCLR (default)
dotnet build -p:UseMono=true  # Mono
```

> ApplicationId distinto permite instalar ambas variantes en el mismo dispositivo simultáneamente.

---

## 9. Checklist rápida

- [ ] `global.json` → SDK `11.0.x`, `allowPrerelease: true`
- [ ] `Directory.Build.props` → `NetVersion=net11.0`, Android min API `24.0`
- [ ] Paquetes MAUI → versión `11.0.0-preview.x`
- [ ] Android `RuntimeIdentifiers` → solo 64-bit (`arm64`, `x64`)
- [ ] `UseMonoRuntime=false` explícito en Android
- [ ] `PublishReadyToRun=false` en Release Android
- [ ] Librería multi-target → agregar `net11.0-*` targets
- [ ] `Xamarin.AndroidX.AppCompat` en Android
- [ ] `google-services.json` / `GoogleService-Info.plist` en `.gitignore`

---

## Referencias

- [.NET 11 release notes](https://github.com/dotnet/core/tree/main/release-notes/11.0)
- [.NET MAUI 11 release notes](https://github.com/dotnet/maui/releases)
- [CoreCLR on Android](https://devblogs.microsoft.com/dotnet/dotnet-android-coreclr/)
