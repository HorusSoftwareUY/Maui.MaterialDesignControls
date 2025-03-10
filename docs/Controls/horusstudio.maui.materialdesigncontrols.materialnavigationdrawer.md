# MaterialNavigationDrawer

A navigation drawer  let people switch between UI views on larger devices [](https://m3.material.io/components/navigation-drawer/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialNavigationDrawer → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialNavigationDrawer.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialNavigationDrawer
        Headline="Mail"
        Command="{Binding TestCommand}"
        ItemsSource="{Binding Items}" />
```

### C# sample

```csharp
var navigationDrawer = new MaterialNavigationDrawer
{
    Headline="Mail"
    Command = TestCommand,
    ItemsSource = Items
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/NavigationDrawerPage.xaml)

## Properties

### <a id="properties-activeindicatorbackgroundcolor"/>**ActiveIndicatorBackgroundColor**

Defines the active background color. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.PrimaryContainer - Dark: MaterialDarkTheme.PrimaryContainer

<br>

### <a id="properties-activeindicatorcornerradius"/>**ActiveIndicatorCornerRadius**

Defines the active indicator corner radius. This is a bindable property.

Property type: [Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>

Default value: 28.0f

<br>

### <a id="properties-activeindicatorlabelcolor"/>**ActiveIndicatorLabelColor**

Defines the active indicator label color. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnPrimaryContainer - Dark: MaterialDarkTheme.OnPrimaryContainer

<br>

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when an icon is clicked
 The default value is AnimationTypes.Fade.
 This is a bindable property.

Property type: AnimationTypes<br>

| Name | Value | Description |
| --- | --: | --- |
| None | 0 | None |
| Fade | 1 | Fade |
| Scale | 2 | Scale |
| Custom | 3 | Custom |

Default value: MaterialAnimation.Type

<br>

### <a id="properties-animationparameter"/>**AnimationParameter**

Gets or sets the parameter to pass to the MaterialNavigationDrawer.Animation property.
 The default value is null.
 This is a bindable property.

Property type: [Nullable&lt;Double&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

Default value: MaterialAnimation.Parameter

<br>

### <a id="properties-badgebackgroundcolor"/>**BadgeBackgroundColor**

Gets or sets the text  for the badge background. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: Colors.Transparent - Dark: Colors.Transparent

<br>

### <a id="properties-badgefontfamily"/>**BadgeFontFamily**

Gets or sets the font family for the badge label. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-badgefontsize"/>**BadgeFontSize**

Gets or sets the  for the badge label. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.LabelLarge

<br>

### <a id="properties-badgetextcolor"/>**BadgeTextColor**

Gets or sets the text  for the badge. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OnSurfaceVariant - Dark: MaterialDarkTheme.OnSurfaceVariant

<br>

### <a id="properties-badgetype"/>**BadgeType**

Gets or sets the MaterialBadgeType. This is a bindable property.

Property type: MaterialBadgeType<br>

| Name | Value | Description |
| --- | --: | --- |
| Small | 0 | Small Badge |
| Large | 1 | Large Badge |

<br>

### <a id="properties-command"/>**Command**

Gets or sets the command for each item. This is a bindable property.

Property type: ICommand<br>

Default value: null

<br>

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when a icon is clicked.
 The default value is null.
 This is a bindable property.

Property type: ICustomAnimation<br>

<br>

### <a id="properties-disabledlabelcolor"/>**DisabledLabelColor**

Gets or sets the text  for the label when is disabled. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.Disable - Dark: MaterialDarkTheme.Disable

<br>

### <a id="properties-dividercolor"/>**DividerColor**

Gets or sets the  for the divider. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.OutlineVariant - Dark: MaterialDarkTheme.OutlineVariant

<br>

### <a id="properties-dividertype"/>**DividerType**

Gets or sets if dividers are visible between sections, items or not visible. This is a bindable property.

Property type: MaterialNavigationDrawerDividerType<br>

Default value: Dividers between sections: NavigationDrawerDividerType.Section

<br>

### <a id="properties-headlinecharactersspacing"/>**HeadlineCharactersSpacing**

Gets or sets the spacing between characters of the headline. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontTracking.TitleSmall

<br>

### <a id="properties-headlinefontattributes"/>**HeadlineFontAttributes**

Gets or sets the text style of the label. This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

Default value: null

<br>

### <a id="properties-headlinefontautoscalingenabled"/>**HeadlineFontAutoScalingEnabled**

Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

<br>

### <a id="properties-headlinefontfamily"/>**HeadlineFontFamily**

Gets or sets the font family for the headline. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-headlinefontsize"/>**HeadlineFontSize**

Defines the font size of the label. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.TitleSmall

<br>

### <a id="properties-headlinemargin"/>**HeadlineMargin**

Gets or sets the margin of the headline label. This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: Thickness(4, 16)

<br>

### <a id="properties-headlinetextcolor"/>**HeadlineTextColor**

Gets or sets the MaterialNavigationDrawer.HeadlineTextColor for the text of the headline. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.Primary - Dark: MaterialDarkTheme.Primary

<br>

### <a id="properties-headlinetexttransform"/>**HeadlineTextTransform**

Defines the casing of the label. This is a bindable property.

Property type: [TextTransform](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.texttransform)<br>

Default value: TextTransform.Defaultl

<br>

### <a id="properties-itemheightrequest"/>**ItemHeightRequest**

Gets or sets the height for each item. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 56.0

<br>

### <a id="properties-itemssource"/>**ItemsSource**

Gets or sets the items source. This is a bindable property.

Property type: [IEnumerable&lt;MaterialNavigationDrawerItem&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>

Default value: True

<br>

### <a id="properties-itemtemplate"/>**ItemTemplate**

Gets or sets the item template for each item from ItemsSource. This is a bindable property.

Property type: [DataTemplate](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.datatemplate)<br>

Default value: null

<br>

### <a id="properties-labelcharactersspacing"/>**LabelCharactersSpacing**

Gets or sets the spacing between characters of each item label. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontTracking.LabelLarge

<br>

### <a id="properties-labelcolor"/>**LabelColor**

Gets or sets the MaterialNavigationDrawer.LabelColor for the text of each item. This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Light: MaterialLightTheme.Text - Dark: MaterialDarkTheme.Text

<br>

### <a id="properties-labelfontattributes"/>**LabelFontAttributes**

Gets or sets the text style of each item label. This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

Default value: null

<br>

### <a id="properties-labelfontautoscalingenabled"/>**LabelFontAutoScalingEnabled**

Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

<br>

### <a id="properties-labelfontfamily"/>**LabelFontFamily**

Gets or sets the font family for each item label. This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-labelfontsize"/>**LabelFontSize**

Gets or sets the MaterialNavigationDrawer.LabelFontSize for the text of each item. This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.LabelLarge / Tablet: 14 - Phone: 11

<br>

### <a id="properties-labeltexttransform"/>**LabelTextTransform**

Defines the casing of the label of each item. This is a bindable property.

Property type: [TextTransform](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.texttransform)<br>

Default value: TextTransform.Default

<br>

### <a id="properties-sectiontemplate"/>**SectionTemplate**

Gets or sets the section template.

Property type: [DataTemplate](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.datatemplate)<br>

Default value: null

<br>
