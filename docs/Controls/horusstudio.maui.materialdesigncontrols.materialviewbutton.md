# MaterialViewButton

It is a touchable view.

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialViewButton → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

## Properties

### <a id="properties-command"/>**Command**

Gets or sets the command to invoke when the button is activated.
 This is a bindable property.

Property type: ICommand<br>

Remarks: This property is used to associate a command with an instance of a button.

- <para>This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel. <see cref="P:Microsoft.Maui.Controls.VisualElement.IsEnabled" /> is controlled by the <see cref="M:Microsoft.Maui.Controls.Command.CanExecute(System.Object)" /> if set.</para>

<br>

### <a id="properties-commandparameter"/>**CommandParameter**

Gets or sets the parameter to pass to the MaterialViewButton.Command property.
 This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: null.

<br>

### <a id="properties-touchanimation"/>**TouchAnimation**

Gets or sets a custom animation to be executed when a icon is clicked.
 This is a bindable property.

Property type: ITouchAnimation<br>

Default value: null.

<br>

### <a id="properties-touchanimationtype"/>**TouchAnimationType**

Gets or sets an animation to be executed when an icon is clicked
 This is a bindable property.

Property type: TouchAnimationTypes<br>

| Name | Value | Description |
| --- | --: | --- |
| None | 0 | None: no animation runs. |
| Fade | 1 | Fade: Represents an animation that simulates a "fade" effect by changing the opacity over the target element. |
| Scale | 2 | Scale: Represents an animation that simulates a "sink" or "sunken" effect by scaling the target element. |
| Bounce | 3 | Bounce: Represents an animation that simulates a "sink" or "sunken" effect with a "bounce" effect when the user releases the target element. |

Default value: TouchAnimationTypes.Fade

<br>

## Events

### <a id="events-clicked"/>**Clicked**

Occurs when the card is clicked/tapped.

<br>

### <a id="events-pressed"/>**Pressed**

Occurs when the card is pressed.

<br>

### <a id="events-released"/>**Released**

Occurs when the card is released.

<br>
