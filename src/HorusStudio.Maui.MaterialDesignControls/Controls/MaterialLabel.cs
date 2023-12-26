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
        LabelLarge,
        LabelMedium,
        LabelSmall,
        BodyLarge,
        BodyMedium,
        BodySmall
    }

    /// <summary>
    /// A label <see cref="View" /> that helps make writing legible and beautiful, and follows Material Design Guidelines <see href="https://m3.material.io/styles/typography/overview">.
    /// </summary>
    public class MaterialLabel : Label
    {
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
        /// The default value is <see cref="LabelTypes.BodyMedium"/>. This is a bindable property.
        /// </summary>
        public LabelTypes Type
        {
            get { return (LabelTypes)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font family for the label. This is a bindable property.
        /// </summary>
        public new string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the regular font family for the label. This is a bindable property.
        /// </summary>
        public string FontFamilyRegular
        {
            get { return (string)GetValue(FontFamilyRegularProperty); }
            set { SetValue(FontFamilyRegularProperty, value); }
        }

        /// <summary>
        /// Gets or sets the medium font family for the label. This is a bindable property.
        /// </summary>
        public string FontFamilyMedium
        {
            get { return (string)GetValue(FontFamilyMediumProperty); }
            set { SetValue(FontFamilyMediumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the text of the label. This is a bindable property.
        /// </summary>
        public new Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
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
                    base.FontFamily = this.FontFamily;
                    base.CharacterSpacing = -0.25;
                    base.FontSize = MaterialFontSize.DisplayLarge;
                    break;
                case LabelTypes.DisplayMedium:
                    base.FontFamily = this.FontFamily;
                    base.CharacterSpacing = 0;
                    base.FontSize = MaterialFontSize.DisplayMedium;
                    break;
                case LabelTypes.DisplaySmall:
                    base.FontFamily = this.FontFamily;
                    base.CharacterSpacing = 0;
                    base.FontSize = MaterialFontSize.DisplaySmall;
                    break;
                case LabelTypes.HeadlineLarge:
                    base.FontFamily = this.FontFamily;
                    base.CharacterSpacing = 0;
                    base.FontSize = MaterialFontSize.HeadlineLarge;
                    break;
                case LabelTypes.HeadlineMedium:
                    base.FontFamily = this.FontFamily;
                    base.CharacterSpacing = 0;
                    base.FontSize = MaterialFontSize.HeadlineMedium;
                    break;
                case LabelTypes.HeadlineSmall:
                    base.FontFamily = this.FontFamily;
                    base.CharacterSpacing = 0;
                    base.FontSize = MaterialFontSize.HeadlineSmall;
                    break;
                case LabelTypes.TitleLarge:
                    base.FontFamily = this.FontFamilyRegular;
                    base.CharacterSpacing = 0;
                    base.FontSize = MaterialFontSize.TitleLarge;
                    break;
                case LabelTypes.TitleMedium:
                    base.FontFamily = this.FontFamilyMedium;
                    base.CharacterSpacing = 0.15;
                    base.FontSize = MaterialFontSize.TitleMedium;
                    break;
                case LabelTypes.TitleSmall:
                    base.FontFamily = this.FontFamilyMedium;
                    base.CharacterSpacing = 0.1;
                    base.FontSize = MaterialFontSize.TitleSmall;
                    break;
                case LabelTypes.LabelLarge:
                    base.FontFamily = this.FontFamilyMedium;
                    base.CharacterSpacing = 0.1;
                    base.FontSize = MaterialFontSize.LabelLarge;
                    break;
                case LabelTypes.LabelMedium:
                    base.FontFamily = this.FontFamilyMedium;
                    base.CharacterSpacing = 0.5;
                    base.FontSize = MaterialFontSize.LabelMedium;
                    break;
                case LabelTypes.LabelSmall:
                    base.FontFamily = this.FontFamilyMedium;
                    base.CharacterSpacing = 0.5;
                    base.FontSize = MaterialFontSize.LabelSmall;
                    break;
                case LabelTypes.BodyLarge:
                    base.FontFamily = this.FontFamily;
                    base.CharacterSpacing = 0.5;
                    base.FontSize = MaterialFontSize.BodyLarge;
                    break;
                case LabelTypes.BodyMedium:
                    base.FontFamily = this.FontFamily;
                    base.CharacterSpacing = 0.25;
                    base.FontSize = MaterialFontSize.BodyMedium;
                    break;
                case LabelTypes.BodySmall:
                    base.FontFamily = this.FontFamily;
                    base.CharacterSpacing = 0.4;
                    base.FontSize = MaterialFontSize.BodySmall;
                    break;
            }
        }

        #endregion Methods
    }
}