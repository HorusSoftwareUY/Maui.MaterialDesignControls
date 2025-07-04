# MaterialPicker

A picker  let users select an option.

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialPicker → MaterialInputBase

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialPicker.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialPicker
    ItemsSource="{Binding ItemsSource}"
    TrailingIcon="picker_arrow.png"
    Placeholder="Select an option" />
```

### C# sample

```csharp
var picker = new MaterialPicker
{
    ItemsSource= ItemsSource,
    TrailingIcon="picker_arrow.png"
    Placeholder="Select an option"
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/PickerPage.xaml)

## Properties

### <a id="properties-alwaysshowlabel"/>**AlwaysShowLabel**

Gets or sets if the label is always displayed.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

<br>

### <a id="properties-background"/>**Background**

Gets or sets a  that describes the background of the input. This is a bindable property.

Property type: [Brush](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.brush)<br>

Default value: Brush

<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a color that describes the background color of the input. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurfaceVariant - Dark: MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets a color that describes the border stroke color of the input. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurfaceVariant - Dark: MaterialDarkTheme.OnSurfaceVariant

Remarks: This property has no effect if  is set to 0. On Android this property will not have an effect unless  is set to a non-default color.

<br>

### <a id="properties-borderwidth"/>**BorderWidth**

Gets or sets the width of the border, in device-independent units. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 1

Remarks: Set this value to a non-zero value in order to have a visible border.

<br>

### <a id="properties-characterspacing"/>**CharacterSpacing**

Gets or sets a value that indicates the number of device-independent units that
 should be in between characters in the text displayed by the Entry. Applies to
 Text and Placeholder.
 The number of device-independent units that should be in between characters in the text.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontTracking.BodyLarge 0.5

Remarks: To be added.

<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the input, in device-independent units. This is a bindable property.

Property type: [CornerRadius](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.cornerradius)<br>

Default value: CornerRadius(0)

<br>

### <a id="properties-erroricon"/>**ErrorIcon**

Allows you to display a trailing icon when input has error. This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Default value: null

Remarks: For more options have a look at MaterialIconButton.

<br>

### <a id="properties-focusedcommand"/>**FocusedCommand**

Gets or sets a Command to be invoked when input is Focused. This is a bindable property.

Property type: ICommand<br>

Default value: null

<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets a value that indicates whether the font for the text of this input
 is bold, italic, or neither. This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Determines whether or not the font of this entry should scale automatically according
 to the operating system settings. This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

Remarks: Typically this should always be enabled for accessibility reasons.

<br>

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets font family for input. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets font size for input. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodyLarge Tablet = 19 / Phone = 16

<br>

### <a id="properties-haserror"/>**HasError**

Gets or sets if the input has an error. This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

<br>

### <a id="properties-heightrequest"/>**HeightRequest**

Gets or sets the height request

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-horizontaltextalignment"/>**HorizontalTextAlignment**

Gets or sets the horizontal text alignment for the input. This is a bindable property.

Property type: [TextAlignment](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.textalignment)<br>

Default value:

<br>

### <a id="properties-inputtapcommand"/>**InputTapCommand**

Gets or sets the command to invoke when the input is tapped.

Property type: ICommand<br>

Default value: null

Remarks: This property is used internally, and it's recommended to avoid setting it directly.

<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets if the input is enabled or disabled. This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

<br>

### <a id="properties-isfocused"/>**IsFocused**

Gets state focused entry

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

<br>

### <a id="properties-itemdisplaypath"/>**ItemDisplayPath**

Gets or sets the property path.
 This property is used to map an object and display a property of it.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

Remarks: If it´s no defined, the control will use toString() method.

<br>

### <a id="properties-itemssource"/>**ItemsSource**

Gets or sets the source list of items to template and display.
 To be added.

Property type: [IEnumerable](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ienumerable)<br>

Default value: Null

Remarks: To be added.

<br>

### <a id="properties-label"/>**Label**

Gets or sets the text displayed as the label of the input.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-labelcolor"/>**LabelColor**

Gets or sets text color for label. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurfaceVariant - Dark: MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-labelfontfamily"/>**LabelFontFamily**

Gets or sets font family for label. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-labellinebreakmode"/>**LabelLineBreakMode**

Gets or sets line break mode for label. This is a bindable property.

Property type: [LineBreakMode](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.linebreakmode)<br>

Default value:

<br>

### <a id="properties-labelmargin"/>**LabelMargin**

Gets or sets margin for label. This is a bindable property.
 The default value is 0

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: Thickness(0)

<br>

### <a id="properties-labelpadding"/>**LabelPadding**

Gets or sets padding for label. This is a bindable property.
 The default value is 0

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: Filled: Thickness(0). Outlined: Thickness(4,1)

<br>

### <a id="properties-labelsize"/>**LabelSize**

Gets or sets font size for label. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodySmall Tablet = 15 / Phone = 12

<br>

### <a id="properties-leadingicon"/>**LeadingIcon**

Allows you to display a leading icon (bitmap image) on the input. This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Default value: null

Remarks: For more options have a look at MaterialIconButton.

<br>

### <a id="properties-leadingiconcommand"/>**LeadingIconCommand**

Gets or sets a Leading icon command. This is a bindable property.

Property type: ICommand<br>

Default value: null

<br>

### <a id="properties-leadingiconcommandparameter"/>**LeadingIconCommandParameter**

Gets or sets a Leading icon command parameter. This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: null

<br>

### <a id="properties-leadingicontintcolor"/>**LeadingIconTintColor**

Gets or sets the  for the leading button icon of the input. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurfaceVariant - Dark: MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-placeholder"/>**Placeholder**

Gets or sets the text displayed as the placeholder of the input.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-placeholdercolor"/>**PlaceholderColor**

Gets or sets text color for placeholder. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurfaceVariant - Dark: MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-placeholderfontfamily"/>**PlaceholderFontFamily**

Gets or sets font family for placeholder. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-placeholderlinebreakmode"/>**PlaceholderLineBreakMode**

Gets or sets line break mode for placeholder. This is a bindable property.

Property type: [LineBreakMode](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.linebreakmode)<br>

Default value: LineBreakMode.NoWrap

<br>

### <a id="properties-placeholdersize"/>**PlaceholderSize**

Gets or sets font size for placeholder. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodyLarge Tablet = 19 / Phone = 16

<br>

### <a id="properties-selectedindex"/>**SelectedIndex**

Gets the index of the selected item of the picker. This is a bindable
 property.
 An 0-based index representing the selected item in the list. Default is -1.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Remarks: A value of -1 represents no item selected.

<br>

### <a id="properties-selectedindexchangedcommand"/>**SelectedIndexChangedCommand**

Gets or sets an ICommand to be executed when selected index has changed. This is a bindable
 property.

Property type: ICommand<br>

<br>

### <a id="properties-selecteditem"/>**SelectedItem**

Gets or sets the selected item.
 To be added.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: Null

Remarks: To be added.

<br>

### <a id="properties-supportingcolor"/>**SupportingColor**

Gets or sets text color for supporting text. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurfaceVariant - Dark: MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-supportingfontfamily"/>**SupportingFontFamily**

Gets or sets font family for supporting text. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-supportinglinebreakmode"/>**SupportingLineBreakMode**

Gets or sets line break mode for supporting text. This is a bindable property.

Property type: [LineBreakMode](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.linebreakmode)<br>

Default value:

<br>

### <a id="properties-supportingmargin"/>**SupportingMargin**

Gets or sets margin for supporting text. This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: Thickness(16, 4)

<br>

### <a id="properties-supportingsize"/>**SupportingSize**

Gets or sets font size for supporting text. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodySmall Tablet = 15 / Phone = 12

<br>

### <a id="properties-supportingtext"/>**SupportingText**

Gets or sets the text displayed as the supporting text of the input.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-text"/>**Text**

Gets or sets the text This property cannot be changed by the user.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the  for the text of the input. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurface - Dark: MaterialDarkTheme.OnSurface

<br>

### <a id="properties-trailingicon"/>**TrailingIcon**

Allows you to display a trailing icon (bitmap image) on the input. This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Default value: null

Remarks: For more options have a look at MaterialIconButton.

<br>

### <a id="properties-trailingiconcommand"/>**TrailingIconCommand**

Gets or sets a Trailing Icon command. This is a bindable property.

Property type: ICommand<br>

Default value: null

<br>

### <a id="properties-trailingiconcommandparameter"/>**TrailingIconCommandParameter**

Gets or sets a Trailing Icon command parameter. This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: null

<br>

### <a id="properties-trailingicontintcolor"/>**TrailingIconTintColor**

Gets or sets the  for the trailing button icon of the input. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurfaceVariant - Dark: MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-type"/>**Type**

Gets or sets the input type according to MaterialInputType enum.
 This is a bindable property.

Property type: MaterialInputType<br>

| Name | Value | Description |
| --- | --: | --- |
| Filled | 0 | Filled input type |
| Outlined | 1 | Outlined input type |

Default value: MaterialInputType.Filled

<br>

### <a id="properties-unfocusedcommand"/>**UnfocusedCommand**

Gets or sets a Command to be invoked when input is Unfocused. This is a bindable property.

Property type: ICommand<br>

Default value: null

<br>

### <a id="properties-verticaltextalignment"/>**VerticalTextAlignment**

Gets or sets the vertical text alignment. This is a bindable property.

Property type: [TextAlignment](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.textalignment)<br>

Default value: null

<br>

## Known issues and pending features

* [Android] Use the colors defined in Material in the picker dialog
