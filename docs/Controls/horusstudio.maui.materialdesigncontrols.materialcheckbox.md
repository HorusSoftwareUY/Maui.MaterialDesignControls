# MaterialCheckBox

A Checkbox  let users select one or more items from a list, or turn an item on or off and follows Material Design Guidelines [](https://m3.material.io/components/checkbox/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialCheckBox → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialCheckBox.jpg)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialCheckBox
        TextSide="Left"
        CommandCheckedChanged="{Binding CheckedChangedCommand}"
        CommandCheckedChangedParameter="Selected or Unselected"
        Text="Checkbox 1"/>
```

### C# sample

```csharp
var checkBox = new MaterialCheckBox()
{
    Text = "Checkbox 1"
    TextSide = TextSide.Left,
    CommandCheckedChanged = viewModel.CheckChangedCommand,
    CommandCheckedChangedParameter = "Selected or Unselected"
};
```

## Properties

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when checkbox is clicked. This is a bindable property.

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

Gets or sets the parameter to pass to the MaterialCheckBox.Animation property. This is a bindable property.

Property type: [Nullable&lt;Double&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

Default value: null.

<br>

### <a id="properties-characterspacing"/>**CharacterSpacing**

Gets or sets the spacing between characters of the label. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-color"/>**Color**

Gets or sets the  for the checkbox color.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light: MaterialLightTheme.Primary - Dark: MaterialDarkTheme.Primary

<br>

### <a id="properties-commandcheckedchanged"/>**CommandCheckedChanged**

Gets or sets the command to invoke when the checkbox changes its status. This is a bindable property.

Property type: ICommand<br>

Remarks: This property is used to associate a command with an instance of a checkbox. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.  is controlled by the  if set.

<br>

### <a id="properties-commandcheckedchangedparameter"/>**CommandCheckedChangedParameter**

Gets or sets the parameter to pass to the MaterialCheckBox.CommandCheckedChangedParameter property. This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: null.

<br>

### <a id="properties-content"/>**Content**

Gets the MaterialCheckBox.Content for the RadioButton.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Remarks: We disabled the set for this property because doesn't have sense set the content because we are setting with the checkbox and label.

<br>

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when checkbox is clicked. This is a bindable property.

Property type: ICustomAnimation<br>

Default value: null.

<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets the text style of the label. This is a bindable property.

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

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-fontsize"/>**FontSize**

Defines the font size of the label. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodyLarge

<br>

### <a id="properties-ischecked"/>**IsChecked**

Gets or sets MaterialCheckBox.IsChecked for the checkbox.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: null

<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets MaterialCheckBox.IsEnabled for the checkbox. This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

<br>

### <a id="properties-text"/>**Text**

Gets or sets the MaterialCheckBox.Text for the label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the MaterialCheckBox.TextColor for the text of the label.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light: MaterialLightTheme.Text - Dark: MaterialDarkTheme.Text

<br>

### <a id="properties-textside"/>**TextSide**

Defines the location of the label. This is a bindable property.

Property type: TextSide<br>

Default value: TextSide.Left

<br>

### <a id="properties-texttransform"/>**TextTransform**

Defines the casing of the label. This is a bindable property.

Property type: [TextTransform](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.texttransform)<br>

<br>

### <a id="properties-tickcolor"/>**TickColor**

Gets or sets the  for the tick color.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light: MaterialLightTheme.OnPrimary - Dark: MaterialDarkTheme.OnPrimary

Remarks: Only is supported on iOS.

<br>

## Events

### <a id="events-checkedchanged"/>**CheckedChanged**

Occurs when the checkbox is checked / unchecked

<br>

## Known issues and pending features

- [iOS] FontAttributes doesn't work
