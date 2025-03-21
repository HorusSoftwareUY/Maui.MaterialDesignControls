# MaterialRating

Ratings provide insight regarding others' opinions and experiences, and can allow the user to submit a rating of their own.

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialRating → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialRating.jpg)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialRating
        Label="How do you rate....?"
        Value="1"
        Animation="Scale"/>
```

### C# sample

```csharp
var MaterialRating = new MaterialRating()
{
    Label = "How do you rate....?",
    Value = 1,
    Animation = AnimationTypes.Scale
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/RatingPage.xaml)

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

Gets or sets the parameter to pass to the MaterialRating.Animation property.
 This is a bindable property.

Property type: [Nullable&lt;Double&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

Default value: null

<br>

### <a id="properties-characterspacing"/>**CharacterSpacing**

Gets or sets the spacing between characters of the label.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when a icon is clicked.
 This is a bindable property.

Property type: ICustomAnimation<br>

Default value: null

<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets the text style of the label.
 This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Defines whether an app's UI reflects text scaling preferences set in the operating system.
 The default value of this property is true

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

Default value: MaterialFontSize.BodyLarge - Tablet = 19 / Phone = 16

<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets MaterialRating.IsEnabled for the rating control.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

<br>

### <a id="properties-itemsperrow"/>**ItemsPerRow**

Defines the quantity of items per row on the rating.
 This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: 5

<br>

### <a id="properties-itemssize"/>**ItemsSize**

Defines the quantity of items on the rating.
 This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: 5

<br>

### <a id="properties-label"/>**Label**

Gets or sets the MaterialRating.Label for the label.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: Null

<br>

### <a id="properties-labelcolor"/>**LabelColor**

Gets or sets the MaterialRating.LabelColor for the text of the label.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.Text - Dark: MaterialDarkTheme.Text

<br>

### <a id="properties-labeltransform"/>**LabelTransform**

Defines the casing of the label.
 This is a bindable property.

Property type: [TextTransform](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.texttransform)<br>

<br>

### <a id="properties-selectediconsource"/>**SelectedIconSource**

Allows you to display a bitmap image on the rating when is selected.
 This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Remarks: For more options have a look at ImageButton.

<br>

### <a id="properties-selectediconssource"/>**SelectedIconsSource**

Allows you to display a bitmap image diferent on each rating when is selected.
 This is a bindable property.

Property type: [IEnumerable&lt;ImageSource&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

Remarks: For more options have a look at ImageButton.

<br>

### <a id="properties-strokecolor"/>**StrokeColor**

Gets or sets the  for the stroke of default start.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-strokethickness"/>**StrokeThickness**

Gets or sets the MaterialRating.StrokeThickness for the default start.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

<br>

### <a id="properties-unselectediconsource"/>**UnselectedIconSource**

Allows you to display a bitmap image on the rating when is unselected.
 This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Remarks: For more options have a look at ImageButton.

<br>

### <a id="properties-unselectediconssource"/>**UnselectedIconsSource**

Allows you to display a bitmap image diferent on each rating when is unselected.
 This is a bindable property.

Property type: [IEnumerable&lt;ImageSource&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

Remarks: For more options have a look at ImageButton.

<br>

### <a id="properties-value"/>**Value**

Defines the value of the Rating.
 This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: -1

<br>
