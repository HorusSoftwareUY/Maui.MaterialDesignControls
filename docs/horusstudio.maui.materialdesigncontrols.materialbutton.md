# MaterialButton

Namespace: HorusStudio.Maui.MaterialDesignControls

A button  that reacts to touch events and follows Material Design Guidelines .

```csharp
public class MaterialButton : Microsoft.Maui.Controls.ContentView, System.ComponentModel.INotifyPropertyChanged, Microsoft.Maui.Controls.Internals.IDynamicResourceHandler, Microsoft.Maui.Controls.IElementDefinition, Microsoft.Maui.Controls.Internals.INameScope, Microsoft.Maui.Controls.IElementController, Microsoft.Maui.IVisualTreeElement, Microsoft.Maui.IElement, Microsoft.Maui.Controls.IEffectControlProvider, Microsoft.Maui.IToolTipElement, Microsoft.Maui.IContextFlyoutElement, Microsoft.Maui.Controls.IControlsElement, Microsoft.Maui.Controls.StyleSheets.IStyleSelectable, Microsoft.Maui.Controls.Internals.INavigationProxy, Microsoft.Maui.Controls.IAnimatable, Microsoft.Maui.Controls.IVisualElementController, Microsoft.Maui.Controls.IResourcesProvider, Microsoft.Maui.Controls.IStyleElement, Microsoft.Maui.Controls.IFlowDirectionController, Microsoft.Maui.Controls.IPropertyPropagationController, Microsoft.Maui.Controls.IVisualController, Microsoft.Maui.Controls.IWindowController, Microsoft.Maui.IView, Microsoft.Maui.ITransform, Microsoft.Maui.Controls.IControlsVisualElement, Microsoft.Maui.Controls.StyleSheets.IStylable, Microsoft.Maui.Controls.IViewController, Microsoft.Maui.Controls.Internals.IGestureController, Microsoft.Maui.Controls.IGestureRecognizers, Microsoft.Maui.IPropertyMapperView, Microsoft.Maui.HotReload.IHotReloadableView, Microsoft.Maui.IReplaceableView, Microsoft.Maui.Controls.IControlsView, Microsoft.Maui.Controls.ILayout, Microsoft.Maui.Controls.ILayoutController, Microsoft.Maui.Controls.IPaddingElement, Microsoft.Maui.Controls.IInputTransparentContainerElement, Microsoft.Maui.Controls.IControlTemplated, Microsoft.Maui.IContentView, Microsoft.Maui.IPadding, Microsoft.Maui.ICrossPlatformLayout, HorusStudio.Maui.MaterialDesignControls.Behaviors.ITouchable
```

## Properties

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when button is clicked.
 The default value is [AnimationTypes.Fade](./horusstudio.maui.materialdesigncontrols.animationtypes.md#fade).
 This is a bindable property.

Property Value: [AnimationTypes](./horusstudio.maui.materialdesigncontrols.animationtypes.md)<br>

### <a id="properties-animationparameter"/>**AnimationParameter**

Gets or sets the parameter to pass to the [MaterialButton.Animation](./horusstudio.maui.materialdesigncontrols.materialbutton.md#animation) property.
 The default value is .
 This is a bindable property.

Property Value: [Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### <a id="properties-background"/>**Background**

Gets or sets a  that describes the background of the button. This is a bindable property.

Property Value: Brush<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets a color that describes the background color of the button. This is a bindable property.

Property Value: Color<br>

### <a id="properties-bordercolor"/>**BorderColor**

Gets or sets a color that describes the border stroke color of the button. This is a bindable property.

Property Value: Color<br>

**Remarks:**

This property has no effect if  is set to 0. On Android this property will not have an effect unless  is set to a non-default color.

### <a id="properties-borderwidth"/>**BorderWidth**

Gets or sets the width of the border, in device-independent units. This is a bindable property.

Property Value: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

**Remarks:**

Set this value to a non-zero value in order to have a visible border.

### <a id="properties-busyindicatorcolor"/>**BusyIndicatorColor**

Gets or sets the for the busy indicator.
 This is a bindable property.

Property Value: Color<br>

### <a id="properties-busyindicatorsize"/>**BusyIndicatorSize**

Gets or sets the size for the busy indicator.
 This is a bindable property.

Property Value: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-characterspacing"/>**CharacterSpacing**

Gets or sets the spacing between each of the characters of [MaterialButton.Text](./horusstudio.maui.materialdesigncontrols.materialbutton.md#text) when displayed on the button.
 This is a bindable property.

Property Value: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-command"/>**Command**

Gets or sets the command to invoke when the button is activated. This is a bindable property.

Property Value: ICommand<br>

**Remarks:**

This property is used to associate a command with an instance of a button. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.  is controlled by the  if set.

### <a id="properties-commandparameter"/>**CommandParameter**

Gets or sets the parameter to pass to the [MaterialButton.Command](./horusstudio.maui.materialdesigncontrols.materialbutton.md#command) property.
 The default value is . This is a bindable property.

Property Value: [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### <a id="properties-contentlayout"/>**ContentLayout**

Gets or sets an object that controls the position of the button image and the spacing between the button's image and the button's text.
 This is a bindable property.

Property Value: ButtonContentLayout<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets the corner radius for the button, in device-independent units. This is a bindable property.

Property Value: [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)<br>

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when button is clicked.
 The default value is .
 This is a bindable property.

Property Value: [ICustomAnimation](./horusstudio.maui.materialdesigncontrols.icustomanimation.md)<br>

### <a id="properties-custombusyindicator"/>**CustomBusyIndicator**

Gets or sets a custom  for busy indicator.
 This is a bindable property.

Property Value: View<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets a value that indicates whether the font for the text of this button is bold, italic, or neither.
 This is a bindable property.

Property Value: FontAttributes<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Determines whether or not the font of this entry should scale automatically according to the operating system settings. Default value is .
 This is a bindable property.

Property Value: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

**Remarks:**

Typically this should always be enabled for accessibility reasons.

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the text of this entry. This is a bindable property.

Property Value: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-fontsize"/>**FontSize**

Gets or sets the size of the font for the text of this entry. This is a bindable property.

Property Value: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-heightrequest"/>**HeightRequest**

Gets or sets the desired height override of this element. This is a bindable property.

Property Value: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

**Remarks:**

The default value is -1, which means the value is unset; the effective minimum height will be zero.

[MaterialButton.HeightRequest](./horusstudio.maui.materialdesigncontrols.materialbutton.md#heightrequest) does not immediately change the Bounds of an element; setting the [MaterialButton.HeightRequest](./horusstudio.maui.materialdesigncontrols.materialbutton.md#heightrequest) will change the resulting height of the element during the next layout pass.

### <a id="properties-icontintcolor"/>**IconTintColor**

Gets or sets the for the text of the button. This is a bindable property.

Property Value: Color<br>

### <a id="properties-imagesource"/>**ImageSource**

Allows you to display a bitmap image on the Button. This is a bindable property.

Property Value: ImageSource<br>

**Remarks:**

For more options have a look at .

### <a id="properties-isbusy"/>**IsBusy**

Gets or sets if button is on busy state (executing Command).
 The default value is .
 This is a bindable property.

Property Value: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-linebreakmode"/>**LineBreakMode**

Determines how [MaterialButton.Text](./horusstudio.maui.materialdesigncontrols.materialbutton.md#text) is shown when the length is overflowing the size of this button.
 This is a bindable property.

Property Value: LineBreakMode<br>

### <a id="properties-padding"/>**Padding**

Gets or sets the padding for the button. This is a bindable property.

Property Value: Thickness<br>

### <a id="properties-shadow"/>**Shadow**

Gets or sets the shadow effect cast by the element. This is a bindable property.
 This is a bindable property.

Property Value: Shadow<br>

### <a id="properties-text"/>**Text**

Gets or sets the text displayed as the content of the button.
 The default value is . This is a bindable property.

Property Value: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

Changing the text of a button will trigger a layout cycle.

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the for the text of the button. This is a bindable property.

Property Value: Color<br>

### <a id="properties-textdecorations"/>**TextDecorations**

Gets or sets [MaterialButton.TextDecorations](./horusstudio.maui.materialdesigncontrols.materialbutton.md#textdecorations) for the text of the button.
 This is a bindable property.

Property Value: TextDecorations<br>

### <a id="properties-texttransform"/>**TextTransform**

Applies text transformation to the [MaterialButton.Text](./horusstudio.maui.materialdesigncontrols.materialbutton.md#text) displayed on this button.
 This is a bindable property.

Property Value: TextTransform<br>

### <a id="properties-tintcolor"/>**TintColor**

Gets or sets the for the text of the button. This is a bindable property.

Property Value: Color<br>

### <a id="properties-type"/>**Type**

Gets or sets the button type according to [MaterialButtonType](./horusstudio.maui.materialdesigncontrols.materialbuttontype.md) enum.
 The default value is [MaterialButtonType.Filled](./horusstudio.maui.materialdesigncontrols.materialbuttontype.md#filled). This is a bindable property.

Property Value: [MaterialButtonType](./horusstudio.maui.materialdesigncontrols.materialbuttontype.md)<br>

## Constructors

### <a id="constructors-.ctor"/>**MaterialButton()**

```csharp
public MaterialButton()
```

## Events

### <a id="events-clicked"/>**Clicked**

Occurs when the button is clicked/tapped.

### <a id="events-focused"/>**Focused**

Occurs when the button is focused.

### <a id="events-pressed"/>**Pressed**

Occurs when the button is pressed.

### <a id="events-released"/>**Released**

Occurs when the button is released.

### <a id="events-unfocused"/>**Unfocused**

Occurs when the button is unfocused.
