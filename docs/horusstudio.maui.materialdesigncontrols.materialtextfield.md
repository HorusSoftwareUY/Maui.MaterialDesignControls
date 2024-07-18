# MaterialTextField

Use a text field when someone needs to enter text into a UI, such as filling in contact or payment information.

Namespace: HorusStudio.Maui.MaterialDesignControls

## Properties

### <a id="properties-background"/>**Background**

Gets or sets a  that describes the background of the input.
 This is a bindable property.

Property type: Brush<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a color that describes the background color of the input.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets a color that describes the border stroke color of the input.
 This is a bindable property.

Property type: Color<br>

Remarks: This property has no effect if  is set to 0. On Android this property will not have an effect unless  is set to a non-default color.

### <a id="properties-borderwidth"/>**BorderWidth**

Gets or sets the width of the border, in device-independent units.
 This is a bindable property.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

Remarks: Set this value to a non-zero value in order to have a visible border.

### <a id="properties-characterspacing"/>**CharacterSpacing**

Gets or sets a value that indicates the number of device-independent units that
 should be in between characters in the text displayed by the Entry. Applies to
 Text and Placeholder.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

Remarks: The number of device-independent units that should be in between characters in the text.

### <a id="properties-clearbuttonvisibility"/>**ClearButtonVisibility**

Determines the behavior of the clear text button on this entry.
 This is a bindable property.

Property type: ClearButtonVisibility<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the input, in device-independent units.
 This is a bindable property.

Property type: CornerRadius<br>

### <a id="properties-cursorcolor"/>**CursorColor**

Gets or sets a color of the caret indicator.

Property type: Color<br>

Remarks: This Property only works on iOS and 'Android' 29 or later

### <a id="properties-cursorposition"/>**CursorPosition**

Gets or sets input's cursor position.
 This is a bindable property.

Property type: [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### <a id="properties-focusedcommand"/>**FocusedCommand**

Gets or sets a focused command.
 This is a bindable property.

Property type: ICommand<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets a value that indicates whether the font for the text of this entry
 is bold, italic, or neither.
 This is a bindable property.

Property type: FontAttributes<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Determines whether or not the font of this entry should scale automatically according
 to the operating system settings.
 This is a bindable property.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Remarks: Typically this should always be enabled for accessibility reasons.

Default value: True

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the input.
 This is a bindable property.

Property type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets the font size for the input.
 This is a bindable property.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-haserror"/>**HasError**

Gets or sets if the input has an error.
 This is a bindable property.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-heightrequest"/>**HeightRequest**

Gets or sets the height request

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-horizontaltextalignment"/>**HorizontalTextAlignment**

Gets or sets the horizontal text alignment for the input.
 This is a bindable property.

Property type: TextAlignment<br>

### <a id="properties-inputtapcommand"/>**InputTapCommand**

Gets or sets the command to invoke when the input is tapped.

Property type: ICommand<br>

Remarks: This property is used internally and it's recommended to avoid setting it directly.

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets if the input is enabled or diabled.
 This is a bindable property.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-ispassword"/>**IsPassword**

Gets or sets if the input is password.
 This is a bindable property.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-isreadonly"/>**IsReadOnly**

Gets or sets a value that indicates whether user should be prevented from modifying the text. Default is false.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Remarks: The IsReadonly property does not alter the visual appearance of the control, unlike the IsEnabled property that also changes the visual appearance of the control

Default value: If true, user cannot modify text. Else, false.

### <a id="properties-isspellcheckenabled"/>**IsSpellCheckEnabled**

Gets or sets a value that controls whether spell checking is enabled.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Remarks: True if spell checking is enabled. Otherwise false.

### <a id="properties-istextpredictionenabled"/>**IsTextPredictionEnabled**

Determines whether text prediction and automatic text correction is enabled.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

### <a id="properties-keyboard"/>**Keyboard**

Gets or sets input's keyboard.
 This is a bindable property.

Property type: Keyboard<br>

### <a id="properties-label"/>**Label**

Gets or sets the text displayed as the label of the input.
 This is a bindable property.

Property type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

### <a id="properties-labelcolor"/>**LabelColor**

Gets or sets the label color.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-labelfontfamily"/>**LabelFontFamily**

Gets or sets the label font family.
 This is a bindable property.

Property type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-labellinebreakmode"/>**LabelLineBreakMode**

Gets or sets the label line break mode. This is a bindable property.

Property type: LineBreakMode<br>

Default value:

### <a id="properties-labelmargin"/>**LabelMargin**

Gets or sets the label margin. This is a bindable property.

Property type: Thickness<br>

Default value: 0

### <a id="properties-labelsize"/>**LabelSize**

Gets or sets the label size.
 This is a bindable property.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-leadingiconcommand"/>**LeadingIconCommand**

Gets or sets a Leading icon command.
 This is a bindable property.

Property type: ICommand<br>

### <a id="properties-leadingiconcommandparameter"/>**LeadingIconCommandParameter**

Gets or sets a Leading icon command parameter.
 This is a bindable property.

Property type: [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### <a id="properties-leadingiconsource"/>**LeadingIconSource**

Allows you to display a leading icon (bitmap image) on the input.
 This is a bindable property.

Property type: ImageSource<br>

Remarks: For more options have a look at [MaterialIconButton](./horusstudio.maui.materialdesigncontrols.materialiconbutton.md).

### <a id="properties-leadingicontintcolor"/>**LeadingIconTintColor**

Gets or sets the  for the leading button icon of the input.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-maxlength"/>**MaxLength**

Gets or sets input's max length.
 This is a bindable property.

Property type: [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### <a id="properties-placeholder"/>**Placeholder**

Gets or sets the text displayed as the placeholder of the input.
 This is a bindable property.

Property type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

### <a id="properties-placeholdercolor"/>**PlaceholderColor**

Gets or sets the place holder color for the input.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-returncommand"/>**ReturnCommand**

Gets or sets the command to run when the user presses the return key, either
 physically or on the on-screen keyboard.
 This is a bindable property.

Property type: ICommand<br>

### <a id="properties-returncommandparameter"/>**ReturnCommandParameter**

Gets or sets the parameter object for the Microsoft.Maui.Controls.Entry.ReturnCommand
 that can be used to provide extra information.
 This is a bindable property.

Property type: [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### <a id="properties-returntype"/>**ReturnType**

Determines what the return key on the on-screen keyboard should look like.
 This is a bindable property.

Property type: ReturnType<br>

### <a id="properties-showtrailingicononlyonerror"/>**ShowTrailingIconOnlyOnError**

Gets or sets if show the trailing icon only on error.
 This is a bindable property.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

### <a id="properties-supportingfontfamily"/>**SupportingFontFamily**

Gets or sets the font family for the input.
 This is a bindable property.

Property type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-supportingfontsize"/>**SupportingFontSize**

Gets or sets the font size for the input.
 This is a bindable property.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-supportinglinebreakmode"/>**SupportingLineBreakMode**

Gets or sets the supporting line break mode. This is a bindable property.

Property type: LineBreakMode<br>

Default value:

### <a id="properties-supportingmargin"/>**SupportingMargin**

Gets or sets the label margin.
 This is a bindable property.

Property type: Thickness<br>

Default value: Thickness(16, 4)

### <a id="properties-supportingtext"/>**SupportingText**

Gets or sets the text displayed as the supporting text of the input.
 This is a bindable property.

Property type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

### <a id="properties-supportingtextcolor"/>**SupportingTextColor**

Gets or sets the supporting text color.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-text"/>**Text**

Gets or sets the text displayed as the content of the input.
 This is a bindable property.

Property type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null.

### <a id="properties-textchangedcommand"/>**TextChangedCommand**

Gets or sets input's text changed command.
 This is a bindable property.

Property type: ICommand<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the  for the text of the input.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-texttransform"/>**TextTransform**

Gets or sets input's texttransform.
 This is a bindable property.

Property type: TextTransform<br>

### <a id="properties-trailingiconcommand"/>**TrailingIconCommand**

Gets or sets a Trailing Icon command.
 This is a bindable property.

Property type: ICommand<br>

### <a id="properties-trailingiconcommandparameter"/>**TrailingIconCommandParameter**

Gets or sets a Trailing Icon command parameter.
 This is a bindable property.

Property type: [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### <a id="properties-trailingiconsource"/>**TrailingIconSource**

Allows you to display a trailing icon (bitmap image) on the input.
 This is a bindable property.

Property type: ImageSource<br>

Remarks: For more options have a look at [MaterialIconButton](./horusstudio.maui.materialdesigncontrols.materialiconbutton.md).

### <a id="properties-trailingicontintcolor"/>**TrailingIconTintColor**

Gets or sets the  for the trailing button icon of the input.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-type"/>**Type**

Gets or sets the input type according to [MaterialInputType](./horusstudio.maui.materialdesigncontrols.materialinputtype.md) enum.
 This is a bindable property.

Property type: [MaterialInputType](./horusstudio.maui.materialdesigncontrols.materialinputtype.md)<br>

| Name | Value | Description |
| --- | --: | --- |
| Filled | 0 | Filled |
| Outlined | 1 | Outlined |

Default value: [MaterialInputType.Filled](./horusstudio.maui.materialdesigncontrols.materialinputtype.md#filled)

### <a id="properties-unfocusedcommand"/>**UnfocusedCommand**

Gets or sets a unfocused command.
 This is a bindable property.

Property type: ICommand<br>

### <a id="properties-verticaltextalignment"/>**VerticalTextAlignment**

Gets or sets the vertical text alignment.This is a bindable property.

Property type: TextAlignment<br>
