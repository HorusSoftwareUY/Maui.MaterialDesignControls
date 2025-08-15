# ITouchableView

The ITouchableView interface serves as a base for touchable views.

Namespace: HorusStudio.Maui.MaterialDesignControls.Behaviors

<br>

## Properties

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets the state when the view is enabled.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

<br>

### <a id="properties-touchanimation"/>**TouchAnimation**

Gets or sets a custom animation to be executed when view is clicked.

Property type: ITouchAnimation<br>

<br>

### <a id="properties-touchanimationtype"/>**TouchAnimationType**

Gets or sets an animation to be executed when view is clicked.

Property type: TouchAnimationTypes<br>

| Name | Value | Description |
| --- | --: | --- |
| None | 0 | None: no animation runs. |
| Fade | 1 | Fade: Represents an animation that simulates a "fade" effect by changing the opacity over the target element. |
| Scale | 2 | Scale: Represents an animation that simulates a "sink" or "sunken" effect by scaling the target element. |
| Bounce | 3 | Bounce: Represents an animation that simulates a "sink" or "sunken" effect with a "bounce" effect when the user releases the target element. |

<br>
