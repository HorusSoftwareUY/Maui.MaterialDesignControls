namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// A divider <see cref="View" /> that group content in lists or other containers and follows Material Design Guidelines <see href="https://m3.material.io/components/divider/overview" />.
    /// </summary>
    public class MaterialDivider : BoxView
    {
        #region Attributes

        private readonly static Color DefaultColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OutlineVariant, Dark = MaterialDarkTheme.OutlineVariant }.GetValueForCurrentTheme<Color>();
        private readonly static double DefaultHeightRequest = 1.0;

        #endregion Attributes

        #region Bindable Properties

        /// <summary>
        /// The backing store for the <see cref="Color" /> bindable property.
        /// </summary>
        public static new readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(MaterialDivider), defaultValue: DefaultColor);

        /// <summary>
        /// The backing store for the <see cref="HeightRequest" /> bindable property.
        /// </summary>
        public static new readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(MaterialDivider), defaultValue: DefaultHeightRequest);

        #endregion Bindable Properties

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="Color" /> of the divider. This is a bindable property.
        /// </summary>
        public new Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the desired height override of this element.
        /// The default value is 1.
        /// This is a bindable property.
        /// </summary>
        public new double HeightRequest
        {
            get { return (double)GetValue(HeightRequestProperty); }
            set { SetValue(HeightRequestProperty, value); }
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