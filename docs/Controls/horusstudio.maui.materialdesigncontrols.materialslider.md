# MaterialSlider

Sliders let users make selections from a range of values and follow Material Design Guidelines. [See more](https://m3.material.io/components/sliders/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialSlider → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialSlider.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

 <material:MaterialSlider 
                     Label="Only label"
                       Value="{Binding Value}"
                    BackgroundColor="{x:Static material:MaterialLightTheme.SurfaceContainer}" />
```

### C# sample

```csharp
var slider = new MaterialSlider
{
   Label = "slider"
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SliderPage.xaml)

## Properties

### <a id="properties-activetrackcolor"/>**ActiveTrackColor**

Gets or sets the color for the active track.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.Primary - Dark: MaterialDarkTheme.Primary

<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

This property is mandatory to set if you want a proper design.
 Allows you to set the color of the thumb shadow.
 You should set it equal to the background color of the slider's container.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-characterspacing"/>**CharacterSpacing**

Gets or sets the spacing between characters of the label.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontTracking.BodyMedium

<br>

### <a id="properties-dragcompletedcommand"/>**DragCompletedCommand**

Gets or sets the command executed at the end of a drag action.
 This is a bindable property.

Property type: ICommand<br>

<br>

### <a id="properties-dragstartedcommand"/>**DragStartedCommand**

Gets or sets the command executed at the beginning of a drag action.
 This is a bindable property.

Property type: ICommand<br>

<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets a value that indicates whether the font for the text of this button is bold, italic, or neither.
 This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

Remarks: Typically this should always be enabled for accessibility reasons.

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

### <a id="properties-inactivetrackcolor"/>**InactiveTrackColor**

Gets or sets the color for the inactive track.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-internalslider"/>**InternalSlider**

Internal implementation of the Slider control.

Property type: [Slider](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.slider)<br>

Remarks: This property can affect the internal behavior of this control. Use only if you fully understand the potential impact.

<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets if slider is enabled.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: true

<br>

### <a id="properties-label"/>**Label**

Gets or sets the text for the label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-labelcolor"/>**LabelColor**

Gets or sets the color for the text of the label.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.Text - Dark: MaterialDarkTheme.Text

<br>

### <a id="properties-labeltransform"/>**LabelTransform**

Defines the casing of the label.
 This is a bindable property.

Property type: [TextTransform](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.texttransform)<br>

<br>

### <a id="properties-maximum"/>**Maximum**

Defines the maximum value of the slider.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 1

<br>

### <a id="properties-maximumcharacterspacing"/>**MaximumCharacterSpacing**

Gets or sets the spacing between characters of the maximum label.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-maximumfontattributes"/>**MaximumFontAttributes**

Gets or sets the text style of the maximum label.
 This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

<br>

### <a id="properties-maximumfontautoscalingenabled"/>**MaximumFontAutoScalingEnabled**

Defines whether an app's UI reflects text scaling preferences set in the operating system.
 The default value of this property is true.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: true

<br>

### <a id="properties-maximumfontfamily"/>**MaximumFontFamily**

Gets or sets the font family for the maximum label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-maximumfontsize"/>**MaximumFontSize**

Defines the font size of the maximum label.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-maximumimagesource"/>**MaximumImageSource**

Allows you to display a bitmap image instead of a label on the maximum side.
 This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

<br>

### <a id="properties-maximumlabel"/>**MaximumLabel**

Gets or sets the text for the maximum label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-maximumlabelcolor"/>**MaximumLabelColor**

Gets or sets the color for the text of the maximum label.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-maximumlabeltransform"/>**MaximumLabelTransform**

Defines the casing of the maximum label.
 This is a bindable property.

Property type: [TextTransform](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.texttransform)<br>

<br>

### <a id="properties-minimum"/>**Minimum**

Defines the minimum value of the slider.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 0

<br>

### <a id="properties-minimumcharacterspacing"/>**MinimumCharacterSpacing**

Gets or sets the spacing between characters of the minimum label.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontTracking.BodyMedium

<br>

### <a id="properties-minimumfontattributes"/>**MinimumFontAttributes**

Gets or sets the text style of the minimum label.
 This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

<br>

### <a id="properties-minimumfontautoscalingenabled"/>**MinimumFontAutoScalingEnabled**

Defines whether an app's UI reflects text scaling preferences set in the operating system.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: true

Remarks: Typically this should always be enabled for accessibility reasons.

<br>

### <a id="properties-minimumfontfamily"/>**MinimumFontFamily**

Gets or sets the font family for the minimum label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-minimumfontsize"/>**MinimumFontSize**

Defines the font size of the minimum label.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-minimumimagesource"/>**MinimumImageSource**

Allows you to display a bitmap image instead of a label on the minimum side.
 This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

<br>

### <a id="properties-minimumlabel"/>**MinimumLabel**

Gets or sets the text for the minimum label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

<br>

### <a id="properties-minimumlabelcolor"/>**MinimumLabelColor**

Gets or sets the color for the text of the minimum label.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.Text - Dark: MaterialDarkTheme.Text

<br>

### <a id="properties-minimumlabeltransform"/>**MinimumLabelTransform**

Defines the casing of the minimum label.
 This is a bindable property.

Property type: [TextTransform](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.texttransform)<br>

<br>

### <a id="properties-showicons"/>**ShowIcons**

Gets or sets if icons are shown.
 This is a bindable property.
 This property is used to show the icons even when the minimum/maximum label is set.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: false

<br>

### <a id="properties-showvalueindicator"/>**ShowValueIndicator**

Defines whether to show the value indicator.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

<br>

### <a id="properties-thumbcolor"/>**ThumbColor**

Gets or sets the thumb color.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.Primary - Dark: MaterialDarkTheme.Primary

<br>

### <a id="properties-thumbheight"/>**ThumbHeight**

Allows you to set the thumb height.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: 44

<br>

### <a id="properties-thumbimagesource"/>**ThumbImageSource**

Allows you to display an image on thumb.
 This is a bindable property.
 As a recommendation, on iOS you should set the thumb background color.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Remarks: For more options, see ImageButton.

<br>

### <a id="properties-thumbwidth"/>**ThumbWidth**

Allows you to set the thumb width.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: 4

<br>

### <a id="properties-trackcornerradius"/>**TrackCornerRadius**

Gets or sets the track corner radius for the slider control.
 This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

<br>

### <a id="properties-trackheight"/>**TrackHeight**

Gets or sets the track height for the slider control.
 This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

<br>

### <a id="properties-trackimagesource"/>**TrackImageSource**

Gets or sets the track image for the slider control.
 This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

<br>

### <a id="properties-userinteractionenabled"/>**UserInteractionEnabled**

Gets or sets if user interactions are enabled.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: true

<br>

### <a id="properties-value"/>**Value**

Defines the value of the slider.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 0

<br>

### <a id="properties-valuechangedcommand"/>**ValueChangedCommand**

Gets or sets the command executed when value changed.
 This is a bindable property.

Property type: ICommand<br>

<br>

### <a id="properties-valueindicatorbackgroundcolor"/>**ValueIndicatorBackgroundColor**

Sets the background color of the value indicator.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-valueindicatorfontsize"/>**ValueIndicatorFontSize**

Sets the value indicator's font size.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-valueindicatorformat"/>**ValueIndicatorFormat**

Sets the value indicator's format. This uses the format from [string.Format(string, object?)](https://docs.microsoft.com/en-us/dotnet/api/system.string.format) 
 to show the value in the specified format.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: {0:0.00}

<br>

### <a id="properties-valueindicatorsize"/>**ValueIndicatorSize**

Allows you to set the value indicator size.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: 44

<br>

### <a id="properties-valueindicatortextcolor"/>**ValueIndicatorTextColor**

Sets the text color of the value indicator.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>
