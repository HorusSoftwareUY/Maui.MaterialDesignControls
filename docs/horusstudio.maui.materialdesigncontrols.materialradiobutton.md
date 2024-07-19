# MaterialRadioButton

A RadioButton  let people select one option from a set of options and follows Material Design Guidelines [See here. ](https://m3.material.io/components/radio-button/overview).
 We reuse some code from MAUI official repository: https://github.com/dotnet/maui/blob/7076514d83f7e16ac49838307aefd598b45adcec/src/Controls/src/Core/RadioButton/RadioButton.cs

Namespace: HorusStudio.Maui.MaterialDesignControls

## Properties

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when radio button is clicked.
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

Gets or sets the parameter to pass to the MaterialRadioButton.Animation property.
 This is a bindable property.

Property type: [Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

Default value: null

### <a id="properties-characterspacing"/>**CharacterSpacing**

Gets or sets the spacing between characters of the label.
 This is a bindable property.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-commandcheckedchanged"/>**CommandCheckedChanged**

Gets or sets the command to invoke when the radio button changes its status.
 This is a bindable property.

Property type: ICommand<br>

Remarks: This property is used to associate a command with an instance of a radio button. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.  is controlled by the  if set.

### <a id="properties-commandcheckedchangedparameter"/>**CommandCheckedChangedParameter**

Gets or sets the parameter to pass to the MaterialRadioButton.CommandCheckedChangedParameter property.
 This is a bindable property.

Property type: [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: null

### <a id="properties-content"/>**Content**

Gets the MaterialRadioButton.Content for the RadioButton.
 This is a bindable property.
 We disabled the set for this property because doesn't have sense set the content because we are setting with the
 radio button and label.

Property type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-controltemplate"/>**ControlTemplate**

Gets or sets the MaterialRadioButton.ControlTemplate for the radio button.
 This is a bindable property.

Property type: ControlTemplate<br>

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when radio button is clicked.
 This is a bindable property.

Property type: ICustomAnimation<br>

Default value: null

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets the text style of the label.
 This is a bindable property.

Property type: FontAttributes<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Defines whether an app's UI reflects text scaling preferences set in the operating system.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the label.
 This is a bindable property.

Property type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-fontsize"/>**FontSize**

Defines the font size of the label.
 This is a bindable property.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-groupname"/>**GroupName**

Gets or sets the [String](https://docs.microsoft.com/en-us/dotnet/api/system.string) GroupName for the radio button. 
 This is a bindable property.

Property type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: DefaultGroupName

### <a id="properties-ischecked"/>**IsChecked**

Gets or sets MaterialRadioButton.IsChecked for the radio button. 
 This is a bindable property.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets MaterialRadioButton.IsEnabled for the radio button.
 This is a bindable property.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-strokecolor"/>**StrokeColor**

Gets or sets the  for the stroke of the radio button.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-text"/>**Text**

Gets or sets the MaterialRadioButton.Text for the label.
 This is a bindable property.

Property type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the MaterialRadioButton.TextColor for the text of the label.
 This is a bindable property.

Property type: Color<br>

### <a id="properties-textside"/>**TextSide**

Defines the location of the label. 
 This is a bindable property.

Property type: TextSide<br>

Default value: TextSide.Left

### <a id="properties-texttransform"/>**TextTransform**

Defines the casing of the label.
 This is a bindable property.

Property type: TextTransform<br>

### <a id="properties-value"/>**Value**

Defines the value of radio button selected
 This is a bindable property.

Property type: [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: null
