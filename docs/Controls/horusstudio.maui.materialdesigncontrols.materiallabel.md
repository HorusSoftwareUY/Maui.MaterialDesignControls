# MaterialLabel

Labels help make writing legible and beautiful, and follow Material Design Guidelines. [See more](https://m3.material.io/styles/typography/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialLabel → [Label](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.label)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialLabel.jpg)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialLabel 
       Type="HeadlineLarge"
       Text="Headline large"/>
```

### C# sample

```csharp

var label = new MaterialLabel()
{
 Type = LabelTypes.HeadlineLarge,
 Text = "This Material Label"
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/LabelPage.xaml)

## Properties

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-fontfamilymedium"/>**FontFamilyMedium**

Gets or sets the medium font family for the label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-fontfamilyregular"/>**FontFamilyRegular**

Gets or sets the regular font family for the label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the color for the text of the label.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-type"/>**Type**

Gets or sets the label type according to LabelTypes.
 This property internally handles FontFamily, CharacterSpacing and FontSize properties.

Property type: LabelTypes<br>

| Name | Value | Description |
| --- | --: | --- |
| DisplayLarge | 0 | Tablet: 80, Phone: 57 |
| DisplayMedium | 1 | Tablet: 62, Phone: 45 |
| DisplaySmall | 2 | Tablet: 50, Phone: 36 |
| HeadlineLarge | 3 | Tablet: 44, Phone: 32 |
| HeadlineMedium | 4 | Tablet: 38, Phone: 28 |
| HeadlineSmall | 5 | Tablet: 32, Phone: 24 |
| TitleLarge | 6 | Tablet: 26, Phone: 22 |
| TitleMedium | 7 | Tablet: 19, Phone: 16 |
| TitleSmall | 8 | Tablet: 17, Phone: 14 |
| BodyLarge | 9 | Tablet: 19, Phone: 16 |
| BodyMedium | 10 | Tablet: 17, Phone: 14 |
| BodySmall | 11 | Tablet: 15, Phone: 12 |
| LabelLarge | 12 | Tablet: 17, Phone: 14 |
| LabelMedium | 13 | Tablet: 15, Phone: 12 |
| LabelSmall | 14 | Tablet: 14, Phone: 11 |

Default value: LabelTypes.BodyMedium

<br>

## Known issues and pending features

* [iOS] FontAttributes and SupportingFontAttributes don't work (MAUI issue).
