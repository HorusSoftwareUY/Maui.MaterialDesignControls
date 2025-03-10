# MaterialChips

A Chips help people enter information, make selections, filter content, or trigger actions [see here.](https://m3.material.io/components/chips/overview)

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialChips → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialChips.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialChips
       Type="Normal"
       IconStateOnSelection="BothVisible"
       LeadingIcon="plus.png"
       Text="Suggestion both"
       TrailingIcon="horus_logo.png"/>
```

### C# sample

```csharp
var chips = new MaterialChips
{
    Type = MaterialChipsType.Normal,
    IconStateOnSelection = IconStateType.BothVisible,
    LeadingIcon = "plus.png",
    Text = "Suggestion both",
    TrailingIcon="horus_logo.png"
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/ChipsPage.xaml)

## Properties

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when is clicked.
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

Gets or sets the parameter to pass to the MaterialChips.Animation property.
 This is a bindable property.

Property type: [Nullable&lt;Double&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

Default value: null

<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a color that describes the background color of the chips.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.SurfaceContainerLow - Dark = MaterialDarkTheme.SurfaceContainerLow

<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets a color that describes the border stroke color of the chips.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Outline - Dark = MaterialDarkTheme.Outline

Remarks:

- <para>This property has no effect if <see cref="P:Microsoft.Maui.Controls.IBorderElement.BorderWidth">IBorderElement.BorderWidth</see> is set to 0.</para>

- <para>On Android this property will not have an effect unless <see cref="P:Microsoft.Maui.Controls.VisualElement.BackgroundColor" />is set to a non-default color.</para>

<br>

### <a id="properties-borderwidth"/>**BorderWidth**

Gets or sets the width of the border, in device-independent units.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 1

Remarks: Set this value to a non-zero value in order to have a visible border.

<br>

### <a id="properties-command"/>**Command**

Gets or sets the command to invoke when the Chips is activated.
 This is a bindable property.

Property type: ICommand<br>

Default value: null

Remarks: This property is used to associate a command with an instance of Chips. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.

- <para>
  <see cref="P:Microsoft.Maui.Controls.VisualElement.IsEnabled">VisualElement.IsEnabled</see> is controlled by the <see cref="M:Microsoft.Maui.Controls.Command.CanExecute(System.Object)">Command.CanExecute(object)</see> if set.</para>

<br>

### <a id="properties-commandparameter"/>**CommandParameter**

Gets or sets the parameter to pass to the MaterialChips.Command property.
 This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: null

<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the Chips.
 This is a bindable property.

Property type: [CornerRadius](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.cornerradius)<br>

Default value: CornerRadius(8)

<br>

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when is clicked.
 This is a bindable property.

Property type: ICustomAnimation<br>

Default value: null

<br>

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the text of this chips.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets the size of the font for the text of this chips.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.LabelLarge / Tablet: 14 - Phone: 11

<br>

### <a id="properties-iconstateonselection"/>**IconStateOnSelection**

Gets or sets the badge type according to IconStateType enum.
 This is a bindable property.

Property type: IconStateType<br>

| Name | Value | Description |
| --- | --: | --- |
| BothVisible | 0 | Visible both icon when selected |
| LeadingVisible | 1 | Visible only Leading icon when selected |
| TrailingVisible | 2 | Visible only Trailing icon when selected |

Default value: IconStateType.BothVisible

<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets the state when the Chips is enabled.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

<br>

### <a id="properties-isselected"/>**IsSelected**

Gets or sets the state when the Chips is selected.
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: False

<br>

### <a id="properties-leadingicon"/>**LeadingIcon**

Gets or sets image leading for the Chips.
 This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Default value: null

<br>

### <a id="properties-leadingicontintcolor"/>**LeadingIconTintColor**

Gets or sets a color that describes the leading icon color of the chips.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Primary - Dark = MaterialDarkTheme.Primary

<br>

### <a id="properties-padding"/>**Padding**

Gets or sets the padding for the Chips.
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

Gets or sets a color that describes the shadow color of the chips.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Shadow - Dark = MaterialDarkTheme.Shadow

<br>

### <a id="properties-text"/>**Text**

Gets or sets text the Chips.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: [String.Empty](https://docs.microsoft.com/en-us/dotnet/api/system.string.empty)

<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets text color the Chips.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.OnSurfaceVariant - Dark = MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-trailingicon"/>**TrailingIcon**

Gets or sets image trailing for the Chips.
 This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Default value: null

<br>

### <a id="properties-trailingicontintcolor"/>**TrailingIconTintColor**

Gets or sets a color that describes the trailing icon color of the chips.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Primary - Dark = MaterialDarkTheme.Primary

<br>

### <a id="properties-type"/>**Type**

Gets or sets type Chips.
 This is a bindable property.

Property type: MaterialChipsType<br>

| Name | Value | Description |
| --- | --: | --- |
| Filter | 0 | Filter chips |
| Normal | 1 | Assist, input and suggestion chips |

Default value: MaterialChipsType.Normal

Remarks:

- <para>Normal: They are for the types assist, input amd suggestion, chips help narrow a user’s intent by presenting dynamically generated suggestions, such as possible responses or search filters.</para>

- <para>Filter: chips use tags or descriptive words to filter content. They can be a good alternative to segmented buttons or checkboxes when viewing a list or search results.</para>

<br>

## Events

### <a id="events-clicked"/>**Clicked**

Occurs when the chips is clicked/tapped.

<br>

## Known issues and pending features

* .NET 7 not work LineBreakMode.
