# MaterialFloatingButton

Floating action buttons (FABs) help people take primary actions [see here.](https://m3.material.io/components/floating-action-button/overview)

Namespace: HorusStudio.Maui.MaterialDesignControls

Inherits from: MaterialFloatingButton → [ContentView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.contentview)

<br>

![](https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialFloatingButton.gif)

### XAML sample

```csharp
xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"

<material:MaterialFloatingButton
      Icon="IconButton"
      ActionCommand="{Binding FloatingButtonActionCommand}"
      x:Name="MaterialFloatingButton"/>
```

### C# sample

```csharp
var MaterialFloatingButton = new MaterialFloatingButton()
{
    Icon = "IconButton",
    ActionCommand = ActionCommand
};
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/FloatingButtonPage.xaml)

## Properties

### <a id="properties-actioncommand"/>**ActionCommand**

Gets or sets command when press floating button
 This is a bindable property.

Property type: ICommand<br>

Default value: Null

Remarks: This property is used to associate a command with an instance of FAB. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.

- <para>
  <see cref="P:Microsoft.Maui.Controls.VisualElement.IsEnabled">VisualElement.IsEnabled</see> is controlled by the <see cref="M:Microsoft.Maui.Controls.Command.CanExecute(System.Object)">Command.CanExecute(object)</see> if set.</para>

<br>

### <a id="properties-actioncommandparameter"/>**ActionCommandParameter**

Gets or sets the parameter to pass to the MaterialFloatingButton.ActionCommand property.
 This is a bindable property.

Property type: [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>

Default value: Null

<br>

### <a id="properties-backgroundcolor"/>**BackgroundColor**

Gets or sets background color floating button
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.PrimaryContainer - Dark = MaterialDarkTheme.PrimaryContainer

<br>

### <a id="properties-cornerradius"/>**CornerRadius**

Gets or sets corners in floating button
 This is a bindable property.

Property type: [CornerRadius](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.cornerradius)<br>

Default value: CornerRadius(16)

<br>

### <a id="properties-icon"/>**Icon**

Gets or sets icon in floating button
 This is a bindable property.

Property type: [ImageSource](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.controls.imagesource)<br>

Default value: Null

<br>

### <a id="properties-iconcolor"/>**IconColor**

Gets or sets icon color in floating button
 This is a bindable property.

Property type: [Color](https://learn.microsoft.com/en-us/dotnet/api/microsoft.maui.graphics.color)<br>

Default value: Theme: Light = MaterialLightTheme.OnPrimaryContainer - Dark = MaterialDarkTheme.OnPrimaryContainer

<br>

### <a id="properties-iconsize"/>**IconSize**

Gets or sets size Icon Size
 This is a bindable property.

Property type: [Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

Default value: 24

<br>

### <a id="properties-position"/>**Position**

Gets or sets Position button
 This is a bindable property.

Property type: MaterialFloatingButtonPosition<br>

| Name | Value | Description |
| --- | --: | --- |
| TopLeft | 0 | Top left |
| TopRight | 1 | Top right |
| BottomRight | 2 | Bottom right |
| BottomLeft | 3 | Bottom left |

Default value: MaterialFloatingButtonPosition.BottomRight

<br>

### <a id="properties-type"/>**Type**

Gets or sets Type button
 This is a bindable property.

Property type: MaterialFloatingButtonType<br>

| Name | Value | Description |
| --- | --: | --- |
| FAB | 0 | Use a FAB to represent the screen’s primary action |
| Small | 1 | A small FAB is used for a secondary, supporting action, or in place of a default FAB in compact window sizes |
| Large | 2 | A large FAB is useful when the layout calls for a clear and prominent primary action, and where a larger footprint would help the user engage |

Default value: MaterialFloatingButtonType.FAB

<br>
