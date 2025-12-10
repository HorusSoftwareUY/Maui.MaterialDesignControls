using System.Globalization;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public class MaterialSegmentedButtonItemView : ContentView
	{
        #region Attributes

        private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
        private readonly static double DefaultFontSize = MaterialFontSize.LabelLarge;
        private readonly static Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Surface, Dark = MaterialDarkTheme.Surface }.GetValueForCurrentTheme<Color>();
        private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialDarkTheme.OnSurface }.GetValueForCurrentTheme<Color>();
        private readonly static double DefaultIconSize = 18;

        #endregion

        #region Bindable Properties

        /// <summary>
        /// The backing store for the <see cref="FontFamily">FontFamily</see> bindable property.
        /// </summary>
        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialSegmentedButton), defaultValue: DefaultFontFamily);

        /// <summary>
        /// The backing store for the <see cref="FontSize">FontSize</see> bindable property.
        /// </summary>
        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialSegmentedButton), defaultValue: DefaultFontSize);

        /// <summary>
        /// The backing store for the <see cref="CharacterSpacing">CharacterSpacing</see> bindable property.
        /// </summary>
        public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialSegmentedButton), Button.CharacterSpacingProperty.DefaultValue);

        /// <summary>
        /// The backing store for the <see cref="FontAttributes">FontAttributes</see> bindable property.
        /// </summary>
        public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialSegmentedButton), defaultValue: Button.FontAttributesProperty.DefaultValue);

        /// <summary>
        /// The backing store for the <see cref="TextTransform">TextTransform</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TextTransformProperty = BindableProperty.Create(nameof(TextTransform), typeof(TextTransform), typeof(MaterialSegmentedButton), defaultValue: Button.TextTransformProperty.DefaultValue);

        /// <summary>
        /// The backing store for the <see cref="TextDecorations">TextDecorations</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TextDecorationsProperty = BindableProperty.Create(nameof(TextDecorations), typeof(TextDecorations), typeof(MaterialSegmentedButton), defaultValue: TextDecorations.None);

        /// <summary>
        /// The backing store for the <see cref="BackgroundColor">BackgroundColor</see> bindable property.
        /// </summary>
        public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialSegmentedButton), defaultValue: DefaultBackgroundColor);

        /// <summary>
        /// The backing store for the <see cref="TextColor">TextColor</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(MaterialSegmentedButton), defaultValue: DefaultTextColor);

        /// <summary>
        /// The backing store for the <see cref="IconSize">IconSize</see> bindable property.
        /// </summary>
        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(double), typeof(MaterialSegmentedButton), defaultValue: DefaultIconSize);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the font family for the segment text.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
        /// </default>
        public string FontFamily
        {
            get => (string)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the font size for the segment text.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="MaterialFontSize.LabelLarge">MaterialFontSize.LabelLarge</see> / Tablet: 17 - Phone: 14
        /// </default>
        [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the spacing between characters for the segment text.
        /// This is a bindable property.
        /// </summary>
        public double CharacterSpacing
        {
            get => (double)GetValue(CharacterSpacingProperty);
            set => SetValue(CharacterSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets <see cref="Microsoft.Maui.Controls.FontAttributes">attributes</see> for the segment text.
        /// This is a bindable property.
        /// </summary>
        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributesProperty);
            set => SetValue(FontAttributesProperty, value);
        }

        /// <summary>
        /// Gets or sets <see cref="Microsoft.Maui.TextTransform">transformation</see> for the segment text.
        /// This is a bindable property.
        /// </summary>
        public TextTransform TextTransform
        {
            get => (TextTransform)GetValue(TextTransformProperty);
            set => SetValue(TextTransformProperty, value);
        }

        /// <summary>
        /// Gets or sets <see cref="Microsoft.Maui.TextDecorations">decorations</see> for the segment text.
        /// This is a bindable property.
        /// </summary>
        public TextDecorations TextDecorations
        {
            get => (TextDecorations)GetValue(TextDecorationsProperty);
            set => SetValue(TextDecorationsProperty, value);
        }

        /// <summary>
        /// Gets or sets the background <see cref="Color">color</see> for the segment.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// Theme: Light = <see cref="MaterialLightTheme.Surface">MaterialLightTheme.Surface</see> - Dark = <see cref="MaterialDarkTheme.Surface">MaterialDarkTheme.Surface</see>
        /// </default>
        /// <remarks>This property has no effect if <see cref="MaterialSegmentedButton.Type">type</see> is set to <see cref="MaterialSegmentedButtonType.Outlined">Outlined</see>.</remarks>
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the text <see cref="Color">color</see> for the segment.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// Theme: Light = <see cref="MaterialLightTheme.OnSurface">MaterialLightTheme.OnSurface</see> - Dark = <see cref="MaterialDarkTheme.OnSurface">MaterialDarkTheme.OnSurface</see>
        /// </default>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the icon size for the segment.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// 18
        /// </default>
        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        #endregion

        #region Layout

        internal StackLayout? _layout;
        internal Label? _label;
        internal Image? _icon;

        #endregion

        #region Methods

        internal bool CreateItemContent(MaterialSegmentedButtonItem item)
        {
            try
            {
                Utils.Logger.Debug("Creating item content");

                this.SetBinding(StackLayout.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));

                _layout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Center,
                    Padding = new Thickness(12, 0),
                    Spacing = 0
                };

                _icon = new Image
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 0, 8, 0),
                    IsVisible = true
                };

                _icon.SetBinding(Image.IsVisibleProperty, new Binding(nameof(item.IsSelected), source: item, converter: new ItemSelectedToIconVisibleConverter(item)));
                _icon.SetBinding(Image.SourceProperty, new Binding(nameof(item.IsSelected), source: item, converter: new ItemSelectedToIconConverter(item)));

                _icon.SetBinding(Image.WidthRequestProperty, new Binding(nameof(IconSize), source: this));
                _icon.SetBinding(Image.MinimumWidthRequestProperty, new Binding(nameof(IconSize), source: this));
                _icon.SetBinding(Image.HeightRequestProperty, new Binding(nameof(IconSize), source: this));
                _icon.SetBinding(Image.MinimumHeightRequestProperty, new Binding(nameof(IconSize), source: this));

                var iconTintColor = new IconTintColorBehavior();
                iconTintColor.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(TextColor), source: this));
                iconTintColor.SetBinding(IconTintColorBehavior.IsEnabledProperty, new Binding(nameof(item.ApplyIconTintColor), source: item));
                _icon.Behaviors.Add(iconTintColor);

                _label = new MaterialLabel
                {
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };

                _label.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(item.Text), source: item));
                _label.SetBinding(MaterialLabel.AutomationIdProperty, new Binding(nameof(item.AutomationId), source: item));

                _label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(TextColor), source: this));
                _label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));
                _label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
                _label.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
                _label.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
                _label.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(TextTransform), source: this));
                _label.SetBinding(MaterialLabel.TextDecorationsProperty, new Binding(nameof(TextDecorations), source: this));

                _layout.Add(_icon);
                _layout.Add(_label);

                Content = _layout;

                return true;
            }
            catch (Exception ex)
            {
                Utils.Logger.LogException("ERROR creating item content", ex, this);
                return false;
            }
        }

        #endregion

        #region Converters

        private class ItemSelectedToIconVisibleConverter(MaterialSegmentedButtonItem item) : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if ((bool)value && item.SelectedIcon != null)
                {
                    return true;
                }
                else if (!(bool)value && item.UnselectedIcon != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        private class ItemSelectedToIconConverter(MaterialSegmentedButtonItem item) : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return (bool)value ? item.SelectedIcon : item.UnselectedIcon;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        #endregion Converters
    }
}