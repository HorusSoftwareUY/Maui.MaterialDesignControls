# MaterialRadioButton

A RadioButton  let people select one option from a set of options and follows Material Design Guidelines .
 We reuse some code from MAUI official repository: https://github.com/dotnet/maui/blob/7076514d83f7e16ac49838307aefd598b45adcec/src/Controls/src/Core/RadioButton/RadioButton.cs

Namespace: HorusStudio.Maui.MaterialDesignControls

## Properties

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when radio button is clicked.
 The default value is [AnimationTypes.Fade](./horusstudio.maui.materialdesigncontrols.animationtypes.md#fade).
 This is a bindable property.

Property Type: [AnimationTypes](./horusstudio.maui.materialdesigncontrols.animationtypes.md)<br>

### <a id="properties-animationparameter"/>**AnimationParameter**

Gets or sets the parameter to pass to the [MaterialRadioButton.Animation](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#animation) property.
 The default value is null.
 This is a bindable property.

Property Type: [Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### <a id="properties-characterspacing"/>**CharacterSpacing**

Gets or sets the spacing between characters of the label. This is a bindable property.

Property Type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-commandcheckedchanged"/>**CommandCheckedChanged**

Gets or sets the command to invoke when the radio button changes its status. This is a bindable property.

Property Type: ICommand<br>

**Remarks:**

This property is used to associate a command with an instance of a radio button. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.  is controlled by the  if set.

### <a id="properties-commandcheckedchangedparameter"/>**CommandCheckedChangedParameter**

Gets or sets the parameter to pass to the [MaterialRadioButton.CommandCheckedChangedParameter](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#commandcheckedchangedparameter) property.
 The default value is null. This is a bindable property.

Property Type: [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### <a id="properties-content"/>**Content**

Gets the [MaterialRadioButton.Content](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#content) for the RadioButton. This is a bindable property.
 We disabled the set for this property because doesn't have sense set the content because we are setting with the
 radio button and label.

Property Type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-controltemplate"/>**ControlTemplate**

Gets or sets the [MaterialRadioButton.ControlTemplate](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#controltemplate) for the radio button. This is a bindable property.

Property Type: ControlTemplate<br>

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when radio button is clicked.
 The default value is null.
 This is a bindable property.

Property Type: [ICustomAnimation](./horusstudio.maui.materialdesigncontrols.icustomanimation.md)<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets the text style of the label. This is a bindable property.

Property Type: FontAttributes<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true

Property Type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the label. This is a bindable property.

Property Type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-fontsize"/>**FontSize**

Defines the font size of the label. This is a bindable property.

Property Type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-groupname"/>**GroupName**

Gets or sets the [String](https://docs.microsoft.com/en-us/dotnet/api/system.string) GroupName for the radio button. 
 The default value is .
 This is a bindable property.

Property Type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-ischecked"/>**IsChecked**

Gets or sets [MaterialRadioButton.IsChecked](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#ischecked) for the radio button. 
 This is a bindable property.

Property Type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets [MaterialRadioButton.IsEnabled](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#isenabled) for the radio button. This is a bindable property.

Property Type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-strokecolor"/>**StrokeColor**

Gets or sets the  for the stroke of the radio button. This is a bindable property.

Property Type: Color<br>

### <a id="properties-text"/>**Text**

Gets or sets the [MaterialRadioButton.Text](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#text) for the label. This is a bindable property.

Property Type: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the [MaterialRadioButton.TextColor](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#textcolor) for the text of the label. This is a bindable property.

Property Type: Color<br>

### <a id="properties-textside"/>**TextSide**

Defines the location of the label. 
 The default value is [TextSide.Left](./horusstudio.maui.materialdesigncontrols.enums.textside.md#left)
 This is a bindable property.

Property Type: [TextSide](./horusstudio.maui.materialdesigncontrols.enums.textside.md)<br>

### <a id="properties-texttransform"/>**TextTransform**

Defines the casing of the label. This is a bindable property.

Property Type: TextTransform<br>

### <a id="properties-value"/>**Value**

Defines the value of radio button selected
 The default value is null
 This is a bindable property.

Property Type: [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>
