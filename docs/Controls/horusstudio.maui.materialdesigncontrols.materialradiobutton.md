# MaterialRadioButton

A RadioButton  let people select one option from a set of options and follows Material Design Guidelines [See here. ](https://m3.material.io/components/radio-button/overview)
 We reuse some code from MAUI official repository: https://github.com/dotnet/maui/blob/7076514d83f7e16ac49838307aefd598b45adcec/src/Controls/src/Core/RadioButton/RadioButton.cs

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialRadioButton → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialRadioButton.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialRadioButton
        TextSide="Left"
        CheckedChangedCommand="{Binding CheckedChangedCommand}"
        Text="Radio button 1"/>
```

### C# sample

```csharp
var radioButton = new MaterialRadioButton()
{
    Text = "Radio button 1",
    TextSide = TextSide.Left,
    CheckedChangedCommand = viewModel.CheckChangedCommand
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/ProgressIndicatorPage.xaml)

## Properties

### <a id="properties-characterspacing"/>**CharacterSpacing**

Gets or sets the spacing between characters of the label.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-checkedchangedcommand"/>**CheckedChangedCommand**

Gets or sets the command to invoke when the radio button changes its status.
 This is a bindable property.

Property type: ICommand<br>

Remarks: This property is used to associate a command with an instance of a radio button.
 This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.
  is controlled by the  if set.
 The command parameter is of type [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean) and corresponds to the value of the MaterialRadioButton.IsChecked property.

<br>

### <a id="properties-content"/>**Content**

Gets the MaterialRadioButton.Content for the RadioButton.
 This is a bindable property.
 We disabled the set for this property because doesn't have sense set the content because we are setting with the
 radio button and label.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-controltemplate"/>**ControlTemplate**

Gets or sets the MaterialRadioButton.ControlTemplate for the radio button.
 This is a bindable property.

Property type: [ControlTemplate](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.controltemplate)<br>

<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets the text style of the label.
 This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Defines whether an app's UI reflects text scaling preferences set in the operating system.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

<br>

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-fontsize"/>**FontSize**

Defines the font size of the label.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-internalradiobutton"/>**InternalRadioButton**

Internal implementation of the  control.

Property type: [RadioButton](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.radiobutton)<br>

Remarks: This property can affect the internal behavior of this control. Use only if you fully understand the potential impact.

<br>

### <a id="properties-ischecked"/>**IsChecked**

Gets or sets the MaterialRadioButton.IsChecked for the radio button. 
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets MaterialRadioButton.IsEnabled for the radio button.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

<br>

### <a id="properties-isselected"/>**IsSelected**

Gets or sets the MaterialRadioButton.IsChecked property for the radio button.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Remarks: This property is used internally, and it's recommended to avoid setting it directly.

<br>

### <a id="properties-strokecolor"/>**StrokeColor**

Gets or sets the  for the stroke of the radio button.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light: MaterialLightTheme.Primary - Dark: MaterialDarkTheme.Primary

<br>

### <a id="properties-text"/>**Text**

Gets or sets the MaterialRadioButton.Text for the label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the MaterialRadioButton.TextColor for the text of the label.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light: MaterialLightTheme.Text - Dark: MaterialDarkTheme.Text

<br>

### <a id="properties-textside"/>**TextSide**

Defines the location of the label. 
 This is a bindable property.

Property type: TextSide<br>

| Name | Value | Description |
| --- | --: | --- |
| Right | 0 | Right |
| Left | 1 | Left |

Default value: TextSide.Left

<br>

### <a id="properties-texttransform"/>**TextTransform**

Defines the casing of the label.
 This is a bindable property.

Property type: [TextTransform](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.texttransform)<br>

<br>

### <a id="properties-touchanimation"/>**TouchAnimation**

Gets or sets a custom animation to be executed when radio button is clicked.
 This is a bindable property.

Property type: ITouchAnimation<br>

Default value: null

<br>

### <a id="properties-touchanimationtype"/>**TouchAnimationType**

Gets or sets an animation to be executed when radio button is clicked.
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

### <a id="properties-value"/>**Value**

Defines the value of the radio button
 This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: MaterialRadioButton.Text

Remarks: If a value is not explicitly set, the control will use the value of the MaterialRadioButton.Text property or the  property as its default.

<br>

## Events

### <a id="events-checkedchanged"/>**CheckedChanged**

Occurs when the radio button is switched

<br>

## Known issues and pending features

* [iOS] FontAttributes doesn't work.
 * Using a control template doesn't work when define a custom style for disabled state.
