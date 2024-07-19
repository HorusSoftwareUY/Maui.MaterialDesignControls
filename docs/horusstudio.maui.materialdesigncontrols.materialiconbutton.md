# MaterialIconButton

An icon button  that reacts to touch events and follows Material Design Guidelines [See here. ](https://m3.material.io/components/icon-buttons/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

## Properties

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when button is clicked.
 This is a bindable property.

Property type: AnimationTypes<br>

| Name | Value | Description |
| --- | --: | --- |
| None | 0 | None animation |
| Fade | 1 | Fade animation |
| Scale | 2 | Scale animation |
| Custom | 3 | Custom animation |

Default value: AnimationTypes.Fade

### <a id="properties-animationparameter"/>**AnimationParameter**

Gets or sets the parameter to pass to the MaterialIconButton.Animation property.
 This is a bindable property.

Property type: [Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

Default value: Null

### <a id="properties-background"/>**Background**

Gets or sets a  that describes the background of the button.
 This is a bindable property.

Property type: Brush<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a color that describes the background color of the button.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets a color that describes the border stroke color of the button.
 This is a bindable property.

Property type: Color<br>

Remarks: This property has no effect if  is set to 0. On Android this property will not have an effect unless  is set to a non-default color.

### <a id="properties-borderwidth"/>**BorderWidth**

Gets or sets the width of the border, in device-independent units.
 This is a bindable property.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

Remarks: Set this value to a non-zero value in order to have a visible border.

### <a id="properties-busyindicatorcolor"/>**BusyIndicatorColor**

Gets or sets the  for the busy indicator.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-busyindicatorsize"/>**BusyIndicatorSize**

Gets or sets the size for the busy indicator.
 This is a bindable property.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-command"/>**Command**

Gets or sets the command to invoke when the button is activated.
 This is a bindable property.

Property type: ICommand<br>

Remarks: This property is used to associate a command with an instance of a button. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.  is controlled by the  if set.

### <a id="properties-commandparameter"/>**CommandParameter**

Gets or sets the parameter to pass to the MaterialIconButton.Command property.
 This is a bindable property.

Property type: [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: Null

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the button, in device-independent units.
 This is a bindable property.

Property type: [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when button is clicked.
 This is a bindable property.

Property type: ICustomAnimation<br>

Default value: Null

### <a id="properties-custombusyindicator"/>**CustomBusyIndicator**

Gets or sets a custom  for busy indicator.
 This is a bindable property.

Property type: View<br>

### <a id="properties-heightrequest"/>**HeightRequest**

Gets or sets the desired height override of this element.
 This is a bindable property.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

Remarks: -1

Which means the value is unset; the effective minimum height will be zero.

MaterialIconButton.HeightRequest does not immediately change the Bounds of an element; setting the MaterialIconButton.HeightRequest will change the resulting height of the element during the next layout pass.

### <a id="properties-icontintcolor"/>**IconTintColor**

Gets or sets the  for the text of the button.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-imagesource"/>**ImageSource**

Allows you to display a bitmap image on the Button.
 This is a bindable property.

Property type: ImageSource<br>

Remarks: For more options have a look at .

### <a id="properties-isbusy"/>**IsBusy**

Gets or sets if button is on busy state (executing Command).
 This is a bindable property.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

### <a id="properties-padding"/>**Padding**

Gets or sets the padding for the button.
 This is a bindable property.

Property type: Thickness<br>

### <a id="properties-shadow"/>**Shadow**

Gets or sets the shadow effect cast by the element.
 This is a bindable property.

Property type: Shadow<br>

### <a id="properties-tintcolor"/>**TintColor**

Gets or sets the  for the text of the button.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-type"/>**Type**

Gets or sets the button type according to MaterialIconButtonType enum.
 This is a bindable property.

Property type: MaterialIconButtonType<br>

| Name | Value | Description |
| --- | --: | --- |
| Filled | 0 | Filled material icon button |
| Tonal | 1 | Tonal material icon button |
| Outlined | 2 | Outlined material icon button |
| Standard | 3 | Standard material icon button |
| Custom | 4 | Custom material icon button |

Default value: MaterialIconButtonType.Filled

### <a id="properties-widthrequest"/>**WidthRequest**

Gets or sets the desired width override of this element.
 This is a bindable property.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: -1

Remarks: Which means the value is unset; the effective minimum width will be zero.

MaterialIconButton.WidthRequest does not immediately change the Bounds of an element; setting the MaterialIconButton.HeightRequest will change the resulting width of the element during the next layout pass.

## Events

### <a id="events-clicked"/>**Clicked**

Occurs when the button is clicked/tapped.

### <a id="events-pressed"/>**Pressed**

Occurs when the button is pressed.

### <a id="events-released"/>**Released**

Occurs when the button is released.
