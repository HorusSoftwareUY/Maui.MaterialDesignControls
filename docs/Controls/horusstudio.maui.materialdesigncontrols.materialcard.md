# MaterialCard

A card  that display content and actions about a single subject, and follows Material Design Guidelines [See here](https://m3.material.io/components/cards/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialCard → [Border](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.border)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialCard.jpg)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialCard
        Type="Elevated">
            <VerticalStackLayout
            Spacing="8">
                <material:MaterialLabel
                    Type="TitleMedium"
                    Text="Elevated type"/>
                <material:MaterialLabel
                    Text="Elevated cards provide separation from a patterned background."/>
            </VerticalStackLayout>
 </material:MaterialCard/>
```

### C# sample

```csharp
var label = new MaterialLabel()
{
    Text = "This a card."
};

var vStack = new VerticalStackLayout()
{
    label
};
    
var card = new MaterialCard()
{
    Type = MaterialCardType.Elevated,
    Content = vStack
};
```

## Properties

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when card is clicked.
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

Gets or sets the parameter to pass to the MaterialCard.Animation property.
 This is a bindable property.

Property type: [Nullable&lt;Double&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

Default value: null

<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a color that describes the background color of the card.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets a color that describes the border stroke color of the card.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Remarks: This property has no effect if  is set to 0.

<br>

### <a id="properties-borderwidth"/>**BorderWidth**

Gets or sets the width of the border, in device-independent units.
 This is a bindable property.

Property type: [Single](https://learn.microsoft.com/en-us/dotnet/api/system.single)<br>

Remarks: Set this value to a non-zero value in order to have a visible border.

<br>

### <a id="properties-command"/>**Command**

Gets or sets the command to invoke when the card is clicked. This is a bindable property.

Property type: ICommand<br>

Remarks: This property is used to associate a command with an instance of a card. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.  is controlled by the  if set.

<br>

### <a id="properties-commandparameter"/>**CommandParameter**

Gets or sets the parameter to pass to the MaterialCard.Command property.
 This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: null.

<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the card, in device-independent units.
 This is a bindable property.

Property type: [CornerRadius](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.cornerradius)<br>

<br>

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when card is clicked.
 This is a bindable property.

Property type: ICustomAnimation<br>

Default value: null

<br>

### <a id="properties-shadow"/>**Shadow**

Gets or sets the shadow effect cast by the element.
 This is a bindable property.

Property type: [Shadow](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.shadow)<br>

<br>

### <a id="properties-shadowcolor"/>**ShadowColor**

Gets or sets a color that describes the shadow color of the card.
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

<br>

### <a id="properties-type"/>**Type**

Gets or sets the card type according to MaterialCardType enum.
 This is a bindable property.

Property type: MaterialCardType<br>

| Name | Value | Description |
| --- | --: | --- |
| Elevated | 0 | Elevated |
| Filled | 1 | Filled |
| Outlined | 2 | Outlined |
| Custom | 3 | Custom |

Default value: MaterialCardType.Filled

<br>

## Events

### <a id="events-clicked"/>**Clicked**

Occurs when the card is clicked/tapped.

<br>

### <a id="events-pressed"/>**Pressed**

Occurs when the card is pressed.

<br>

### <a id="events-released"/>**Released**

Occurs when the card is released.

<br>

## Known issues and pending features

- Disable color styles looks a bit weird with the opacities that the guideline specifies, we have to review them
