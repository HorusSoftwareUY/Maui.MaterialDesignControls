namespace HorusStudio.Maui.MaterialDesignControls
{
    public enum LabelTypes
    {
        /// <summary>Tablet: 80, Phone: 57</summary>
        DisplayLarge,
        /// <summary>Tablet: 62, Phone: 45</summary>
        DisplayMedium,
        /// <summary>Tablet: 50, Phone: 36</summary>
        DisplaySmall,
        /// <summary>Tablet: 44, Phone: 32</summary>
        HeadlineLarge,
        /// <summary>Tablet: 38, Phone: 28</summary>
        HeadlineMedium,
        /// <summary>Tablet: 32, Phone: 24</summary>
        HeadlineSmall,
        /// <summary>Tablet: 26, Phone: 22</summary>
        TitleLarge,
        /// <summary>Tablet: 19, Phone: 16</summary>
        TitleMedium,
        /// <summary>Tablet: 17, Phone: 14</summary>
        TitleSmall,
        /// <summary>Tablet: 19, Phone: 16</summary>
        BodyLarge,
        /// <summary>Tablet: 17, Phone: 14</summary>
        BodyMedium,
        /// <summary>Tablet: 15, Phone: 12</summary>
        BodySmall,
        /// <summary>Tablet: 17, Phone: 14</summary>
        LabelLarge,
        /// <summary>Tablet: 15, Phone: 12</summary>
        LabelMedium,
        /// <summary>Tablet: 14, Phone: 11</summary>
        LabelSmall
    }

    /// <summary>
    /// A label <see cref="View" /> that helps make writing legible and beautiful, and follows Material Design Guidelines <see href="https://m3.material.io/styles/typography/overview">See here. </see>
    /// </summary>
    /// <example>
    ///
    /// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialLabel.jpg</img>
    ///
    /// <h3>XAML sample</h3>
    /// <code>
    /// <xaml>
    /// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
    /// 
    /// &lt;material:MaterialLabel 
    ///        Type="HeadlineLarge"
    ///        Text="Headline large"/&gt;
    /// </xaml>
    /// </code>
    /// 
    /// <h3>C# sample</h3>
    /// <code>
    /// var label = new MaterialLabel()
    /// {
    ///     Type = LabelTypes.HeadlineLarge,
    ///     Text = "This Material Label"
    /// };
    ///</code>
    ///
    /// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/LabelPage.xaml)
    /// 
    /// </example>
    /// <todoList>
    /// * [iOS] FontAttributes doesn't work.
    /// </todoList>
    public class MaterialLabel : Label
    {
        // TODO: [iOS] FontAttributes and SupportingFontAttributes don't work (MAUI issue)

        #region Attributes

        private const LabelTypes DefaultType = LabelTypes.BodyMedium;
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamily = _ => MaterialFontFamily.Default;
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamilyRegular = _ => MaterialFontFamily.Regular;
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultFontFamilyMedium = _ => MaterialFontFamily.Medium;
        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTextColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();

        #endregion Attributes

        #region Bindable Properties

        /// <summary>
        /// The backing store for the <see cref="Type" />
        /// bindable property.
        /// </summary>
        public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(LabelTypes), typeof(MaterialLabel), defaultValue: DefaultType, propertyChanged: (bindable, oldValue, newValue) =>
        {
            if (bindable is MaterialLabel self)
            {
                if (Enum.IsDefined(typeof(LabelTypes), oldValue) &&
                    Enum.IsDefined(typeof(LabelTypes), newValue) &&
                    (LabelTypes)oldValue != (LabelTypes)newValue)
                {
                    self.TypeChanged((LabelTypes)newValue);
                }
            }
        });

        /// <summary>
        /// The backing store for the <see cref="FontFamily" />
        /// bindable property.
        /// </summary>
        public new static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialLabel), defaultValueCreator: DefaultFontFamily, propertyChanged: (bindable, _, _) =>
        {
            if (bindable is MaterialLabel self)
            {
                self.SetFontFamily();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="FontFamilyRegular" />
        /// bindable property.
        /// </summary>
        public static readonly BindableProperty FontFamilyRegularProperty = BindableProperty.Create(nameof(FontFamilyRegular), typeof(string), typeof(MaterialLabel), defaultValueCreator: DefaultFontFamilyRegular);

        /// <summary>
        /// The backing store for the <see cref="FontFamilyMedium" />
        /// bindable property.
        /// </summary>
        public static readonly BindableProperty FontFamilyMediumProperty = BindableProperty.Create(nameof(FontFamilyMedium), typeof(string), typeof(MaterialLabel), defaultValueCreator: DefaultFontFamilyMedium);

        /// <summary>
        /// The backing store for the <see cref="TextColor" />
        /// bindable property.
        /// </summary>
        public new static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialLabel), defaultValueCreator: DefaultTextColor);

        #endregion Bindable Properties

        #region Properties

        /// <summary>
        /// Gets or sets the label type according to <see cref="LabelTypes"/> enum.
        /// This property handle internally the FontFamily, CharacterSpacing and FontSize properties.
        /// </summary>
        /// <default>
        /// <see cref="LabelTypes.BodyMedium"/>
        /// </default>
        public LabelTypes Type
        {
            get => (LabelTypes)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family for the label.
        /// This is a bindable property.
        /// </summary>
        public new string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the regular font family for the label.
        /// This is a bindable property.
        /// </summary>
        public string FontFamilyRegular
        {
            get => (string)GetValue(FontFamilyRegularProperty);
            set => SetValue(FontFamilyRegularProperty, value);
        }

        /// <summary>
        /// Gets or sets the medium font family for the label.
        /// This is a bindable property.
        /// </summary>
        public string FontFamilyMedium
        {
            get => (string)GetValue(FontFamilyMediumProperty);
            set => SetValue(FontFamilyMediumProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the text of the label.
        /// This is a bindable property.
        /// </summary>
        public new Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        #endregion Properties

        #region Constructors

        public MaterialLabel()
        {
            base.TextColor = this.TextColor;
            base.FontFamily = this.FontFamily;

            SetBinding(Label.TextColorProperty, new Binding(nameof(TextColor), source: this));

            if (Type == DefaultType)
            {
                TypeChanged(Type);
            }
        }

        #endregion Constructors

        #region Methods

        private void SetFontFamily()
        {
            base.FontFamily = this.FontFamily;
        }

        private void TypeChanged(LabelTypes type)
        {
            switch (type)
            {
                case LabelTypes.DisplayLarge:
                    base.FontFamily = FontFamilyRegular;
                    CharacterSpacing = MaterialFontTracking.DisplayLarge;
                    FontSize = MaterialFontSize.DisplayLarge;
                    break;
                case LabelTypes.DisplayMedium:
                    base.FontFamily = FontFamilyRegular;
                    CharacterSpacing = MaterialFontTracking.DisplayMedium;
                    FontSize = MaterialFontSize.DisplayMedium;
                    break;
                case LabelTypes.DisplaySmall:
                    base.FontFamily = FontFamilyRegular;
                    CharacterSpacing = MaterialFontTracking.DisplaySmall;
                    FontSize = MaterialFontSize.DisplaySmall;
                    break;
                case LabelTypes.HeadlineLarge:
                    base.FontFamily = FontFamilyRegular;
                    CharacterSpacing = MaterialFontTracking.HeadlineLarge;
                    FontSize = MaterialFontSize.HeadlineLarge;
                    break;
                case LabelTypes.HeadlineMedium:
                    base.FontFamily = FontFamilyRegular;
                    CharacterSpacing = MaterialFontTracking.HeadlineMedium;
                    FontSize = MaterialFontSize.HeadlineMedium;
                    break;
                case LabelTypes.HeadlineSmall:
                    base.FontFamily = FontFamilyRegular;
                    CharacterSpacing = MaterialFontTracking.HeadlineSmall;
                    FontSize = MaterialFontSize.HeadlineSmall;
                    break;
                case LabelTypes.TitleLarge:
                    base.FontFamily = FontFamilyRegular;
                    CharacterSpacing = MaterialFontTracking.TitleLarge;
                    FontSize = MaterialFontSize.TitleLarge;
                    break;
                case LabelTypes.TitleMedium:
                    base.FontFamily = FontFamilyMedium;
                    CharacterSpacing = MaterialFontTracking.TitleMedium;
                    FontSize = MaterialFontSize.TitleMedium;
                    break;
                case LabelTypes.TitleSmall:
                    base.FontFamily = FontFamilyMedium;
                    CharacterSpacing = MaterialFontTracking.TitleSmall;
                    FontSize = MaterialFontSize.TitleSmall;
                    break;
                case LabelTypes.BodyLarge:
                    base.FontFamily = FontFamilyRegular;
                    CharacterSpacing = MaterialFontTracking.BodyLarge;
                    FontSize = MaterialFontSize.BodyLarge;
                    break;
                case LabelTypes.BodyMedium:
                    base.FontFamily = FontFamilyRegular;
                    CharacterSpacing = MaterialFontTracking.BodyMedium;
                    FontSize = MaterialFontSize.BodyMedium;
                    break;
                case LabelTypes.BodySmall:
                    base.FontFamily = FontFamilyRegular;
                    CharacterSpacing = MaterialFontTracking.BodySmall;
                    FontSize = MaterialFontSize.BodySmall;
                    break;
                case LabelTypes.LabelLarge:
                    base.FontFamily = FontFamilyMedium;
                    CharacterSpacing = MaterialFontTracking.LabelLarge;
                    FontSize = MaterialFontSize.LabelLarge;
                    break;
                case LabelTypes.LabelMedium:
                    base.FontFamily = FontFamilyMedium;
                    CharacterSpacing = MaterialFontTracking.LabelMedium;
                    FontSize = MaterialFontSize.LabelMedium;
                    break;
                case LabelTypes.LabelSmall:
                    base.FontFamily = FontFamilyMedium;
                    CharacterSpacing = MaterialFontTracking.LabelSmall;
                    FontSize = MaterialFontSize.LabelSmall;
                    break;
            }
        }

        #endregion Methods
    }
}