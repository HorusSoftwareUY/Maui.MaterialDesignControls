# MaterialButton

A button  that reacts to touch events and follows Material Design Guidelines .

Namespace: HorusStudio.Maui.MaterialDesignControls

## Properties

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when button is clicked.
 The default value is [AnimationTypes.Fade](./horusstudio.maui.materialdesigncontrols.animationtypes.md#fade).
 This is a bindable property.

Property Type: [AnimationTypes](./horusstudio.maui.materialdesigncontrols.animationtypes.md)<br>

Default: The default value is [AnimationTypes.Fade](./horusstudio.maui.materialdesigncontrols.animationtypes.md#fade).

### <a id="properties-animationparameter"/>**AnimationParameter**

Gets or sets the parameter to pass to the [MaterialButton.Animation](./horusstudio.maui.materialdesigncontrols.materialbutton.md#animation) property.
 The default value is null.
 This is a bindable property.

Property Type: [Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### <a id="properties-background"/>**Background**

Gets or sets a  that describes the background of the button. This is a bindable property.

Property Type: Brush<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a color that describes the background color of the button. This is a bindable property.

Property Type: Color<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets a color that describes the border stroke color of the button. This is a bindable property.

Property Type: Color<br>

**Remarks:**

This property has no effect if  is set to 0. On Android this property will not have an effect unless  is set to a non-default color.

### <a id="properties-borderwidth"/>**BorderWidth**

Gets or sets the width of the border, in device-independent units. This is a bindable property.

Property Type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

**Remarks:**

Set this value to a non-zero value in order to have a visible border.

### <a id="properties-busyindicatorcolor"/>**BusyIndicatorColor**

Gets or sets the  for the busy indicator.
 This is a bindable property.

Property Type: Color<br>

### <a id="properties-busyindicatorsize"/>**BusyIndicatorSize**

Gets or sets the size for the busy indicator.
 This is a bindable property.

Property Type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-characterspacing"/>**CharacterSpacing**

Gets or sets the spacing between each of the characters of [MaterialButton.Text](./horusstudio.maui.materialdesigncontrols.materialbutton.md#text) when displayed on the button.
 This is a bindable property.

Property Type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-command"/>**Command**

Gets or sets the command to invoke when the button is activated. This is a bindable property.

Property Type: ICommand<br>

**Remarks:**

This property is used to associate a command with an instance of a button. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.  is controlled by the  if set.

### <a id="properties-commandparameter"/>**CommandParameter**

Gets or sets the parameter to pass to the [MaterialButton.Command](./horusstudio.maui.materialdesigncontrols.materialbutton.md#command) property.
 The default value is null. This is a bindable property.

Property Type: [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### <a id="properties-contentlayout"/>**ContentLayout**

Gets or sets an object that controls the position of the button image and the spacing between the button's image and the button's text.
 This is a bindable property.

Property Type: ButtonContentLayout<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the button, in device-independent units. This is a bindable property.

Property Type: [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when button is clicked.
 The default value is null.
 This is a bindable property.

Property Type: [ICustomAnimation](./horusstudio.maui.materialdesigncontrols.icustomanimation.md)<br>

### <a id="properties-custombusyindicator"/>**CustomBusyIndicator**

Gets or sets a custom  for busy indicator.
 This is a bindable property.

Property Type: View<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets a value that indicates whether the font for the text of this button is bold, italic, or neither.
 This is a bindable property.

Property Type: FontAttributes<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Determines whether or not the font of this entry should scale automatically according to the operating system settings. Default value is true.
 This is a bindable property.

Property Type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

**Remarks:**

Typically this should always be enabled for accessibility reasons.

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the text of this entry. This is a bindable property.

Property Type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets the size of the font for the text of this entry. This is a bindable property.

Property Type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-heightrequest"/>**HeightRequest**

Gets or sets the desired height override of this element. This is a bindable property.

Property Type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

**Remarks:**

The default value is -1, which means the value is unset; the effective minimum height will be zero.

[MaterialButton.HeightRequest](./horusstudio.maui.materialdesigncontrols.materialbutton.md#heightrequest) does not immediately change the Bounds of an element; setting the [MaterialButton.HeightRequest](./horusstudio.maui.materialdesigncontrols.materialbutton.md#heightrequest) will change the resulting height of the element during the next layout pass.

### <a id="properties-icontintcolor"/>**IconTintColor**

Gets or sets the  for the text of the button. This is a bindable property.

Property Type: Color<br>

### <a id="properties-imagesource"/>**ImageSource**

Allows you to display a bitmap image on the Button. This is a bindable property.

Property Type: ImageSource<br>

**Remarks:**

For more options have a look at .

### <a id="properties-isbusy"/>**IsBusy**

Gets or sets if button is on busy state (executing Command).
 The default value is false.
 This is a bindable property.

Property Type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-linebreakmode"/>**LineBreakMode**

Determines how [MaterialButton.Text](./horusstudio.maui.materialdesigncontrols.materialbutton.md#text) is shown when the length is overflowing the size of this button.
 This is a bindable property.

Property Type: LineBreakMode<br>

### <a id="properties-padding"/>**Padding**

Gets or sets the padding for the button. This is a bindable property.

Property Type: Thickness<br>

### <a id="properties-shadow"/>**Shadow**

Gets or sets the shadow effect cast by the element. This is a bindable property.
 This is a bindable property.

Property Type: Shadow<br>

### <a id="properties-text"/>**Text**

Gets or sets the text displayed as the content of the button.
 The default value is null. This is a bindable property.

Property Type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

Changing the text of a button will trigger a layout cycle.

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the  for the text of the button. This is a bindable property.

Property Type: Color<br>

### <a id="properties-textdecorations"/>**TextDecorations**

Gets or sets [MaterialButton.TextDecorations](./horusstudio.maui.materialdesigncontrols.materialbutton.md#textdecorations) for the text of the button.
 This is a bindable property.

Property Type: TextDecorations<br>

### <a id="properties-texttransform"/>**TextTransform**

Applies text transformation to the [MaterialButton.Text](./horusstudio.maui.materialdesigncontrols.materialbutton.md#text) displayed on this button.
 This is a bindable property.

Property Type: TextTransform<br>

### <a id="properties-tintcolor"/>**TintColor**

Gets or sets the  for the text of the button. This is a bindable property.

Property Type: Color<br>

### <a id="properties-type"/>**Type**

Gets or sets the button type according to [MaterialButtonType](./horusstudio.maui.materialdesigncontrols.materialbuttontype.md) enum.
 The default value is [MaterialButtonType.Filled](./horusstudio.maui.materialdesigncontrols.materialbuttontype.md#filled). This is a bindable property.

Property Type: [MaterialButtonType](./horusstudio.maui.materialdesigncontrols.materialbuttontype.md)<br>

## Events

### <a id="events-clicked"/>**Clicked**

Occurs when the button is clicked/tapped.

### <a id="events-focused"/>**Focused**

Occurs when the button is focused.

### <a id="events-pressed"/>**Pressed**

Occurs when the button is pressed.

### <a id="events-released"/>**Released**

Occurs when the button is released.

### <a id="events-unfocused"/>**Unfocused**

Occurs when the button is unfocused.
