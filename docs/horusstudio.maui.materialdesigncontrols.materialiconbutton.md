# MaterialIconButton

An icon button  that reacts to touch events and follows Material Design Guidelines [](https://m3.material.io/components/icon-buttons/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialIconButton → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

## Properties

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when button is clicked.
 The default value is AnimationTypes.Fade.
 This is a bindable property.

Property type: AnimationTypes<br>

| Name | Value | Description |
| --- | --: | --- |

<br>

### <a id="properties-animationparameter"/>**AnimationParameter**

Gets or sets the parameter to pass to the MaterialIconButton.Animation property.
 The default value is null.
 This is a bindable property.

Property type: [Nullable&lt;Double&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

<br>

### <a id="properties-background"/>**Background**

Gets or sets a  that describes the background of the button. This is a bindable property.

Property type: [Brush](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.brush)<br>

<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a color that describes the background color of the button. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets a color that describes the border stroke color of the button. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Remarks: This property has no effect if  is set to 0. On Android this property will not have an effect unless  is set to a non-default color.

<br>

### <a id="properties-borderwidth"/>**BorderWidth**

Gets or sets the width of the border, in device-independent units. This is a bindable property.

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

### <a id="properties-command"/>**Command**

Gets or sets the command to invoke when the button is activated. This is a bindable property.

Property type: ICommand<br>

Remarks: This property is used to associate a command with an instance of a button. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.  is controlled by the  if set.

<br>

### <a id="properties-commandparameter"/>**CommandParameter**

Gets or sets the parameter to pass to the MaterialIconButton.Command property.
 The default value is null. This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the button, in device-independent units. This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

<br>

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when button is clicked.
 The default value is null.
 This is a bindable property.

Property type: ICustomAnimation<br>

<br>

### <a id="properties-custombusyindicator"/>**CustomBusyIndicator**

Gets or sets a custom  for busy indicator.
 This is a bindable property.

Property type: [View](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.view)<br>

<br>

### <a id="properties-heightrequest"/>**HeightRequest**

Gets or sets the desired height override of this element. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Remarks:

- <para>The default value is -1, which means the value is unset; the effective minimum height will be zero.</para>

- <para>
  <see cref="P:HorusStudio.Maui.MaterialDesignControls.MaterialIconButton.HeightRequest" /> does not immediately change the Bounds of an element; setting the <see cref="P:HorusStudio.Maui.MaterialDesignControls.MaterialIconButton.HeightRequest" /> will change the resulting height of the element during the next layout pass.</para>

<br>

### <a id="properties-icontintcolor"/>**IconTintColor**

Gets or sets the  for the text of the button. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-imagesource"/>**ImageSource**

Allows you to display a bitmap image on the Button. This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Remarks: For more options have a look at .

<br>

### <a id="properties-isbusy"/>**IsBusy**

Gets or sets if button is on busy state (executing Command).
 The default value is false.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

<br>

### <a id="properties-padding"/>**Padding**

Gets or sets the padding for the button. This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

<br>

### <a id="properties-shadow"/>**Shadow**

Gets or sets the shadow effect cast by the element. This is a bindable property.

Property type: [Shadow](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.shadow)<br>

<br>

### <a id="properties-tintcolor"/>**TintColor**

Gets or sets the  for the text of the button. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-type"/>**Type**

Gets or sets the button type according to MaterialIconButtonType enum.
 The default value is MaterialIconButtonType.Filled. This is a bindable property.

Property type: MaterialIconButtonType<br>

| Name | Value | Description |
| --- | --: | --- |

<br>

### <a id="properties-widthrequest"/>**WidthRequest**

Gets or sets the desired width override of this element. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Remarks:

- <para>The default value is -1, which means the value is unset; the effective minimum width will be zero.</para>

- <para>
  <see cref="P:HorusStudio.Maui.MaterialDesignControls.MaterialIconButton.WidthRequest" /> does not immediately change the Bounds of an element; setting the <see cref="P:HorusStudio.Maui.MaterialDesignControls.MaterialIconButton.HeightRequest" /> will change the resulting width of the element during the next layout pass.</para>

<br>

## Events

### <a id="events-clicked"/>**Clicked**

Occurs when the button is clicked/tapped.

<br>

### <a id="events-pressed"/>**Pressed**

Occurs when the button is pressed.

<br>

### <a id="events-released"/>**Released**

Occurs when the button is released.

<br>
