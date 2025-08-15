# MaterialProgressIndicator

Progress indicators show the status of a process and follow Material Design Guidelines. [See more](https://m3.material.io/components/progress-indicators/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialProgressIndicator → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialProgressIndictor.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialProgressIndicator
        Type="Linear"
        IndicatorColor="DarkBlue"
        TrackColor="LightBlue"/>
```

### C# sample

```csharp
var progressIndicator = new MaterialProgressIndicator()
{
    Type = MaterialProgressIndicatorType.Linear,
    IndicatorColor = Colors.Blue,
    TrackColor = Colors.LightBlue
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/ProgressIndicatorPage.xaml)

## Properties

### <a id="properties-heightrequest"/>**HeightRequest**

Gets or sets the height of the progress indicator.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-indicatorcolor"/>**IndicatorColor**

Gets or sets the color for the active indicator of the progress indicator.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light: MaterialLightTheme.Primary - Dark: MaterialDarkTheme.Primary

<br>

### <a id="properties-isvisible"/>**IsVisible**

Gets or sets if progress indicator is visible.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: true

<br>

### <a id="properties-trackcolor"/>**TrackColor**

Gets or sets the track color of the progress indicator.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light: MaterialLightTheme.SurfaceContainerHighest - Dark: MaterialDarkTheme.SurfaceContainerHighest

Remarks: This property will not have an effect unless MaterialProgressIndicator.Type is set to MaterialProgressIndicatorType.Linear.

<br>

### <a id="properties-type"/>**Type**

Gets or sets the progress indicator type.
 This is a bindable property.

Property type: MaterialProgressIndicatorType<br>

| Name | Value | Description |
| --- | --: | --- |
| Circular | 0 | Circular |
| Linear | 1 | Linear |

Default value: MaterialProgressIndicatorType.Circular

<br>

### <a id="properties-widthrequest"/>**WidthRequest**

Gets or sets the width of the progress indicator.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>
