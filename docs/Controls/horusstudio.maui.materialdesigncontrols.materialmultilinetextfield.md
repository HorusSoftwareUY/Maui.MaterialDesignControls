# MaterialMultilineTextField

Multiline text fields let users enter multiline text into a UI and follow Material Design Guidelines. [See more](https://m3.material.io/components/text-fields/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialMultilineTextField → MaterialInputBase

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialMultilineTextField.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialMultilineTextField
    Placeholder="Enter text here" />
```

### C# sample

```csharp
var textField = new MaterialMultilineTextField
{
    Placeholder = "Enter text here"
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/MultilineTextFieldPage.xaml)

## Properties

### <a id="properties-alwaysshowlabel"/>**AlwaysShowLabel**

Gets or sets if the label is always displayed.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

<br>

### <a id="properties-automationid"/>**AutomationId**

Gets or sets a value that allows the automation framework to find and interact with this element.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Remarks: This value may only be set once on an element.
 
 When set on this control, the AutomationId is also used as a base identifier for its internal elements:
 - The main input control (e.g., Entry, Editor, Picker, etc.) uses the same AutomationId value.
 - The hint label uses the identifier "{AutomationId}_Hint".
 - The supporting text label uses the identifier "{AutomationId}_SupportingText".
 - The placeholder text label uses the identifier "{AutomationId}_Placeholder".
 - The leading icon button uses the identifier "{AutomationId}_LeadingIcon".
 - The trailing icon button uses the identifier "{AutomationId}_TrailingIcon".
 
 This convention allows automated tests and accessibility tools to consistently locate all subelements of the control.

<br>

### <a id="properties-autosize"/>**AutoSize**

Gets or sets a value that controls whether the editor will change size to accommodate
 input as the user enters it.
 Whether the editor will change size to accommodate input as the user enters it.

Property type: [EditorAutoSizeOption](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.editorautosizeoption)<br>

Remarks: Automatic resizing is turned off by default.

<br>

### <a id="properties-background"/>**Background**

Gets or sets a Brush that describes the background of the input. This is a bindable property.

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

Remarks: This property has no effect if IBorderElement.BorderWidth is set to 0. On Android this property will not have an effect unless VisualElement.BackgroundColor is set to a non-default color.

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

Remarks: To be added.

<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the input, in device-independent units. This is a bindable property.

Property type: [CornerRadius](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.cornerradius)<br>

Default value: CornerRadius(0)

<br>

### <a id="properties-cursorcolor"/>**CursorColor**

Gets or sets a color of the caret indicator.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.Primary - Dark: MaterialDarkTheme.Primary

Remarks: This Property only works on iOS and 'Android' 29 or later

<br>

### <a id="properties-cursorposition"/>**CursorPosition**

Gets or sets input's cursor position. This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

<br>

### <a id="properties-erroranimation"/>**ErrorAnimation**

Gets or sets a custom animation to be executed when the control has an error.
 This is a bindable property.

Property type: IErrorAnimation<br>

Remarks: When this property is set, the ErrorAnimationType property is ignored.

<br>

### <a id="properties-erroranimationtype"/>**ErrorAnimationType**

Gets or sets the animation type to be executed when the control has an error.
 This is a bindable property.

Property type: ErrorAnimationTypes<br>

| Name | Value | Description |
| --- | --: | --- |
| None | 0 | None: no animation runs. |
| Shake | 1 | Shake: Represents an animation that simulates a "shake" effect by moving the target element back and forth along the X-axis. |
| Heart | 2 | Breath: represents an animation that performs a "heartbeat" effect by scaling the target element in a pulsating manner. |
| Jump | 3 | Jump: represents an animation that creates a "jump" effect by translating the target element along the Y-axis. |

Default value: ErrorAnimationTypes.Shake

Remarks: This property will only be considered if the ErrorAnimation property is null.

<br>

### <a id="properties-erroricon"/>**ErrorIcon**

Allows you to display a trailing icon when input has error. This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Default value: null

Remarks: For more options see MaterialIconButton.

<br>

### <a id="properties-focusedcommand"/>**FocusedCommand**

Gets or sets a Command to be invoked when input is Focused. This is a bindable property.

Property type: ICommand<br>

Default value: null

<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets a value that indicates whether the font for the text of this input is bold, italic, or neither.
 This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Determines whether or not the font of this entry should scale automatically according
 to the operating system settings. Default value is true. This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

Remarks: Typically this should always be enabled for accessibility reasons.

<br>

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets font family for input.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets font size for input.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodyLarge: Tablet = 19 / Phone = 16

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

Gets or sets the horizontal text alignment for the input.
 This is a bindable property.

Property type: [TextAlignment](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.textalignment)<br>

Default value: TextAlignment.Start

<br>

### <a id="properties-inputtapcommand"/>**InputTapCommand**

Gets or sets the command to invoke when the input is tapped.

Property type: ICommand<br>

Default value: null

Remarks: This property is used internally, and it's recommended to avoid setting it directly.

<br>

### <a id="properties-internalborderwidth"/>**InternalBorderWidth**

This property is for internal use by the control. BorderWidth property should be used instead.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-internaleditor"/>**InternalEditor**

Internal implementation of the Editor control.

Property type: [Editor](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.editor)<br>

Remarks: This property can affect the internal behavior of this control. Use only if you fully understand the potential impact.

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

### <a id="properties-isreadonly"/>**IsReadOnly**

Gets or sets a value that indicates whether user should be prevented from modifying the text.
 If true, user cannot modify text. Else, false.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

Remarks: The IsReadonly property does not alter the visual appearance of the control, unlike the IsEnabled property that also changes the visual appearance of the control

<br>

### <a id="properties-isspellcheckenabled"/>**IsSpellCheckEnabled**

Gets or sets a value that controls whether spell checking is enabled.
 true if spell checking is enabled. Otherwise false.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Remarks: To be added.

<br>

### <a id="properties-istextpredictionenabled"/>**IsTextPredictionEnabled**

Determines whether text prediction and automatic text correction is enabled.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

<br>

### <a id="properties-keyboard"/>**Keyboard**

Gets or sets input's keyboard. This is a bindable property.

Property type: [Keyboard](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.keyboard)<br>

<br>

### <a id="properties-label"/>**Label**

Gets or sets the text displayed as the label of the input.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-labelcolor"/>**LabelColor**

Gets or sets text color for label.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurfaceVariant - Dark: MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-labelfontfamily"/>**LabelFontFamily**

Gets or sets font family for label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-labellinebreakmode"/>**LabelLineBreakMode**

Gets or sets line break mode for label. This is a bindable property.

Property type: [LineBreakMode](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.linebreakmode)<br>

Default value: LineBreakMode.NoWrap

<br>

### <a id="properties-labelmargin"/>**LabelMargin**

Gets or sets margin for label.
 This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: Thickness(0)

<br>

### <a id="properties-labelpadding"/>**LabelPadding**

Gets or sets padding for label.
 This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: Filled: Thickness(0). Outlined: Thickness(4,1)

<br>

### <a id="properties-labelsize"/>**LabelSize**

Gets or sets font size for label.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodySmall: Tablet = 15 / Phone = 12

<br>

### <a id="properties-leadingicon"/>**LeadingIcon**

Allows you to display a leading icon (bitmap image) on the input. This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Default value: null

Remarks: For more options see MaterialIconButton.

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

Gets or sets the color for the leading button icon of the input.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurfaceVariant - Dark: MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-maxlength"/>**MaxLength**

Gets or sets input's max length. This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

<br>

### <a id="properties-placeholder"/>**Placeholder**

Gets or sets the text displayed as the placeholder of the input.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-placeholdercolor"/>**PlaceholderColor**

Gets or sets text color for placeholder.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurfaceVariant - Dark: MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-placeholderfontfamily"/>**PlaceholderFontFamily**

Gets or sets font family for placeholder.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-placeholderlinebreakmode"/>**PlaceholderLineBreakMode**

Gets or sets line break mode for placeholder.
 This is a bindable property.

Property type: [LineBreakMode](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.linebreakmode)<br>

Default value: LineBreakMode.NoWrap

<br>

### <a id="properties-placeholdersize"/>**PlaceholderSize**

Gets or sets font size for placeholder.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodyLarge: Tablet = 19 / Phone = 16

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

Default value: LineBreakMode.NoWrap

<br>

### <a id="properties-supportingmargin"/>**SupportingMargin**

Gets or sets margin for supporting text. This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: Thickness(16, 4)

<br>

### <a id="properties-supportingsize"/>**SupportingSize**

Gets or sets font size for supporting text. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodySmall: Tablet = 15 / Phone = 12

<br>

### <a id="properties-supportingtext"/>**SupportingText**

Gets or sets the text displayed as the supporting text of the input.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-text"/>**Text**

Gets or sets the text displayed as the content of the input.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-textchangedcommand"/>**TextChangedCommand**

Gets or sets input's text changed command. This is a bindable property.

Property type: ICommand<br>

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the color for the text of the input.
 This is a bindable property.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurface - Dark: MaterialDarkTheme.OnSurface

<br>

### <a id="properties-texttransform"/>**TextTransform**

Gets or sets input's texttransform. This is a bindable property.

Property type: [TextTransform](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.texttransform)<br>

<br>

### <a id="properties-trailingicon"/>**TrailingIcon**

Allows you to display a trailing icon (bitmap image) on the input. This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Default value: null

Remarks: For more options see MaterialIconButton.

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

Gets or sets the color for the trailing button icon of the input.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurfaceVariant - Dark: MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-type"/>**Type**

Gets or sets the input type according to MaterialInputType.
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

<br>

## Known issues and pending features

* [iOS] FontAttributes doesn't work
 * HeightRequest doesn't work, it is not respected when writing and making a text change.
 * VerticalTextAlignment doesn't work
