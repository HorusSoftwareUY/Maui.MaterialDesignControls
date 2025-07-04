# MaterialSnackbarConfig

User-defined configuration to display a MaterialSnackbar

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialSnackbarConfig → [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialSnackbar.gif)

### C# sample

```csharp
IMaterialSnackbar _snackbar = ...
var config = new MaterialSnackbarConfig("Lorem ipsum dolor sit amet")
{
    LeadingIcon = new MaterialSnackbarConfig.IconConfig("info.png", () => Console.WriteLine("Leading icon tapped!"))),
    TrailingIcon = new MaterialSnackbarConfig.IconConfig("ic_close.png", () => Console.WriteLine("Trailing icon tapped!")))
};
_snackbar.Show(config);
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SnackbarPage.xaml)

## Properties

### <a id="properties-action"/>**Action**

Gets or sets configuration for custom action, if available.

Property type: ActionConfig<br>

Default value: null

<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a color that describes the background color of snackbar.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.InverseSurface - Dark: MaterialDarkTheme.InverseSurface

<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets corner radius for snackbar

Property type: [Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>

Default value: 8

<br>

### <a id="properties-defaultactioncolor"/>**DefaultActionColor**

Action text  to be applied by default to every MaterialSnackbar that doesn't set one

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.InversePrimary - Dark: MaterialDarkTheme.InversePrimary

<br>

### <a id="properties-defaultactionsize"/>**DefaultActionSize**

Action font size to be applied by default to every MaterialSnackbar that doesn't set one

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodyMedium

<br>

### <a id="properties-defaultactiontextdecorations"/>**DefaultActionTextDecorations**

Action text decorations to be applied by default to every MaterialSnackbar that doesn't set one

Property type: [TextDecorations](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.textdecorations)<br>

Default value: TextDecorations.None

<br>

### <a id="properties-defaultbackgroundcolor"/>**DefaultBackgroundColor**

Background  to be applied by default to every MaterialSnackbar that doesn't set one

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.InverseSurface - Dark: MaterialDarkTheme.InverseSurface

<br>

### <a id="properties-defaultcornerradius"/>**DefaultCornerRadius**

Corner radius to be applied by default to every MaterialSnackbar that doesn't set one

Property type: [Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>

Default value: 8

<br>

### <a id="properties-defaultduration"/>**DefaultDuration**

Duration applied by default to every MaterialSnackbar that doesn't set one

Property type: [TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan)<br>

Default value: 3 seconds

<br>

### <a id="properties-defaultfontsize"/>**DefaultFontSize**

Text font size to be applied by default to every MaterialSnackbar that doesn't set one

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodyMedium

<br>

### <a id="properties-defaulticoncolor"/>**DefaultIconColor**

Icon  to be applied by default to every MaterialSnackbar that doesn't set one

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.InverseOnSurface - Dark: MaterialDarkTheme.InverseOnSurface

<br>

### <a id="properties-defaulticonsize"/>**DefaultIconSize**

Icon size to be applied by default to every MaterialSnackbar that doesn't set one

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: 24

<br>

### <a id="properties-defaultmargin"/>**DefaultMargin**

Margin to be applied by default to every MaterialSnackbar that doesn't set one

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: 24

<br>

### <a id="properties-defaultpadding"/>**DefaultPadding**

Padding to be applied by default to every MaterialSnackbar that doesn't set one

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: 16

<br>

### <a id="properties-defaultposition"/>**DefaultPosition**

Position to be applied by default to every MaterialSnackbar that doesn't set one

Property type: MaterialSnackbarPosition<br>

| Name | Value | Description |
| --- | --: | --- |
| Bottom | 0 | Display at bottom of screen |
| Top | 1 | Display at top of screen |

Default value: MaterialSnackbarPosition.Bottom

<br>

### <a id="properties-defaultspacing"/>**DefaultSpacing**

Spacing between components to be applied by default to every MaterialSnackbar that doesn't set one

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: 16

<br>

### <a id="properties-defaulttextcolor"/>**DefaultTextColor**

Text  to be applied by default to every MaterialSnackbar that doesn't set one

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.InverseOnSurface - Dark: MaterialDarkTheme.InverseOnSurface

<br>

### <a id="properties-duration"/>**Duration**

Gets or sets time that snackbar will be displayed.

Property type: [TimeSpan](https://learn.microsoft.com/en-us/dotnet/api/system.timespan)<br>

Default value: 3 seconds

<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets text size for snackbar.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodyMedium

<br>

### <a id="properties-leadingicon"/>**LeadingIcon**

Gets or sets configuration for leading icon, if available.

Property type: IconConfig<br>

Default value: null

<br>

### <a id="properties-margin"/>**Margin**

Gets or sets snackbar margin.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: 24

<br>

### <a id="properties-message"/>**Message**

Gets text to be displayed on snackbar.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-ondismissed"/>**OnDismissed**

Action to be executed when snackbar is dismissed.

Property type: [Action](https://learn.microsoft.com/en-us/dotnet/api/system.action)<br>

Default value: null

<br>

### <a id="properties-padding"/>**Padding**

Gets or sets snackbar padding.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: 16

<br>

### <a id="properties-position"/>**Position**

Gets or sets snackbar position on screen.

Property type: MaterialSnackbarPosition<br>

| Name | Value | Description |
| --- | --: | --- |
| Bottom | 0 | Display at bottom of screen |
| Top | 1 | Display at top of screen |

Default value: MaterialSnackbarPosition.Bottom

<br>

### <a id="properties-spacing"/>**Spacing**

Gets or sets space between snackbar components.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: 16

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets a color for text on snackbar.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.InverseOnSurface - Dark: MaterialDarkTheme.InverseOnSurface

<br>

### <a id="properties-trailingicon"/>**TrailingIcon**

Gets or sets configuration for trailing icon, if available.

Property type: IconConfig<br>

Default value: null

<br>

## Known issues and pending features

* Add FontFamily configuration
