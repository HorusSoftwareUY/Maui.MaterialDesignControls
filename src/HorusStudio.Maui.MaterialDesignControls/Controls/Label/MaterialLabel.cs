namespace HorusStudio.Maui.MaterialDesignControls
{
    public enum LabelTypes
    {
        DisplayLarge,
        DisplayMedium,
        DisplaySmall,
        HeadlineLarge,
        HeadlineMedium,
        HeadlineSmall,
        TitleLarge,
        TitleMedium,
        TitleSmall,
        BodyLarge,
        BodyMedium,
        BodySmall,
        LabelLarge,
        LabelMedium,
        LabelSmall
    }

    /// <summary>
    /// A label <see cref="View" /> that helps make writing legible and beautiful, and follows Material Design Guidelines <see href="https://m3.material.io/styles/typography/overview" />.
    /// </summary>
    public class MaterialLabel : Label
    {
        // TODO: [iOS] FontAttributes doesn't work

        #region Attributes

        private readonly static LabelTypes DefaultType = LabelTypes.BodyMedium;
        private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
        private readonly static string DefaultFontFamilyRegular = MaterialFontFamily.Regular;
        private readonly static string DefaultFontFamilyMedium = MaterialFontFamily.Medium;
        private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();

        #endregion Attributes

        #region Bindable Properties

        /// <summary>
        /// The backing store for the <see cref="Type" /> bindable property.
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
        /// The backing store for the <see cref="FontFamily" /> bindable property.
        /// </summary>
        public static new readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialLabel), defaultValue: DefaultFontFamily);

        /// <summary>
        /// The backing store for the <see cref="FontFamilyRegular" /> bindable property.
        /// </summary>
        public static readonly BindableProperty FontFamilyRegularProperty = BindableProperty.Create(nameof(FontFamilyRegular), typeof(string), typeof(MaterialLabel), defaultValue: DefaultFontFamilyRegular);

        /// <summary>
        /// The backing store for the <see cref="FontFamilyMedium" /> bindable property.
        /// </summary>
        public static readonly BindableProperty FontFamilyMediumProperty = BindableProperty.Create(nameof(FontFamilyMedium), typeof(string), typeof(MaterialLabel), defaultValue: DefaultFontFamilyMedium);

        /// <summary>
        /// The backing store for the <see cref="TextColor" /> bindable property.
        /// </summary>
        public static new readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialLabel), defaultValue: DefaultTextColor);

        #endregion Bindable Properties

        #region Properties

        /// <summary>
        /// Gets or sets the label type according to <see cref="LabelTypes"/> enum.
        /// This property handle internally the FontFamily, CharacterSpacing and FontSize properties.
        /// The default value is <see cref="LabelTypes.BodyMedium"/>. This is a bindable property.
        /// </summary>
        public LabelTypes Type
        {
            get => (LabelTypes)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        /// <summary>
        /// Gets or sets the font family for the label. This is a bindable property.
        /// </summary>
        public new string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the regular font family for the label. This is a bindable property.
        /// </summary>
        public string FontFamilyRegular
        {
            get => (string)GetValue(FontFamilyRegularProperty);
            set => SetValue(FontFamilyRegularProperty, value);
        }

        /// <summary>
        /// Gets or sets the medium font family for the label. This is a bindable property.
        /// </summary>
        public string FontFamilyMedium
        {
            get => (string)GetValue(FontFamilyMediumProperty);
            set => SetValue(FontFamilyMediumProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the text of the label. This is a bindable property.
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

            SetBinding(Label.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
            SetBinding(Label.FontFamilyProperty, new Binding(nameof(FontFamilyRegular), source: this));
            SetBinding(Label.FontFamilyProperty, new Binding(nameof(FontFamilyMedium), source: this));
            SetBinding(Label.TextColorProperty, new Binding(nameof(TextColor), source: this));

            if (Type == DefaultType)
            {
                TypeChanged(Type);
            }
        }

        #endregion Constructors

        #region Methods

        private void TypeChanged(LabelTypes type)
        {
            switch (type)
            {
                case LabelTypes.DisplayLarge:
                    base.FontFamily = this.FontFamilyRegular;
                    base.CharacterSpacing = MaterialFontTracking.DisplayLarge;
                    base.FontSize = MaterialFontSize.DisplayLarge;
                    break;
                case LabelTypes.DisplayMedium:
                    base.FontFamily = this.FontFamilyRegular;
                    base.CharacterSpacing = MaterialFontTracking.DisplayMedium;
                    base.FontSize = MaterialFontSize.DisplayMedium;
                    break;
                case LabelTypes.DisplaySmall:
                    base.FontFamily = this.FontFamilyRegular;
                    base.CharacterSpacing = MaterialFontTracking.DisplaySmall;
                    base.FontSize = MaterialFontSize.DisplaySmall;
                    break;
                case LabelTypes.HeadlineLarge:
                    base.FontFamily = this.FontFamilyRegular;
                    base.CharacterSpacing = MaterialFontTracking.HeadlineLarge;
                    base.FontSize = MaterialFontSize.HeadlineLarge;
                    break;
                case LabelTypes.HeadlineMedium:
                    base.FontFamily = this.FontFamilyRegular;
                    base.CharacterSpacing = MaterialFontTracking.HeadlineMedium;
                    base.FontSize = MaterialFontSize.HeadlineMedium;
                    break;
                case LabelTypes.HeadlineSmall:
                    base.FontFamily = this.FontFamilyRegular;
                    base.CharacterSpacing = MaterialFontTracking.HeadlineSmall;
                    base.FontSize = MaterialFontSize.HeadlineSmall;
                    break;
                case LabelTypes.TitleLarge:
                    base.FontFamily = this.FontFamilyRegular;
                    base.CharacterSpacing = MaterialFontTracking.TitleLarge;
                    base.FontSize = MaterialFontSize.TitleLarge;
                    break;
                case LabelTypes.TitleMedium:
                    base.FontFamily = this.FontFamilyMedium;
                    base.CharacterSpacing = MaterialFontTracking.TitleMedium;
                    base.FontSize = MaterialFontSize.TitleMedium;
                    break;
                case LabelTypes.TitleSmall:
                    base.FontFamily = this.FontFamilyMedium;
                    base.CharacterSpacing = MaterialFontTracking.TitleSmall;
                    base.FontSize = MaterialFontSize.TitleSmall;
                    break;
                case LabelTypes.BodyLarge:
                    base.FontFamily = this.FontFamilyRegular;
                    base.CharacterSpacing = MaterialFontTracking.BodyLarge;
                    base.FontSize = MaterialFontSize.BodyLarge;
                    break;
                case LabelTypes.BodyMedium:
                    base.FontFamily = this.FontFamilyRegular;
                    base.CharacterSpacing = MaterialFontTracking.BodyMedium;
                    base.FontSize = MaterialFontSize.BodyMedium;
                    break;
                case LabelTypes.BodySmall:
                    base.FontFamily = this.FontFamilyRegular;
                    base.CharacterSpacing = MaterialFontTracking.BodySmall;
                    base.FontSize = MaterialFontSize.BodySmall;
                    break;
                case LabelTypes.LabelLarge:
                    base.FontFamily = this.FontFamilyMedium;
                    base.CharacterSpacing = MaterialFontTracking.LabelLarge;
                    base.FontSize = MaterialFontSize.LabelLarge;
                    break;
                case LabelTypes.LabelMedium:
                    base.FontFamily = this.FontFamilyMedium;
                    base.CharacterSpacing = MaterialFontTracking.LabelMedium;
                    base.FontSize = MaterialFontSize.LabelMedium;
                    break;
                case LabelTypes.LabelSmall:
                    base.FontFamily = this.FontFamilyMedium;
                    base.CharacterSpacing = MaterialFontTracking.LabelSmall;
                    base.FontSize = MaterialFontSize.LabelSmall;
                    break;
            }
        }

        #endregion Methods
    }
}