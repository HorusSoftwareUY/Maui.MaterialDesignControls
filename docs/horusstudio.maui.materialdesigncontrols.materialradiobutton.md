# MaterialRadioButton

Namespace: HorusStudio.Maui.MaterialDesignControls

A RadioButton  let people select one option from a set of options and follows Material Design Guidelines .
 We reuse some code from MAUI official repository: https://github.com/dotnet/maui/blob/7076514d83f7e16ac49838307aefd598b45adcec/src/Controls/src/Core/RadioButton/RadioButton.cs

```csharp
public class MaterialRadioButton : Microsoft.Maui.Controls.ContentView, System.ComponentModel.INotifyPropertyChanged, Microsoft.Maui.Controls.Internals.IDynamicResourceHandler, Microsoft.Maui.Controls.IElementDefinition, Microsoft.Maui.Controls.Internals.INameScope, Microsoft.Maui.Controls.IElementController, Microsoft.Maui.IVisualTreeElement, Microsoft.Maui.IElement, Microsoft.Maui.Controls.IEffectControlProvider, Microsoft.Maui.IToolTipElement, Microsoft.Maui.IContextFlyoutElement, Microsoft.Maui.Controls.IControlsElement, Microsoft.Maui.Controls.StyleSheets.IStyleSelectable, Microsoft.Maui.Controls.Internals.INavigationProxy, Microsoft.Maui.Controls.IAnimatable, Microsoft.Maui.Controls.IVisualElementController, Microsoft.Maui.Controls.IResourcesProvider, Microsoft.Maui.Controls.IStyleElement, Microsoft.Maui.Controls.IFlowDirectionController, Microsoft.Maui.Controls.IPropertyPropagationController, Microsoft.Maui.Controls.IVisualController, Microsoft.Maui.Controls.IWindowController, Microsoft.Maui.IView, Microsoft.Maui.ITransform, Microsoft.Maui.Controls.IControlsVisualElement, Microsoft.Maui.Controls.StyleSheets.IStylable, Microsoft.Maui.Controls.IViewController, Microsoft.Maui.Controls.Internals.IGestureController, Microsoft.Maui.Controls.IGestureRecognizers, Microsoft.Maui.IPropertyMapperView, Microsoft.Maui.HotReload.IHotReloadableView, Microsoft.Maui.IReplaceableView, Microsoft.Maui.Controls.IControlsView, Microsoft.Maui.Controls.ILayout, Microsoft.Maui.Controls.ILayoutController, Microsoft.Maui.Controls.IPaddingElement, Microsoft.Maui.Controls.IInputTransparentContainerElement, Microsoft.Maui.Controls.IControlTemplated, Microsoft.Maui.IContentView, Microsoft.Maui.IPadding, Microsoft.Maui.ICrossPlatformLayout, HorusStudio.Maui.MaterialDesignControls.Behaviors.ITouchable
```

## Properties

### <a id="properties-animation"/>**Animation**

Gets or sets an animation to be executed when radio button is clicked.
 The default value is [AnimationTypes.Fade](./horusstudio.maui.materialdesigncontrols.animationtypes.md#fade).
 This is a bindable property.

Property Value: [AnimationTypes](./horusstudio.maui.materialdesigncontrols.animationtypes.md)<br>

### <a id="properties-animationparameter"/>**AnimationParameter**

Gets or sets the parameter to pass to the [MaterialRadioButton.Animation](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#animation) property.
 The default value is .
 This is a bindable property.

Property Value: [Nullable&lt;Double&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>

### <a id="properties-characterspacing"/>**CharacterSpacing**

Gets or sets the spacing between characters of the label. This is a bindable property.

Property Value: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-commandcheckedchanged"/>**CommandCheckedChanged**

Gets or sets the command to invoke when the radio button changes its status. This is a bindable property.

Property Value: ICommand<br>

**Remarks:**

This property is used to associate a command with an instance of a radio button. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.  is controlled by the  if set.

### <a id="properties-commandcheckedchangedparameter"/>**CommandCheckedChangedParameter**

Gets or sets the parameter to pass to the [MaterialRadioButton.CommandCheckedChangedParameter](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#commandcheckedchangedparameter) property.
 The default value is . This is a bindable property.

Property Value: [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

### <a id="properties-content"/>**Content**

Gets the [MaterialRadioButton.Content](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#content) for the RadioButton. This is a bindable property.
 We disabled the set for this property because doesn't have sense set the content because we are setting with the
 radio button and label.

Property Value: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-controltemplate"/>**ControlTemplate**

Gets or sets the [MaterialRadioButton.ControlTemplate](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#controltemplate) for the radio button. This is a bindable property.

Property Value: ControlTemplate<br>

### <a id="properties-customanimation"/>**CustomAnimation**

Gets or sets a custom animation to be executed when radio button is clicked.
 The default value is .
 This is a bindable property.

Property Value: [ICustomAnimation](./horusstudio.maui.materialdesigncontrols.icustomanimation.md)<br>

### <a id="properties-fontattributes"/>**FontAttributes**

Gets or sets the text style of the label. This is a bindable property.

Property Value: FontAttributes<br>

### <a id="properties-fontautoscalingenabled"/>**FontAutoScalingEnabled**

Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true

Property Value: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-fontfamily"/>**FontFamily**

Gets or sets the font family for the label. This is a bindable property.

Property Value: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-fontsize"/>**FontSize**

Defines the font size of the label. This is a bindable property.

Property Value: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-groupname"/>**GroupName**

Gets or sets the [String](https://docs.microsoft.com/en-us/dotnet/api/system.string) GroupName for the radio button. 
 The default value is .
 This is a bindable property.

Property Value: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-ischecked"/>**IsChecked**

Gets or sets [MaterialRadioButton.IsChecked](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#ischecked) for the radio button. 
 This is a bindable property.

Property Value: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-isenabled"/>**IsEnabled**

Gets or sets [MaterialRadioButton.IsEnabled](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#isenabled) for the radio button. This is a bindable property.

Property Value: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-strokecolor"/>**StrokeColor**

Gets or sets the for the stroke of the radio button. This is a bindable property.

Property Value: Color<br>

### <a id="properties-text"/>**Text**

Gets or sets the [MaterialRadioButton.Text](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#text) for the label. This is a bindable property.

Property Value: [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)<br>

### <a id="properties-textcolor"/>**TextColor**

Gets or sets the [MaterialRadioButton.TextColor](./horusstudio.maui.materialdesigncontrols.materialradiobutton.md#textcolor) for the text of the label. This is a bindable property.

Property Value: Color<br>

### <a id="properties-textside"/>**TextSide**

Defines the location of the label. 
 The default value is [TextSide.Left](./horusstudio.maui.materialdesigncontrols.enums.textside.md#left)
 This is a bindable property.

Property Value: [TextSide](./horusstudio.maui.materialdesigncontrols.enums.textside.md)<br>

### <a id="properties-texttransform"/>**TextTransform**

Defines the casing of the label. This is a bindable property.

Property Value: TextTransform<br>

### <a id="properties-value"/>**Value**

Defines the value of radio button selected
 The default value is 
 This is a bindable property.

Property Value: [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)<br>

## Constructors

### <a id="constructors-.ctor"/>**MaterialRadioButton()**

```csharp
public MaterialRadioButton()
```

## Events
