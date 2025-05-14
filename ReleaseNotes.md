# Release Notes <!-- omit from toc -->

## Content table <!-- omit from toc -->
- [Versions](#versions)
  - [1.1.0](#110)
  - [1.0.0-preview.6](#100-preview6)
  - [1.0.0-preview.5](#100-preview5)
  - [1.0.0-preview.4](#100-preview4)
  - [1.0.0-preview.3](#100-preview3)
  - [1.0.0-preview.2](#100-preview2)
  - [1.0.0-preview.1](#100-preview1)

## Versions
### [1.1.0](https://www.nuget.org/packages/HorusStudio.Maui.MaterialDesignControls/1.1.0)
* Configure if icon tint color should be automatically set to TextColor or not on: MaterialButton, MaterialChips, MaterialIconButton and MaterialNavigationDrawerItem
* Fix TextChanged and TextChangedCommand not being fired on MaterialTextField and MaterialMultilineTextField
* Expose internal MAUI control used on: MaterialButton, MaterialCheckBox, MaterialDatePicker, MaterialMultilineTextField, MaterialPicker, MaterialRadioButton, MaterialSlider, MaterialTextField and MaterialTimePicker
* Fix spacing on MaterialRating when label is empty
* Fix touch bubbling on iOS for: MaterialButton, MaterialDatePicker, MaterialIconButton, MaterialMultilineTextField, MaterialPicker, MaterialSwitch, MaterialTextField and MaterialTimePicker
* Ability to override custom handlers for: MaterialButton, MaterialRadioButton, MaterialTextField, MaterialTimePicker, MaterialDatePicker, MaterialPicker, MaterialMultilineTextField, MaterialCheckbox and MaterialSlider
  
### [1.0.0-preview.6](https://www.nuget.org/packages/HorusStudio.Maui.MaterialDesignControls/1.0.0-preview.6)
* Add LineBreakMode to inputs placeholder
* Add TextDecorations to Snackbar action
* [iOS] Bug: Fix Snackbar background color
  
### [1.0.0-preview.5](https://www.nuget.org/packages/HorusStudio.Maui.MaterialDesignControls/1.0.0-preview.5)
* Bug: Outlined inputs not rendered when overriding global style
* Bug: Tint color fixed on MaterialIconButton
* Bug: ConfigureSnackbar throw NullReferenceException if BackgroundColor was not set
* [iOS] Bug: Leading and trailing icons width on Snackbar
 
### [1.0.0-preview.4](https://www.nuget.org/packages/HorusStudio.Maui.MaterialDesignControls/1.0.0-preview.4)
* [Android] Bug #49: MaterialFloatingButton does not appear.

### [1.0.0-preview.3](https://www.nuget.org/packages/HorusStudio.Maui.MaterialDesignControls/1.0.0-preview.3)
* Fix events on MaterialButton.
* Fix touch on MaterialCard.
* Fix events on MaterialIconButton.
* Expand touchable area on MaterialSwitch.
* Add events to MaterialViewButton.
* Fix default icons on MaterialPicker, MaterialDatePicker and MaterialTimePicker.
* Handle image loading error on MaterialIconButton.
* Support iOS/MacCatalyst 17.0+.

### [1.0.0-preview.2](https://www.nuget.org/packages/HorusStudio.Maui.MaterialDesignControls/1.0.0-preview.2)
* Bug fixing and improvements.

### [1.0.0-preview.1](https://www.nuget.org/packages/HorusStudio.Maui.MaterialDesignControls/1.0.0-preview.1)
First release for Android, iOS and MacCatalyst containing:
* MaterialBadge
* MaterialButton
* MaterialCard
* MaterialCheckbox
* MaterialChips
* MaterialChipsGroup
* MaterialDatePicker
* MaterialDivider
* MaterialFloatingButton
* MaterialIconButton
* MaterialLabel
* MaterialMultilineTextField
* MaterialNavigationDrawer
* MaterialPicker
* MaterialProgressIndicator
* MaterialRadioButton
* MaterialRating
* MaterialSelection
* MaterialSlider
* MaterialSwitch
* MaterialSnackbar
* MaterialTextField
* MaterialTimePicker
* MaterialTopAppBar
* MaterialViewButton

