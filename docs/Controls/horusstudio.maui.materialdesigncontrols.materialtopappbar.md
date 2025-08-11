# MaterialTopAppBar

A top app bar  that display navigation, actions, and text at the top of a screen, and follows Material Design Guidelines [see here.](https://m3.material.io/components/top-app-bar/overview)

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialTopAppBar → [Grid](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.grid)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialTopAppbar.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

< material:MaterialTopAppBar
        Headline="Large type"
        Description="Description text"
        LeadingIconCommand="{Binding LeadingIconTapCommand}"
        LeadingIcon="ic_back_b.png"
        ScrollViewName="scrollView"
        Type="Large" />
```

### C# sample

```csharp
var topAppBar = new MaterialTopAppBar
{
    Headline = "Large type",
    Description = "Description text",
    LeadingIconCommand = LeadingIconTap,
    LeadingIcon = "ic_back_b.png",
    ScrollViewName = "scrollView",
    Type = MaterialTopAppBarType.Large,
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/TopAppBarPage.xaml)

## Properties

### <a id="properties-busyindicatorcolor"/>**BusyIndicatorColor**

Gets or sets the  for the busy indicators.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Primary - Dark = MaterialDarkTheme.Primary

<br>

### <a id="properties-busyindicatorsize"/>**BusyIndicatorSize**

Gets or sets the size for the busy indicators.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 24.0

<br>

### <a id="properties-description"/>**Description**

Gets or sets the description text displayed on the top app bar. 
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-descriptioncolor"/>**DescriptionColor**

Gets or sets the  for the description text of the top app bar. 
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Text - Dark = MaterialDarkTheme.Text

<br>

### <a id="properties-descriptionfontattributes"/>**DescriptionFontAttributes**

Gets or sets a value that indicates whether the font of this top app bar description text is bold, italic, or neither.
 This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

Default value: FontAttributes.None

<br>

### <a id="properties-descriptionfontfamily"/>**DescriptionFontFamily**

Gets or sets the font family for the description text of this top app bar. 
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-descriptionfontsize"/>**DescriptionFontSize**

Gets or sets the size of the font for the description text of this top app bar. 
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.BodyMedium / Tablet = 17 / Phone = 14

<br>

### <a id="properties-descriptionmarginadjustment"/>**DescriptionMarginAdjustment**

Allows you to adjust the margins of the description text. 
 This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: new Thickness(10, 8, 10, 16)

Remarks: This property does not take into account the Left and Right of the set , it only applies the Top and Bottom values.

<br>

### <a id="properties-headline"/>**Headline**

Gets or sets the headline text displayed on the top app bar.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-headlinecolor"/>**HeadlineColor**

Gets or sets the  for the headline text of the top app bar. 
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.Text - Dark = MaterialDarkTheme.Text

<br>

### <a id="properties-headlinefontattributes"/>**HeadlineFontAttributes**

Gets or sets a value that indicates whether the font of this top app bar headline text is bold, italic, or neither.
 This is a bindable property.

Property type: [FontAttributes](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.fontattributes)<br>

Default value: FontAttributes.None

<br>

### <a id="properties-headlinefontfamily"/>**HeadlineFontFamily**

Gets or sets the font family for the headline text of this top app bar. 
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: MaterialFontFamily.Default

<br>

### <a id="properties-headlinefontsize"/>**HeadlineFontSize**

Gets or sets the size of the font for the headline text of this top app bar. 
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: MaterialFontSize.LabelLarge / Tablet: 14 - Phone: 11

<br>

### <a id="properties-headlinemarginadjustment"/>**HeadlineMarginAdjustment**

Allows you to adjust the margins of the headline text. 
 This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: default(Thickness)

Remarks: This property does not take into account the Left and Right of the set , it only applies the Top and Bottom values.

<br>

### <a id="properties-iconbuttontouchanimation"/>**IconButtonTouchAnimation**

Gets or sets a custom animation to be executed when leading and trailing icon button are clicked. 
 This is a bindable property.

Property type: ITouchAnimation<br>

Default value: null

<br>

### <a id="properties-iconbuttontouchanimationtype"/>**IconButtonTouchAnimationType**

Gets or sets an animation to be executed when leading and trailing icon button are clicked.
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

### <a id="properties-iconpadding"/>**IconPadding**

Gets or sets the padding of the MaterialTopAppBar.LeadingIcon and MaterialTopAppBar.TrailingIcons of this top app bar.
 This is a bindable property.

Property type: [Thickness](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.thickness)<br>

Default value: 12

<br>

### <a id="properties-iconsize"/>**IconSize**

Gets or sets the size of the MaterialTopAppBar.LeadingIcon and MaterialTopAppBar.TrailingIcons of this top app bar.
 This is a bindable property.

Property type: [Double](https://learn.microsoft.com/en-us/dotnet/api/system.double)<br>

Default value: 48.0

<br>

### <a id="properties-iscollapsed"/>**IsCollapsed**

Indicates if app bar is collapsed or not. 
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

<br>

### <a id="properties-leadingicon"/>**LeadingIcon**

Allows you to display an icon button on the left side of the top app bar. 
 This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Default value: null

<br>

### <a id="properties-leadingiconcommand"/>**LeadingIconCommand**

Gets or sets the command to invoke when the leading icon button is clicked. 
 This is a bindable property.

Property type: ICommand<br>

Default value: null

Remarks: This property is used to associate a command with an instance of a top app bar. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.

<br>

### <a id="properties-leadingiconisbusy"/>**LeadingIconIsBusy**

Gets or sets if the leading icon button is on busy state (executing Command). 
 This is a bindable property.

Property type: [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: false

<br>

### <a id="properties-scrollviewanimationlength"/>**ScrollViewAnimationLength**

Gets or sets the duration of the collapse or expand animation bound to the  element. 
 This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: 250

<br>

### <a id="properties-scrollviewname"/>**ScrollViewName**

Gets or sets the name of the  element to which the top app bar will be linked to run collapse or expand animations depending on the user's scroll.
 This is a bindable property.

Property type: [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

Default value: null

<br>

### <a id="properties-trailingicons"/>**TrailingIcons**

Allows you to display a list of icon buttons on the right side of the top app bar. 
 This is a bindable property.

Property type: [IList](https://learn.microsoft.com/en-us/dotnet/api/system.collections.ilist)<br>

Default value: null

Remarks: This property supports a maximum of 3 icon buttons.

<br>

### <a id="properties-type"/>**Type**

Gets or sets the top app bar type according to MaterialTopAppBarType enum.
 This is a bindable property.

Property type: MaterialTopAppBarType<br>

| Name | Value | Description |
| --- | --: | --- |
| CenterAligned | 0 | Center and aligned headline |
| Small | 1 | Small headline below the leading icon |
| Medium | 2 | Medium headline below the leading icon |
| Large | 3 | Large headline below the leading icon |

Default value: MaterialTopAppBarType.CenterAligned

Remarks:

- <para>CenterAligned: Center and aligned headline.</para>

- <para>Small: Small headline below the leading icon.</para>

- <para>Medium: Medium headline below the leading icon.</para>

- <para>Large: Large headline below the leading icon.</para>

<br>

## Known issues and pending features

* [iOS] The scroll animation has lag by the headline size change
