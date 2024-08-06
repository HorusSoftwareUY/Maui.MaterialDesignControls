# MaterialBadge

Badges show notifications, counts, or status information on navigation items and icons [see here.](https://m3.material.io/components/badges/overview)

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialBadge → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialBadge.jpg)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialBadge
       Text="badge"/>
```

### C# sample

```csharp
var badgeSmall = new MaterialBadge()
{
    Type = MaterialBadgeType.Small, 
    Text = "Badge small"
};
```

## Properties

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a color that describes the background color of the badge. This is a bindable property.
 
            Theme: Light: MaterialLightTheme.Error - Dark: MaterialDarkTheme.Error

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the Badge, in device-independent units. This is a bindable property.

Property type: [CornerRadius](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.cornerradius)<br>

Default value: 8

<br>

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the text of this badge. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets the size of the font for the text of this badge. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.LabelSmall / Tablet: 14 - Phone: 11

Remarks: The font size may be affected by the following cases:

- <para>Badge type is small, the font size not change.</para>

- <para>Badge type is Large, the font is changed.</para>

<br>

### <a id="properties-padding"/>**Padding**

Gets or sets the padding for the Badge. This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: 16,0

<br>

### <a id="properties-text"/>**Text**

Gets or sets the text displayed as the content of the badge. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: Empty

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the  for the text of the Badge. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light: MaterialLightTheme.OnError - Dark: MaterialDarkTheme.OnError

Remarks: The text color may be affected by the following cases:

- <para>Badge type is small, the text color is not defined.</para>

- <para>Badge type is Large, the text color is defined.</para>

<br>

### <a id="properties-type"/>**Type**

Gets or sets the badge type according to MaterialBadgeType enum. This is a bindable property.

Property type: MaterialBadgeType<br>

| Name | Value | Description |
| --- | --: | --- |
| Small | 0 | Small Badge |
| Large | 1 | Large Badge |

Default value: MaterialBadgeType.Large

<br>

## Events
