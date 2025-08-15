# MaterialChip

Chips help people enter information, make selections, filter content, or trigger actions and follow Material Design Guidelines. [See more.](https://m3.material.io/components/chips/overview)

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialChip → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

Remarks: The [MaterialViewGroup](docs/Controls/horusstudio.maui.materialdesigncontrols.materialviewgroup.md) class allows grouping chips, providing control over the selection type (Single or Multiple), item selection through bindings, and commands that trigger when the selection changes.

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialChip.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialChip
       Type="Normal"
       LeadingIcon="plus.png"
       Text="Suggestion both"
       TrailingIcon="horus_logo.png"/>
```

### C# sample

```csharp
var chip = new MaterialChip
{
    Type = MaterialChipType.Normal,
    LeadingIcon = "plus.png",
    Text = "Suggestion both",
    TrailingIcon="horus_logo.png"
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/ChipsPage.xaml)

## Properties

### <a id="properties-applyleadingicontintcolor"/>**ApplyLeadingIconTintColor**

Gets or sets if leading icon should use tint color or not.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: true

<br>

### <a id="properties-applytrailingicontintcolor"/>**ApplyTrailingIconTintColor**

Gets or sets if trailing icon should use tint color or not.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: true

<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a background color for Chip.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.SurfaceContainerLow - Dark = MaterialDarkTheme.SurfaceContainerLow

<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets stroke color for Chip.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Outline - Dark = MaterialDarkTheme.Outline

Remarks:

- <para>This property has no effect if <see cref="P:Microsoft.Maui.Controls.IBorderElement.BorderWidth">IBorderElement.BorderWidth</see> is set to 0.</para>

- <para>On Android this property will not have an effect unless <see cref="P:Microsoft.Maui.Controls.VisualElement.BackgroundColor">VisualElement.BackgroundColor</see> is set to a non-default color.</para>

<br>

### <a id="properties-borderwidth"/>**BorderWidth**

Gets or sets border width for Chip in device-independent units.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 1

Remarks: Set this value to a non-zero value in order to have a visible border.

<br>

### <a id="properties-command"/>**Command**

Gets or sets the command to invoke when Chip is activated.
 This is a bindable property.

Property type: ICommand<br>

Default value: null

Remarks: This property is used to associate a command with an instance of Chips. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.

- <para>
  <see cref="P:Microsoft.Maui.Controls.VisualElement.IsEnabled">VisualElement.IsEnabled</see> is controlled by the <see cref="M:Microsoft.Maui.Controls.Command.CanExecute(System.Object)">Command.CanExecute(object)</see> if set.</para>

<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for Chip.
 This is a bindable property.

Property type: [CornerRadius](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.cornerradius)<br>

Default value: CornerRadius(8)

<br>

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets a font family for Chip.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets font size for Chip.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.LabelLarge / Tablet: 14 - Phone: 11

<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets if Chip is enabled.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: true

<br>

### <a id="properties-isselected"/>**IsSelected**

Gets or sets if Chip is selected (Filter type only).
 Inherited from IGroupableView.IsSelected.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: false

<br>

### <a id="properties-leadingicon"/>**LeadingIcon**

Gets or sets a leading icon for Chip.
 This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Default value: null

<br>

### <a id="properties-leadingicontintcolor"/>**LeadingIconTintColor**

Gets or sets tint color for Chip's leading icon.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Primary - Dark = MaterialDarkTheme.Primary

<br>

### <a id="properties-padding"/>**Padding**

Gets or sets the padding for Chip.
 This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: Thickness(16,0)

<br>

### <a id="properties-shadow"/>**Shadow**

Gets or sets the shadow effect cast by the element.
 This is a bindable property.

Property type: [Shadow](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.shadow)<br>

Default value: MaterialElevation.Level1

<br>

### <a id="properties-shadowcolor"/>**ShadowColor**

Gets or sets shadow color for Chip.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Shadow - Dark = MaterialDarkTheme.Shadow

<br>

### <a id="properties-text"/>**Text**

Gets or sets text for Chip.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: [string.Empty](https://docs.microsoft.com/en-us/dotnet/api/system.string.empty)

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets text color for Chip.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.OnSurfaceVariant - Dark = MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-touchanimation"/>**TouchAnimation**

Gets or sets a custom animation to be executed when Chip is activated.
 Inherited from ITouchableView.TouchAnimation.
 This is a bindable property.

Property type: ITouchAnimation<br>

Default value: null

<br>

### <a id="properties-touchanimationtype"/>**TouchAnimationType**

Gets or sets an animation to be executed when Chip is activated.
 Inherited from ITouchableView.TouchAnimationType.
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

### <a id="properties-trailingicon"/>**TrailingIcon**

Gets or sets a trailing icon for Chip.
 This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Default value: null

<br>

### <a id="properties-trailingicontintcolor"/>**TrailingIconTintColor**

Gets or sets tint color for Chip's trailing icon.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Primary - Dark = MaterialDarkTheme.Primary

<br>

### <a id="properties-type"/>**Type**

Gets or sets Chip type.
 This is a bindable property.

Property type: MaterialChipType<br>

| Name | Value | Description |
| --- | --: | --- |
| Filter | 0 | Filter chips |
| Normal | 1 | Assist, input and suggestion chips |

Default value: MaterialChipType.Normal

Remarks:

- <para>Normal: Help narrow a user’s intent by presenting dynamically generated suggestions, such as possible responses.</para>

- <para>Filter: Use tags or descriptive words to filter content. They can be a good alternative to segmented buttons or checkboxes when viewing a list or search results.</para>

<br>

### <a id="properties-value"/>**Value**

Gets or sets a value for Chip.
 Inherited from IGroupableView.Value.
 This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: MaterialChip.Text

Remarks: If a value is not explicitly set, the control will use the Text property if set or the Id property as its default.

<br>

## Events

### <a id="events-clicked"/>**Clicked**

Occurs when the chips is clicked/tapped.

<br>
