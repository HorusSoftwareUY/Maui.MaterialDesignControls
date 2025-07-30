# MaterialButton

A button  that reacts to touch events and follows Material Design Guidelines [](https://m3.material.io/components/buttons/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialButton → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialButton.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialButton
    Type="Elevated"
    Text="Confirm"
    Command="{Binding ButtonCommand}"
    IsBusy="{Binding ButtonCommand.IsRunning}"/>
```

### C# sample

```csharp
var button = new MaterialButton
{
    Type = MaterialButtonType.Filled,
    Text = "Save",
    Command = ButtonCommand,
    IsBusy = ButtonCommand.IsRunning
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/ButtonPage.xaml)

## Properties

### <a id="properties-applyicontintcolor"/>**ApplyIconTintColor**

Gets or sets the if the icon applies the tint color.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: true

<br>

### <a id="properties-background"/>**Background**

Gets or sets a  that describes the background of the button.
 This is a bindable property.

Property type: [Brush](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.brush)<br>

<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a color that describes the background color of the button.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.Primary - Dark: MaterialDarkTheme.Primary

<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets a color that describes the border stroke color of the button.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Remarks: This property has no effect if  is set to 0. On Android this property will not have an effect unless  is set to a non-default color.

<br>

### <a id="properties-borderwidth"/>**BorderWidth**

Gets or sets the width of the border, in device-independent units.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Remarks: Set this value to a non-zero value in order to have a visible border.

<br>

### <a id="properties-busyindicatorcolor"/>**BusyIndicatorColor**

Gets or sets the  for the busy indicator.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-busyindicatorsize"/>**BusyIndicatorSize**

Gets or sets the size for the busy indicator.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-characterspacing"/>**CharacterSpacing**

Gets or sets the spacing between each of the characters of MaterialButton.Text when displayed on the button.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-command"/>**Command**

Gets or sets the command to invoke when the button is activated.
 This is a bindable property.

Property type: ICommand<br>

Remarks: This property is used to associate a command with an instance of a button. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.  is controlled by the  if set.

<br>

### <a id="properties-commandparameter"/>**CommandParameter**

Gets or sets the parameter to pass to the MaterialButton.Command property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: null

<br>

### <a id="properties-contentlayout"/>**ContentLayout**

Gets or sets an object that controls the position of the button image and the spacing between the button's image and the button's text.
 This is a bindable property.

Property type: [ButtonContentLayout](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.button.buttoncontentlayout)<br>

<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the button, in device-independent units.
 This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

<br>

### <a id="properties-custombusyindicator"/>**CustomBusyIndicator**

Gets or sets a custom  for busy indicator.
 This is a bindable property.

Property type: [View](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.view)<br>

<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets a value that indicates whether the font for the text of this button is bold, italic, or neither.
 This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Determines whether or not the font of this entry should scale automatically according to the operating system settings.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

Remarks: Typically this should always be enabled for accessibility reasons.

<br>

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the text of this entry.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets the size of the font for the text of this entry.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-heightrequest"/>**HeightRequest**

Gets or sets the desired height override of this element.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: -1

Remarks:

- <para>which means the value is unset; the effective minimum height will be zero.</para>

- <para>
  <see cref="P:HorusStudio.Maui.MaterialDesignControls.MaterialButton.HeightRequest" /> does not immediately change the Bounds of an element; setting the <see cref="P:HorusStudio.Maui.MaterialDesignControls.MaterialButton.HeightRequest" /> will change the resulting height of the element during the next layout pass.</para>

<br>

### <a id="properties-iconsize"/>**IconSize**

Gets or sets the icon size.
 This is a bindable property.

Property type: [Size](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.size)<br>

Default value: Size.Zero.

Remarks: With a default value of Size.Zero, the icon will be handled automatically by each platform's native behavior. If a size is specified, that size will be applied to the icon on all platforms.

<br>

### <a id="properties-icontintcolor"/>**IconTintColor**

Gets or sets the  for the icon of the button.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-imagesource"/>**ImageSource**

Allows you to display a bitmap image on the Button.
 This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Remarks: For more options have a look at .

<br>

### <a id="properties-internalbutton"/>**InternalButton**

Internal implementation of the  control.

Property type: [Button](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.button)<br>

Remarks: This property can affect the internal behavior of this control. Use only if you fully understand the potential impact.

<br>

### <a id="properties-internalicontintcolor"/>**InternalIconTintColor**

This property is for internal use by the control. The IconTintColor property should be used instead.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-isbusy"/>**IsBusy**

Gets or sets if button is on busy state (executing Command).
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

<br>

### <a id="properties-linebreakmode"/>**LineBreakMode**

Determines how MaterialButton.Text is shown when the length is overflowing the size of this button.
 This is a bindable property.

Property type: [LineBreakMode](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.linebreakmode)<br>

<br>

### <a id="properties-padding"/>**Padding**

Gets or sets the padding for the button.
 This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

<br>

### <a id="properties-shadow"/>**Shadow**

Gets or sets the shadow effect cast by the element.
 This is a bindable property.

Property type: [Shadow](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.shadow)<br>

<br>

### <a id="properties-text"/>**Text**

Gets or sets the text displayed as the content of the button.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

Remarks: Changing the text of a button will trigger a layout cycle.

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the  for the text of the button.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnPrimary - Dark: MaterialDarkTheme.OnPrimary

<br>

### <a id="properties-textdecorations"/>**TextDecorations**

Gets or sets MaterialButton.TextDecorations for the text of the button.
 This is a bindable property.

Property type: [TextDecorations](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.textdecorations)<br>

<br>

### <a id="properties-texttransform"/>**TextTransform**

Applies text transformation to the MaterialButton.Text displayed on this button.
 This is a bindable property.

Property type: [TextTransform](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.texttransform)<br>

<br>

### <a id="properties-touchanimation"/>**TouchAnimation**

Gets or sets a custom touch animation to be executed when button is clicked.
 This is a bindable property.

Property type: ITouchAnimation<br>

Default value: null

<br>

### <a id="properties-touchanimationtype"/>**TouchAnimationType**

Gets or sets an animation to be executed when button is clicked.
 This is a bindable property.

Property type: TouchAnimationTypes<br>

| Name | Value | Description |
| --- | --: | --- |
| None | 0 | None: no animation runs. |
| Fade | 1 | Fade: Represents an animation that simulates a "fade" effect by changing the opacity over the target element. |
| Scale | 2 | Scale: Represents an animation that simulates a "sink" or "sunken" effect by scaling the target element. |
| Bounce | 3 | Bounce: Represents an animation that simulates a "sink" or "sunken" effect with a "bounce" effect when the user releases the target element. |

Default value: TouchAnimationTypes.Fade.

<br>

### <a id="properties-type"/>**Type**

Gets or sets the button type according to MaterialButtonType enum.
 This is a bindable property.

Property type: MaterialButtonType<br>

| Name | Value | Description |
| --- | --: | --- |
| Elevated | 0 | Elevated button |
| Filled | 1 | Filled button |
| Tonal | 2 | Filled tonal button |
| Outlined | 3 | Outlined button |
| Text | 4 | Text button |
| Custom | 5 | Custom button |

Default value: MaterialButtonType.Filled

<br>

## Events

### <a id="events-clicked"/>**Clicked**

Occurs when the button is clicked/tapped.

<br>

### <a id="events-focused"/>**Focused**

Occurs when the button is focused.

<br>

### <a id="events-pressed"/>**Pressed**

Occurs when the button is pressed.

<br>

### <a id="events-released"/>**Released**

Occurs when the button is released.

<br>

### <a id="events-unfocused"/>**Unfocused**

Occurs when the button is unfocused.

<br>

## Known issues and pending features

* [iOS] IconTintColor doesn't react to VisualStateManager changes.
 * Shadow doesn't react to VisualStateManager changes.
 * ContentLayout is buggy.
 * Add default Material behavior for pressed state on default styles (v2).
 * [iOS] FontAttributes and SupportingFontAttributes don't work (MAUI issue)
