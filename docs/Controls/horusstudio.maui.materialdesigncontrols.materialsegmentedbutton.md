# MaterialSegmentedButton

Segmented buttons  help people select options, switch views, or sort elements, and follows Material Design Guidelines [](https://m3.material.io/components/segmented-buttons/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialSegmentedButton → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialSegmentedButton.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialSegmentedButton
    ItemsSource="{Binding Items}"
    SelectionCommand="{Binding OnItemSelectedCommand}"
    Type="Outlined"/>
```

### C# sample

```csharp
var segmentedButtons = new MaterialSegmentedButton
{
    SelectionCommand = OnItemSelectedCommand,
    ItemsSource = Items,
    Type = MaterialSegmentedButtonType.Outlined
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SegmentedButtonsPage.xaml)

## Properties

### <a id="properties-allowmultiselect"/>**AllowMultiSelect**

Gets or sets the if the control allows multiple selection.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets the border color for the control.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Outline - Dark = MaterialDarkTheme.Outline

Remarks:

- <para>This property has no effect if <see cref="P:Microsoft.Maui.Controls.IBorderElement.BorderWidth">IBorderElement.BorderWidth</see> is set to 0.</para>

- <para>On Android this property will not have an effect unless <see cref="P:Microsoft.Maui.Controls.VisualElement.BackgroundColor" />is set to a non-default color.</para>

<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the control.
 This is a bindable property.

Property type: [CornerRadius](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.cornerradius)<br>

Default value: CornerRadius(16)

<br>

### <a id="properties-heightrequest"/>**HeightRequest**

Gets or sets the desired height override of this element.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 40

<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets the state when the Segmented is enabled.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

<br>

### <a id="properties-itemssource"/>**ItemsSource**

Gets or sets items source mapped to segmented buttons.
 This is a bindable property.

Property type: [IEnumerable&lt;MaterialSegmentedButtonItem&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

Default value: Null

<br>

### <a id="properties-itemtemplate"/>**ItemTemplate**

Gets or sets the item template for each item from ItemsSource.
 This is a bindable property.

Property type: [DataTemplate](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.datatemplate)<br>

Default value: null

<br>

### <a id="properties-selecteditem"/>**SelectedItem**

Gets or sets the selected button.
 This is a bindable property.

Property type: MaterialSegmentedButtonItem<br>

Default value: Null

Remarks: Useful when you set MaterialSegmentedButton.AllowMultiSelect to False

<br>

### <a id="properties-selecteditems"/>**SelectedItems**

Gets or sets the selected buttons.
 This is a bindable property.

Property type: [IEnumerable&lt;MaterialSegmentedButtonItem&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

Default value: Array.Empty

Remarks: Useful when you set MaterialSegmentedButton.AllowMultiSelect to True

<br>

### <a id="properties-touchanimation"/>**TouchAnimation**

Gets or sets a custom animation to be executed when is clicked.
 This is a bindable property.

Property type: ITouchAnimation<br>

Default value: null

<br>

### <a id="properties-touchanimationtype"/>**TouchAnimationType**

Gets or sets an animation to be executed when is clicked.
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

### <a id="properties-type"/>**Type**

Gets or sets the segmented buttons type according to MaterialSegmentedButtonType enum.
 This is a bindable property.

Property type: MaterialSegmentedButtonType<br>

| Name | Value | Description |
| --- | --: | --- |
| Outlined | 0 | Outlined segmented button |
| Filled | 1 | Filled segmented button |

Default value: MaterialButtonType.Outlined

<br>

## Events

### <a id="events-selectionchanged"/>**SelectionChanged**

Occurs when the selection of one of the segmented buttons changes.

<br>

## Known issues and pending features

* [iOS] FontAttributes doesn't work (MAUI issue)
 * [iOS] TextDecorations doesn't work correctly when different values are set for different VisualStates
