namespace HorusStudio.Maui.MaterialDesignControls
{
    public static class MaterialDesignControls
    {
        private const string CommonStatesGroupName = "CommonStates";

        public static void Init(Application application)
        {
            // Button
            application.Resources.Add(ButtonStyle());
            // Icon button
            application.Resources.Add(IconButtonStyle());
        }

        private static Style ButtonStyle()
        {
            var commonStatesGroup = new VisualStateGroup { Name = CommonStatesGroupName };

            var disabledState = new VisualState { Name = ButtonCommonStates.Disabled };
            disabledState.Setters.Add(
                MaterialButton.BackgroundColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.12f));

            disabledState.Setters.Add(
                MaterialButton.TextColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));

            disabledState.Setters.Add(
                MaterialButton.IconTintColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));

            disabledState.Setters.Add(MaterialButton.ShadowProperty, null);

            disabledState.Setters.Add(
                MaterialButton.BorderColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.12f));

            var pressedState = new VisualState { Name = ButtonCommonStates.Pressed };
            // TODO: Add default Material behavior for pressed state (v2)

            commonStatesGroup.States.Add(new VisualState { Name = ButtonCommonStates.Normal });
            commonStatesGroup.States.Add(disabledState);
            commonStatesGroup.States.Add(pressedState);
            //VisualStateManager.SetVisualStateGroups(this, new VisualStateGroupList(true) { commonStatesGroup });

            var style = new Style(typeof(MaterialButton));
            style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

            return style;
        }

        private static Style IconButtonStyle()
        {
            var commonStatesGroup = new VisualStateGroup { Name = CommonStatesGroupName };

            var disabledState = new VisualState { Name = ButtonCommonStates.Disabled };
            disabledState.Setters.Add(
                MaterialIconButton.BackgroundColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.12f));

            disabledState.Setters.Add(
                MaterialIconButton.IconTintColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.38f));

            disabledState.Setters.Add(MaterialIconButton.ShadowProperty, null);

            disabledState.Setters.Add(
                MaterialIconButton.BorderColorProperty,
                new AppThemeBindingExtension
                {
                    Light = MaterialLightTheme.OnSurface,
                    Dark = MaterialDarkTheme.OnSurface
                }
                .GetValueForCurrentTheme<Color>()
                .WithAlpha(0.12f));

            var pressedState = new VisualState { Name = ButtonCommonStates.Pressed };
            // TODO: Add default Material behavior for pressed state (v2)

            commonStatesGroup.States.Add(new VisualState { Name = ButtonCommonStates.Normal });
            commonStatesGroup.States.Add(disabledState);
            commonStatesGroup.States.Add(pressedState);
            //VisualStateManager.SetVisualStateGroups(this, new VisualStateGroupList(true) { commonStatesGroup });

            var style = new Style(typeof(MaterialIconButton));
            style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

            return style;
        }
    }
}