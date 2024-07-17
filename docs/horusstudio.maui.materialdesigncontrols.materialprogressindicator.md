# MaterialProgressIndicator

Namespace: HorusStudio.Maui.MaterialDesignControls

A progress indicator  that show the status of a process and follows Material Design Guidelines.

```csharp
public class MaterialProgressIndicator : Microsoft.Maui.Controls.ContentView, System.ComponentModel.INotifyPropertyChanged, Microsoft.Maui.Controls.Internals.IDynamicResourceHandler, Microsoft.Maui.Controls.IElementDefinition, Microsoft.Maui.Controls.Internals.INameScope, Microsoft.Maui.Controls.IElementController, Microsoft.Maui.IVisualTreeElement, Microsoft.Maui.IElement, Microsoft.Maui.Controls.IEffectControlProvider, Microsoft.Maui.IToolTipElement, Microsoft.Maui.IContextFlyoutElement, Microsoft.Maui.Controls.IControlsElement, Microsoft.Maui.Controls.StyleSheets.IStyleSelectable, Microsoft.Maui.Controls.Internals.INavigationProxy, Microsoft.Maui.Controls.IAnimatable, Microsoft.Maui.Controls.IVisualElementController, Microsoft.Maui.Controls.IResourcesProvider, Microsoft.Maui.Controls.IStyleElement, Microsoft.Maui.Controls.IFlowDirectionController, Microsoft.Maui.Controls.IPropertyPropagationController, Microsoft.Maui.Controls.IVisualController, Microsoft.Maui.Controls.IWindowController, Microsoft.Maui.IView, Microsoft.Maui.ITransform, Microsoft.Maui.Controls.IControlsVisualElement, Microsoft.Maui.Controls.StyleSheets.IStylable, Microsoft.Maui.Controls.IViewController, Microsoft.Maui.Controls.Internals.IGestureController, Microsoft.Maui.Controls.IGestureRecognizers, Microsoft.Maui.IPropertyMapperView, Microsoft.Maui.HotReload.IHotReloadableView, Microsoft.Maui.IReplaceableView, Microsoft.Maui.Controls.IControlsView, Microsoft.Maui.Controls.ILayout, Microsoft.Maui.Controls.ILayoutController, Microsoft.Maui.Controls.IPaddingElement, Microsoft.Maui.Controls.IInputTransparentContainerElement, Microsoft.Maui.Controls.IControlTemplated, Microsoft.Maui.IContentView, Microsoft.Maui.IPadding, Microsoft.Maui.ICrossPlatformLayout
```

## Properties

### <a id="properties-heightrequest"/>**HeightRequest**

Gets or sets height of the progress indicator. This is a bindable property.

Property Value: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-indicatorcolor"/>**IndicatorColor**

Gets or sets the for the active indicator of the progress indicator. This is a bindable property.

Property Value: Color<br>

### <a id="properties-isvisible"/>**IsVisible**

Gets or sets if progress indicator is visible.
 The default value is .
 This is a bindable property.

Property Value: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

### <a id="properties-trackcolor"/>**TrackColor**

Gets or sets the for the track of the progress indicator. This is a bindable property.

Property Value: Color<br>

**Remarks:**

This property will not have an effect unless [MaterialProgressIndicator.Type](./horusstudio.maui.materialdesigncontrols.materialprogressindicator.md#type) is set to [MaterialProgressIndicatorType.Linear](./horusstudio.maui.materialdesigncontrols.materialprogressindicatortype.md#linear).

### <a id="properties-type"/>**Type**

Gets or sets the progress indicator type according to [MaterialProgressIndicatorType](./horusstudio.maui.materialdesigncontrols.materialprogressindicatortype.md) enum.
 The default value is [MaterialProgressIndicatorType.Circular](./horusstudio.maui.materialdesigncontrols.materialprogressindicatortype.md#circular). This is a bindable property.

Property Value: [MaterialProgressIndicatorType](./horusstudio.maui.materialdesigncontrols.materialprogressindicatortype.md)<br>

### <a id="properties-widthrequest"/>**WidthRequest**

Gets or sets width of the progress indicator. This is a bindable property.

Property Value: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

## Constructors

### <a id="constructors-.ctor"/>**MaterialProgressIndicator()**

```csharp
public MaterialProgressIndicator()
```

## Events
