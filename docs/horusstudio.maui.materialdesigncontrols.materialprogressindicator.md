# MaterialProgressIndicator

A progress indicator  that show the status of a process and follows Material Design Guidelines.

Namespace: HorusStudio.Maui.MaterialDesignControls

## Properties

### <a id="properties-heightrequest"/>**HeightRequest**

Gets or sets height of the progress indicator. This is a bindable property.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>

### <a id="properties-indicatorcolor"/>**IndicatorColor**

Gets or sets the  for the active indicator of the progress indicator. This is a bindable property.

Property type: Color<br>

### <a id="properties-isvisible"/>**IsVisible**

Gets or sets if progress indicator is visible.
 This is a bindable property.

Property type: [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)<br>

Default value: True

### <a id="properties-trackcolor"/>**TrackColor**

Gets or sets the  for the track of the progress indicator. This is a bindable property.

Property type: Color<br>

Remarks: This property will not have an effect unless [MaterialProgressIndicator.Type](./horusstudio.maui.materialdesigncontrols.materialprogressindicator.md#type) is set to [MaterialProgressIndicatorType.Linear](./horusstudio.maui.materialdesigncontrols.materialprogressindicatortype.md#linear).

### <a id="properties-type"/>**Type**

Gets or sets the progress indicator type according to [MaterialProgressIndicatorType](./horusstudio.maui.materialdesigncontrols.materialprogressindicatortype.md) enum.
 This is a bindable property.

Property type: [MaterialProgressIndicatorType](./horusstudio.maui.materialdesigncontrols.materialprogressindicatortype.md)<br>

| Name | Value | Description |
| --- | --: | --- |
| Circular | 0 | Circular |
| Linear | 1 | Linear |

Default value: [MaterialProgressIndicatorType.Circular](./horusstudio.maui.materialdesigncontrols.materialprogressindicatortype.md#circular)

### <a id="properties-widthrequest"/>**WidthRequest**

Gets or sets width of the progress indicator. This is a bindable property.

Property type: [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double)<br>
