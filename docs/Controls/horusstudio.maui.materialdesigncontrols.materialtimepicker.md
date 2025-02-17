# MaterialTimePicker

A time picker  Time pickers let users select a time. They typically appear in forms and dialogs.

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialTimePicker → MaterialInputBase

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialTimePicker.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialTimePicker
   Placeholder="Creation time" />
```

### C# sample

```csharp
var timePicker = new MaterialTimePicker
{
    Placeholder="Creation time"
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/TimePickerPage.xaml)

## Properties

### <a id="properties-background"/>**Background**

Gets or sets a  that describes the background of the input. This is a bindable property.

Property type: [Brush](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.brush)<br>

<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a color that describes the background color of the input. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets a color that describes the border stroke color of the input. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Remarks: This property has no effect if  is set to 0. On Android this property will not have an effect unless  is set to a non-default color.

<br>

### <a id="properties-borderwidth"/>**BorderWidth**

Gets or sets the width of the border, in device-independent units. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

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

<br>

### <a id="properties-focusedcommand"/>**FocusedCommand**

Gets or sets a focused command. This is a bindable property.

Property type: ICommand<br>

<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets a value that indicates whether the font for the text of this entry
 is bold, italic, or neither. This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

Default value: Null

<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Determines whether or not the font of this entry should scale automatically according
 to the operating system settings. Default value is true. This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

Remarks: Typically this should always be enabled for accessibility reasons.

<br>

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the input. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets the font size for the input. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-format"/>**Format**

The format of the time to display to the user. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
A valid time format string.

Default value: null

Remarks: Format string is the same is passed to DateTime.ToString (string format).

<br>

### <a id="properties-haserror"/>**HasError**

Gets or sets if the input has an error. This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

<br>

### <a id="properties-horizontaltextalignment"/>**HorizontalTextAlignment**

Gets or sets the horizontal text alignment for the input. This is a bindable property.

Property type: [TextAlignment](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.textalignment)<br>

<br>

### <a id="properties-inputtapcommand"/>**InputTapCommand**

Gets or sets the command to invoke when the input is tapped.

Property type: ICommand<br>

Remarks: This property is used internally and it's recommended to avoid setting it directly.

<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets if the input is enabled or diabled. This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

<br>

### <a id="properties-label"/>**Label**

Gets or sets the text displayed as the label of the input.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-labelcolor"/>**LabelColor**

Gets or sets the label color. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-labelfontfamily"/>**LabelFontFamily**

Gets or sets the label font family. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-labellinebreakmode"/>**LabelLineBreakMode**

Gets or sets the label line break mode. This is a bindable property.

Property type: [LineBreakMode](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.linebreakmode)<br>

Default value:

<br>

### <a id="properties-labelmargin"/>**LabelMargin**

Gets or sets the label margin. This is a bindable property.
 The default value is 0

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

<br>

### <a id="properties-labelsize"/>**LabelSize**

Gets or sets the label size. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-leadingiconcommand"/>**LeadingIconCommand**

Gets or sets a Leading icon command. This is a bindable property.

Property type: ICommand<br>

<br>

### <a id="properties-leadingiconcommandparameter"/>**LeadingIconCommandParameter**

Gets or sets a Leading icon command parameter. This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

<br>

### <a id="properties-leadingiconsource"/>**LeadingIconSource**

Allows you to display a leading icon (bitmap image) on the input. This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Remarks: For more options have a look at MaterialIconButton.

<br>

### <a id="properties-leadingicontintcolor"/>**LeadingIconTintColor**

Gets or sets the  for the leading button icon of the input. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-placeholder"/>**Placeholder**

Gets or sets the text displayed as the placeholder of the input.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-placeholdercolor"/>**PlaceholderColor**

Gets or sets the place holder color for the input. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-showtrailingicononlyonerror"/>**ShowTrailingIconOnlyOnError**

Gets or sets if show the trailing icon only on error. This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: false

<br>

### <a id="properties-supportingfontfamily"/>**SupportingFontFamily**

Gets or sets the font family for the input. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-supportingfontsize"/>**SupportingFontSize**

Gets or sets the font size for the input. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-supportinglinebreakmode"/>**SupportingLineBreakMode**

Gets or sets the supporting line break mode. This is a bindable property.

Property type: [LineBreakMode](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.linebreakmode)<br>

Default value:

<br>

### <a id="properties-supportingmargin"/>**SupportingMargin**

Gets or sets the label margin. This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: Thickness(16, 4)

<br>

### <a id="properties-supportingtext"/>**SupportingText**

Gets or sets the text displayed as the supporting text of the input.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-supportingtextcolor"/>**SupportingTextColor**

Gets or sets the supporting text color. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-text"/>**Text**

Gets or sets the text displayed as the content of the input. This property cannot be changed by the user.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the  for the text of the input. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-time"/>**Time**

Gets or sets the displayed time. This is a bindable property.
 
The  displayed in the TimePicker.

Property type: [Nullable&lt;TimeSpan&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

Default value: null

Remarks: To be added.

<br>

### <a id="properties-trailingiconcommand"/>**TrailingIconCommand**

Gets or sets a Trailing Icon command. This is a bindable property.

Property type: ICommand<br>

<br>

### <a id="properties-trailingiconcommandparameter"/>**TrailingIconCommandParameter**

Gets or sets a Trailing Icon command parameter. This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

<br>

### <a id="properties-trailingiconsource"/>**TrailingIconSource**

Allows you to display a trailing icon (bitmap image) on the input. This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Remarks: For more options have a look at MaterialIconButton.

<br>

### <a id="properties-trailingicontintcolor"/>**TrailingIconTintColor**

Gets or sets the  for the trailing button icon of the input. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

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

Gets or sets a unfocused command. This is a bindable property.

Property type: ICommand<br>

<br>

## Known issues and pending features

* [iOS] Font attributes doesn´t work
 * [iOS] Horizontal Text Aligment doesn´t work when there is a date selected
