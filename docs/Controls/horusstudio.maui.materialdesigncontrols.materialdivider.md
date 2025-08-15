# MaterialDivider

Dividers group content in lists or other containers and follow Material Design Guidelines. [See more](https://m3.material.io/components/divider/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialDivider → [BoxView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.boxview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialDivider.jpg)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialDivider
       Color="{Binding DividerColor}"/>
```

### C# sample

```csharp
var divider = new MaterialDivider()
{
    Color = Colors.Black
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/DividerPage.xaml)

## Properties

### <a id="properties-color"/>**Color**

Gets or sets the color of the divider.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light: MaterialLightTheme.OutlineVariant - Dark: MaterialDarkTheme.OutlineVariant

<br>

### <a id="properties-heightrequest"/>**HeightRequest**

Gets or sets the desired height override of this element.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 1

<br>
