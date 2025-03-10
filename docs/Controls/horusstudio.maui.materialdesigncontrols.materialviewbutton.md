# MaterialViewButton

It is a touchable view.

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialViewButton → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

## Properties

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when an icon is clicked
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

Gets or sets the parameter to pass to the MaterialViewButton.Animation property.
 This is a bindable property.

Property type: [Nullable&lt;Double&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

Default value: null.

<br>

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

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when a icon is clicked.
 This is a bindable property.

Property type: ICustomAnimation<br>

Default value: null.

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
