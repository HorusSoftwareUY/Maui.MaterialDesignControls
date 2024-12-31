# MaterialDesignControls Plugin for .Net MAUI

<img src="resources/material_design_icon.jpg" width="128">

MaterialDesignControls Plugin for .Net MAUI, provides a collection of .Net MAUI controls that follow the [Material Design Guidelines](https://m3.material.io/)

## Demo

[TODO:VIDEO_DEMO]

## Content table
- [Setup](#setup)
- [Platform support](#platform-support)
- [API Usage](#api-usage)
- [Controls](#controls)
- [Styles](#styles)
- [Sample app](#sample-app)
- [Developed by](#developed-by)
- [Contributions](#contributions)
- [License](#license)

## Setup
* Available on NuGet: [TODO:ADD_NUGET_LINK]
* Install into your .NET MAUI project.

## Platform support
MaterialDesignControls Plugin provides cross-platform controls for:
* .NET 7
* Android
* iOS
* tvOS (upcoming)
* macOS (upcoming)
* Windows (upcoming)

## API Usage
You must configure MaterialDesignControls in your `MauiProgram` when you `Build()` your `MauiApp`:
```C#
var builder = MauiApp.CreateBuilder();
builder
    .UseMauiApp<App>()
    .ConfigureMaterialDesignControls();           
```

You must add this namespace to your XAML files to use Material Design controls:
```XML
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
```

## Controls
* [MaterialBadge](docs/Controls/horusstudio.maui.materialdesigncontrols.materialbadge.md)
* [MaterialButton](docs/Controls/horusstudio.maui.materialdesigncontrols.materialbutton.md)
* [MaterialCard](docs/Controls/horusstudio.maui.materialdesigncontrols.materialcard.md)
* [MaterialCheckbox](docs/Controls/horusstudio.maui.materialdesigncontrols.materialcheckbox.md)
* [MaterialChips](docs/Controls/horusstudio.maui.materialdesigncontrols.materialchips.md)
* [MaterialDivider](docs/Controls/horusstudio.maui.materialdesigncontrols.materialdivider.md)
* [MaterialIconButton](docs/Controls/horusstudio.maui.materialdesigncontrols.materialiconbutton.md)
* [MaterialLabel](docs/Controls/horusstudio.maui.materialdesigncontrols.materiallabel.md)
* [MaterialProgressIndicator](docs/Controls/horusstudio.maui.materialdesigncontrols.materialprogressindicator.md)
* [MaterialRadioButton](docs/Controls/horusstudio.maui.materialdesigncontrols.materialradiobutton.md)
* [MaterialRating](docs/Controls/horusstudio.maui.materialdesigncontrols.materialrating.md)
* [MaterialTimePicker](docs/Controls/horusstudio.maui.materialdesigncontrols.materialtimepicker.md)
* [MaterialViewButton](docs/Controls/horusstudio.maui.materialdesigncontrols.materialviewbutton.md)

### Coming soon
* MaterialChipsGroup
* MaterialDatePicker
* MaterialEditor
* MaterialEntry
* MaterialCodeEntry
* MaterialField
* MaterialPicker
* MaterialDoublePicker
* MaterialSelection
* MaterialSlider
* MaterialSegmented
* MaterialFloatingButton
* MaterialSwitch
* MaterialTopAppBar
* MaterialNavigationDrawer
* MaterialCustomControl
* MaterialSearch
* MaterialSnackBar
* MaterialDialog
* MaterialBottomSheet

## Styles
You can override default colors, font sizes, font families, and other styles to apply to all Material Design controls:
* [MaterialAnimation](docs/Styles/horusstudio.maui.materialdesigncontrols.materialanimation.md)
* [MaterialLightTheme](docs/Styles/horusstudio.maui.materialdesigncontrols.materiallighttheme.md)
* [MaterialDarkTheme](docs/Styles/horusstudio.maui.materialdesigncontrols.materialdarktheme.md)
* [MaterialElevation](docs/Styles/horusstudio.maui.materialdesigncontrols.materialelevation.md)
* [MaterialFontFamily](docs/Styles/horusstudio.maui.materialdesigncontrols.materialfontfamily.md)
* [MaterialFontSize](docs/Styles/horusstudio.maui.materialdesigncontrols.materialfontsize.md)
* [MaterialFontTracking](docs/Styles/horusstudio.maui.materialdesigncontrols.materialfonttracking.md)

You must configure default styles in your MauiProgram when you Build() your MauiApp:
```C#
builder.Services.ConfigureMaterial();
```
```C#
private static IServiceCollection ConfigureMaterial(this IServiceCollection services)
{
    MaterialFontFamily.Medium = FontMedium;
    MaterialFontFamily.Regular = FontRegular;
    MaterialFontFamily.Default = MaterialFontFamily.Regular;

    MaterialLightTheme.Primary = Color.FromArgb("#6750A4");
    MaterialLightTheme.Secondary = Color.FromArgb("#625B71");

    MaterialDarkTheme.Primary = Color.FromArgb("#D0BCFF");
    MaterialDarkTheme.Secondary = Color.FromArgb("#CCC2DC");

    return services;
}
```

## Sample app

[Sample app here.](samples/HorusStudio.Maui.MaterialDesignControls.Sample)

## Developed by
<a href="http://horus.com.uy"><img src="https://assets-global.website-files.com/64a7016392b0b7da3a8604e3/64a7016392b0b7da3a8604ed_horus.svg" width="128"></a>


## Contributions
Contributions are welcome! If you find a bug want a feature added please report it.

If you want to contribute code please file an issue, create a branch, and file a pull request.

## License 
MIT License - see LICENSE.txt
