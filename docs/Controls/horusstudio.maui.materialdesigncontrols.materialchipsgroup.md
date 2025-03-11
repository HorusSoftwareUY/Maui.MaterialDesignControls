# MaterialChipsGroup

Chips group facilitate the selection of one or more options within a group, optimizing space usage effectively [see here.](https://m3.material.io/components/chips/overview)

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialChipsGroup → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignPlugin/develop/screenshots/MaterialChips.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesign;assembly=HorusStudio.Maui.MaterialDesign"

<material:MaterialChipsGroup
       IsMultipleSelection="True"
       ItemsSource="{Binding Colors}"
       LabelText="Colors *"
       SelectedItems="{Binding SelectedColors}"
       SupportingText="Please select at least 4 colors"/>
```

### C# sample

```csharp
var chips = new MaterialChipsGroup
{
    IsMultipleSelection = true,
    ItemsSource = Colors,
    LabelText = "Colors *",
    SelectedItems = SelectedColors,
    SupportingText="Please select at least 4 colors"
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesign.Sample/Pages/ChipsPage.xaml)

## Properties

### <a id="properties-align"/>**Align**

Gets or sets the horizontal alignment for the chips.
 This is a bindable property.

Property type: Align<br>

| Name | Value | Description |
| --- | --: | --- |
| Start | 0 | Start |
| Center | 1 | Center |
| End | 2 | End |

Default value: Align.Start

<br>

### <a id="properties-animateerror"/>**AnimateError**

Gets or sets if the error animation is enabled for the ChipsGroup.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

<br>

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when is clicked.
 This is a bindable property.

Property type: AnimationTypes<br>

| Name | Value | Description |
| --- | --: | --- |
| None | 0 | None |
| Fade | 1 | Fade |
| Scale | 2 | Scale |
| Custom | 3 | Custom |

Default value: AnimationTypes.Fade

<br>

### <a id="properties-animationparameter"/>**AnimationParameter**

Gets or sets the parameter to pass to the MaterialChipsGroup.Animation property.
 This is a bindable property.

Property type: [Nullable&lt;Double&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

Default value: null

<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets the background color for the Chips.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.SurfaceContainerLow - Dark = MaterialDarkTheme.SurfaceContainerLow

<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets the border color for the Chips.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Outline - Dark = MaterialDarkTheme.Outline

<br>

### <a id="properties-chipsflexlayoutpercentagebasis"/>**ChipsFlexLayoutPercentageBasis**

Gets or sets the basis for the ChipsGroup.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 0.0

<br>

### <a id="properties-chipsheightrequest"/>**ChipsHeightRequest**

Gets or sets the height for the Chips.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 32.0

<br>

### <a id="properties-chipspadding"/>**ChipsPadding**

Gets or sets the padding for the Chips.
 This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: Thickness(16,0)

<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the Chips.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 8.0

<br>

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family of text for the Chips.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets the font size of text for the Chips.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.LabelLarge / Tablet = 17 / Phone = 14

<br>

### <a id="properties-horizontalspacing"/>**HorizontalSpacing**

Gets or sets the horizontal spacing between the chips.
 This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: 4

<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets the state when the Chips is enabled.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

<br>

### <a id="properties-ismultipleselection"/>**IsMultipleSelection**

Gets or sets if the multi selection is enabled for the ChipsGroup.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

<br>

### <a id="properties-itemssource"/>**ItemsSource**

Gets or sets the source of the items for the ChipsGroup.
 This is a bindable property.

Property type: [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)<br>

Default value: null

<br>

### <a id="properties-labelsize"/>**LabelSize**

Gets or sets the font size of text for the ChipsGroup.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodySmall / Tablet = 15 / Phone = 12

<br>

### <a id="properties-labeltext"/>**LabelText**

Gets or sets the text for the ChipsGroup.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-labeltextcolor"/>**LabelTextColor**

Gets or sets the font color of text for the ChipsGroup.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Text - Dark = MaterialDarkTheme.Text

<br>

### <a id="properties-padding"/>**Padding**

Gets or sets the padding for the ChipsGroup.
 This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: Thickness(12,0)

<br>

### <a id="properties-propertypath"/>**PropertyPath**

Gets or sets the property path.
 This property is used to map an object and display a property of it.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

Remarks: If it´s no defined, the control will use toString() method.

<br>

### <a id="properties-selecteditem"/>**SelectedItem**

Gets or sets the selected item for the ChipsGroup.
 This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: null

<br>

### <a id="properties-selecteditems"/>**SelectedItems**

Gets or sets the selected items for the ChipsGroup.
 This is a bindable property.

Property type: [IList](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ilist)<br>

Default value: null

Remarks: This property needs to be initialized from the implementation. It cannot be null.

<br>

### <a id="properties-supportingsize"/>**SupportingSize**

Gets or sets the font size of supporting text for the ChipsGroup.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodySmall / Tablet = 15 / Phone = 12

<br>

### <a id="properties-supportingtext"/>**SupportingText**

Gets or sets the supporting text for the ChipsGroup.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-supportingtextcolor"/>**SupportingTextColor**

Gets or sets the font color of supporting text for the ChipsGroup.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Error - Dark = MaterialDarkTheme.Error

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the font color of text for the Chips.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.OnSurfaceVariant - Dark = MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-verticalspacing"/>**VerticalSpacing**

Gets or sets the vertical spacing between the chips.
 This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: 4

<br>

## Known issues and pending features

* For the SelectedItems to be updated correctly they must be initialized. Finding a way to make it work even when the list starts out null
