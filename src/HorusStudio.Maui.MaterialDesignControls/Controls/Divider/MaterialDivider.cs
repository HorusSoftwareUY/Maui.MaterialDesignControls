namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Dividers group content in lists or other containers and follow Material Design Guidelines. <see href="https://m3.material.io/components/divider/overview">See more</see>.
    /// </summary>
    /// <example>
    ///
    /// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialDivider.jpg</img>
    ///
    /// <h3>XAML sample</h3>
    /// <code>
    /// <xaml>
    /// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
    /// 
    /// &lt;material:MaterialDivider
    ///        Color="{Binding DividerColor}"/&gt;
    /// </xaml>
    /// </code>
    /// 
    /// <h3>C# sample</h3>
    /// <code>
    /// var divider = new MaterialDivider()
    /// {
    ///     Color = Colors.Black
    /// };
    /// </code>
    ///
    /// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/DividerPage.xaml)
    /// 
    /// </example>
    public class MaterialDivider : BoxView
    {
        #region Attributes

        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OutlineVariant, Dark = MaterialDarkTheme.OutlineVariant }.GetValueForCurrentTheme<Color>();
        private const double DefaultHeightRequest = 1.0;

        #endregion Attributes

        #region Bindable Properties

        /// <summary>
        /// The backing store for the <see cref="Color">Color</see> bindable property.
        /// </summary>
        public new static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(MaterialDivider), defaultValueCreator: DefaultColor);

        /// <summary>
        /// The backing store for the <see cref="HeightRequest">HeightRequest</see> bindable property.
        /// </summary>
        public new static readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(MaterialDivider), defaultValue: DefaultHeightRequest);

        #endregion Bindable Properties

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="Color">color</see> of the divider.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// Theme: Light: <see cref="MaterialLightTheme.OutlineVariant">MaterialLightTheme.OutlineVariant</see> - Dark: <see cref="MaterialDarkTheme.OutlineVariant">MaterialDarkTheme.OutlineVariant</see>
        /// </default>
        public new Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set =>  SetValue(ColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the desired height override of this element.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// 1
        /// </default>
        public new double HeightRequest
        {
            get => (double)GetValue(HeightRequestProperty);
            set => SetValue(HeightRequestProperty, value);
        }

        #endregion Properties

        #region Constructor

        public MaterialDivider()
        {
            base.Color = this.Color;
            base.HeightRequest = this.HeightRequest;

            SetBinding(BoxView.ColorProperty, new Binding(nameof(Color), source: this));
            SetBinding(BoxView.HeightRequestProperty, new Binding(nameof(HeightRequest), source: this));
        }

        #endregion Constructor
    }
}