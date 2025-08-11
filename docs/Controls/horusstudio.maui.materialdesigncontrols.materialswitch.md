# MaterialSwitch

A switch  that allows the selection of an item on or off, and follows Material Design Guidelines [](https://m3.material.io/components/switch/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialSwitch → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialSwitch.jpg)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialSwitch
        IsToggled="True"/>
```

### C# sample

```csharp
var switch = new MaterialSwitch()
{
    IsToggled = True
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SwitchPage.xaml)

## Properties

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets a color that describes the border stroke color of the switch. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Remarks: This property has no effect if  is set to 0.

<br>

### <a id="properties-borderwidth"/>**BorderWidth**

Gets or sets the width of the border. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Remarks: Set this value to a non-zero value in order to have a visible border.

<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets a value that indicates whether the font for the text of this switch is bold, italic, or neither.
 This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

<br>

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the text of this switch. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets the size of the font for the text of this switch. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-horizontaltextalignment"/>**HorizontalTextAlignment**

Gets or sets a value that indicates the horizontal alignment of the text and supporting text.
 This is a bindable property.

Property type: [TextAlignment](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.textalignment)<br>

<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets if switch is on 'OnDisabled' state or 'OffDisabled'.
 The default value is true.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

<br>

### <a id="properties-istoggled"/>**IsToggled**

Gets or sets if switch is on 'On' state or on 'Off'.
 The default value is false.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

<br>

### <a id="properties-selectedicon"/>**SelectedIcon**

Allows you to display a image on the switch's thumb when it is on the ON state. This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

<br>

### <a id="properties-spacing"/>**Spacing**

Gets or sets the spacing between the switch and the texts (Text and SupportingText).
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-supportingfontattributes"/>**SupportingFontAttributes**

Gets or sets a value that indicates whether the font for the supporting text of this switch is bold, italic, or neither.
 This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

<br>

### <a id="properties-supportingfontfamily"/>**SupportingFontFamily**

Gets or sets the font family for the supporting text of this switch. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-supportingfontsize"/>**SupportingFontSize**

Gets or sets the size of the font for the supporting text of this switch. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-supportingtext"/>**SupportingText**

Gets or sets the supporting text displayed next to the switch and under the Text.
 The default value is null. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-supportingtextcolor"/>**SupportingTextColor**

Gets or sets the  for the supporting text of the switch. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-text"/>**Text**

Gets or sets the text displayed next to the switch.
 The default value is null. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the  for the text of the switch. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-textside"/>**TextSide**

Determines if the Text and SupportingText are displayed to the right or left of the switch.
 The default value is Left. This is a bindable property.

Property type: TextSide<br>

| Name | Value | Description |
| --- | --: | --- |
| Right | 0 | Right |
| Left | 1 | Left |

<br>

### <a id="properties-textspacing"/>**TextSpacing**

Gets or sets the spacing between the Text and SupportingText.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-thumbcolor"/>**ThumbColor**

Gets or sets a color that describes the thumb color of the switch. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-toggledcommand"/>**ToggledCommand**

Gets or sets the command to invoke when the switch's IsToggled property changes. This is a bindable property.

Property type: ICommand<br>

Remarks: This property is used to associate a command with an instance of a switch.
 This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.
 The command parameter is of type [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean) and corresponds to the value of the MaterialSwitch.IsToggled property.

<br>

### <a id="properties-touchanimation"/>**TouchAnimation**

Gets or sets a custom animation to be executed when button is clicked.
 This is a bindable property.

Property type: ITouchAnimation<br>

Default value: Null

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

Default value: TouchAnimationTypes.Fade

<br>

### <a id="properties-trackcolor"/>**TrackColor**

Gets or sets a color that describes the track color of the switch. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-trackheightrequest"/>**TrackHeightRequest**

Gets or sets the desired height of the track. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Remarks: The default and minimum value is 32.

<br>

### <a id="properties-trackwidthrequest"/>**TrackWidthRequest**

Gets or sets the desired width of the track. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Remarks: The default and minimum value is 52.

<br>

### <a id="properties-unselectedicon"/>**UnselectedIcon**

Allows you to display a image on the switch's thumb when it is on the OFF state. This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

<br>

## Known issues and pending features

* Track color animation: change from on-track color to off-track color within the toggle animation.
 * [iOS] FontAttributes and SupportingFontAttributes don't work (MAUI issue)
