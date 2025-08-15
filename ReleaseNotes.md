# Release Notes <!-- omit from toc -->

## Content table <!-- omit from toc -->
- [Versions](#versions)
  - [2.0.0](#200)
  - [1.1.0](#110)
  - [1.0.0-preview.6](#100-preview6)
  - [1.0.0-preview.5](#100-preview5)
  - [1.0.0-preview.4](#100-preview4)
  - [1.0.0-preview.3](#100-preview3)
  - [1.0.0-preview.2](#100-preview2)
  - [1.0.0-preview.1](#100-preview1)

## Versions
### [2.0.0](https://www.nuget.org/packages/HorusStudio.Maui.MaterialDesignControls/2.0.0)
- Update to .NET 9
- [MaterialSegmentedButton] new control added
- [MaterialViewGroup] new control added to group controls and handle single or multiple selection
- [IValidableView] added for error animations
- [MaterialTopAppBar] Add IconPadding property
- [MaterialFloatingButton] [iOS] Fix tap issue
- Improvements with main thread
- [MaterialNavigationDrawer] Add IconSize property
- [MaterialRating] Add ValueChangedCommand and ValueChanged event
- [MaterialPicker/MaterialDatePicker/MaterialTimePicker] [iOS] Fix HorizontalTextAlignment
- [MaterialInputBase] Don't animate on tap when control is disabled
- [MaterialInputBase] Add AlwaysShowLabel property for Outlined type
- [MaterialInputBase] Add ErrorAnimationType and ErrorAnimatiion properties
- [MaterialCard] Support Transparent or null shadow
- [BREAKING CHANGE] [MaterialChipsGroup] Remove control (replaced with MaterialViewGroup)
- [BREAKING CHANGE] [MaterialRadioButtonsGroup] Remove control (replaced with MaterialViewGroup)
- [BREAKING CHANGE] [MaterialChips] Rename MaterialChip
- [BREAKING CHANGE] [MaterialChipsType] Rename to MaterialChipType
- [BREAKING CHANGE] [MaterialChip] Remove “Unselected” VisualState, “Normal” is used instead
- [BREAKING CHANGE] [MaterialChip] Remove “IconStateOnSelection” property
- [BREAKING CHANGE] [MaterialChip] Add SelectionChangedCommand and SelectionChanged event
- [BREAKING CHANGE] [ITouchable] Rename to ITouchableView
- [BREAKING CHANGE] [MaterialButton/MaterialCard/MaterialCheckbox/MaterialChip/MaterialIconButton/MaterialNavigationDrawer/MaterialRadioButton/MaterialRating/MaterialSwitch/MaterialViewButton] Rename Animation property to TouchAnimationType
- [BREAKING CHANGE] [MaterialButton/MaterialCard/MaterialCheckbox/MaterialChip/MaterialIconButton/MaterialNavigationDrawer/MaterialRadioButton/MaterialRating/MaterialSwitch/MaterialViewButton] Remove AnimationParameter property
- [BREAKING CHANGE] [MaterialButton/MaterialCard/MaterialCheckbox/MaterialChip/MaterialIconButton/MaterialNavigationDrawer/MaterialRadioButton/MaterialRating/MaterialSwitch/MaterialViewButton] Rename CustomAnimation property to TouchAnimation
- [BREAKING CHANGE] [MaterialTopAppBar] Rename IconButtonAnimation property to IconButtonTouchAnimationType
- [BREAKING CHANGE] [MaterialTopAppBar] Remove IconButtonAnimationParameter property
- [BREAKING CHANGE] [MaterialTopAppBar] Rename IconButtonCustomAnimation property to IconButtonTouchAnimation
- [Issue #93] Fix NullReferenceException thrown when applying implicit styles

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

