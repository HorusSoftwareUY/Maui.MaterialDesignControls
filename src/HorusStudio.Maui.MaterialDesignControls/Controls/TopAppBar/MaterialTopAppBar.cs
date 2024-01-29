using System.Runtime.CompilerServices;
using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public enum MaterialTopAppBarType
    {
        CenterAligned, Small, Medium, Large
    }

    /// <summary>
    /// A top app bar <see cref="View" /> that display navigation, actions, and text at the top of a screen, and follows Material Design Guidelines <see href="https://m3.material.io/components/top-app-bar/overview" />.
    /// </summary>
    public class MaterialTopAppBar : Grid
    {
        #region Attributes

        private readonly static MaterialTopAppBarType DefaultType = MaterialTopAppBarType.CenterAligned;
        private readonly static Color DefaultHeadlineColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
        private readonly static double DefaultHeadlineFontSize = MaterialFontSize.TitleLarge;
        private readonly static string DefaultHeadlineFontFamily = MaterialFontFamily.Default;
        private readonly static FontAttributes DefaultHeadlineFontAttributes = FontAttributes.None;
        private readonly static Color DefaultDescriptionColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
        private readonly static double DefaultDescriptionFontSize = MaterialFontSize.TitleMedium;
        private readonly static string DefaultDescriptionFontFamily = MaterialFontFamily.Default;
        private readonly static FontAttributes DefaultDescriptionFontAttributes = FontAttributes.None;
        private readonly static Thickness DefaultDescriptionMarginAdjustment = new Thickness(_descriptionLateralMargin, 0, _descriptionLateralMargin, 0);
        private readonly static double DefaultIconSize = 48.0;
        private readonly static AnimationTypes DefaultIconButtonAnimationType = MaterialAnimation.Type;
#nullable enable
        private readonly static double? DefaultIconButtonAnimationParameter = MaterialAnimation.Parameter;
#nullable disable
        private readonly static Color DefaultBusyIndicatorColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
        private readonly static double DefaultBusyIndicatorSize = 24.0;
        private readonly static int DefaultScrollViewAnimationLength = 250;

        private const double _descriptionLateralMargin = 10;

        private const int SmallRowHeight = 48;
        private const int MediumRowHeight = 106;
        private const int LargeRowHeight = 106;

        private const int SmallLabelLateralMargin = 48;
        private const int MediumLabelLateralMargin = 10;
        private const int LargeLabelLateralMargin = 10;

        private bool _isCollapsed = false;

        #endregion Attributes

        #region Bindable Properties

        /// <summary>
        /// The backing store for the <see cref="Type" /> bindable property.
        /// </summary>
        public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialTopAppBarType), typeof(MaterialTopAppBar), DefaultType, BindingMode.OneTime, propertyChanged: (bindable, oldValue, newValue) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                if (Enum.IsDefined(typeof(MaterialTopAppBarType), oldValue) &&
                    Enum.IsDefined(typeof(MaterialTopAppBarType), newValue) &&
                    (MaterialTopAppBarType)oldValue != (MaterialTopAppBarType)newValue)
                {
                    self.UpdateLayoutAfterTypeChanged((MaterialTopAppBarType)newValue);
                }
            }
        });

        /// <summary>
        /// The backing store for the <see cref="Headline" /> bindable property.
        /// </summary>
        public static readonly BindableProperty HeadlineProperty = BindableProperty.Create(nameof(Headline), typeof(string), typeof(MaterialTopAppBar), null, BindingMode.OneTime);

        /// <summary>
        /// The backing store for the <see cref="HeadlineColor" /> bindable property.
        /// </summary>
        public static readonly BindableProperty HeadlineColorProperty = BindableProperty.Create(nameof(HeadlineColor), typeof(Color), typeof(MaterialTopAppBar), DefaultHeadlineColor, BindingMode.OneTime);

        /// <summary>
        /// The backing store for the <see cref="HeadlineFontSize" /> bindable property.
        /// </summary>
        public static readonly BindableProperty HeadlineFontSizeProperty = BindableProperty.Create(nameof(HeadlineFontSize), typeof(double), typeof(MaterialTopAppBar), defaultValue: DefaultHeadlineFontSize);

        /// <summary>
        /// The backing store for the <see cref="HeadlineFontFamily" /> bindable property.
        /// </summary>
        public static readonly BindableProperty HeadlineFontFamilyProperty = BindableProperty.Create(nameof(HeadlineFontFamily), typeof(string), typeof(MaterialTopAppBar), defaultValue: DefaultHeadlineFontFamily);

        /// <summary>
        /// The backing store for the <see cref="HeadlineFontAttributes" /> bindable property.
        /// </summary>
        public static readonly BindableProperty HeadlineFontAttributesProperty = BindableProperty.Create(nameof(HeadlineFontAttributes), typeof(FontAttributes), typeof(MaterialButton), defaultValue: DefaultHeadlineFontAttributes);

        /// <summary>
        /// The backing store for the <see cref="HeadlineMarginAdjustment" /> bindable property.
        /// </summary>
        public static readonly BindableProperty HeadlineMarginAdjustmentProperty = BindableProperty.Create(nameof(HeadlineMarginAdjustment), typeof(Thickness), typeof(MaterialTopAppBar), default(Thickness), BindingMode.OneTime);

        /// <summary>
        /// The backing store for the <see cref="Description" /> bindable property.
        /// </summary>
        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(MaterialTopAppBar), null, BindingMode.OneTime, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetDescription();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="DescriptionColor" /> bindable property.
        /// </summary>
        public static readonly BindableProperty DescriptionColorProperty = BindableProperty.Create(nameof(DescriptionColor), typeof(Color), typeof(MaterialTopAppBar), DefaultDescriptionColor, BindingMode.OneTime);

        /// <summary>
        /// The backing store for the <see cref="DescriptionFontSize" /> bindable property.
        /// </summary>
        public static readonly BindableProperty DescriptionFontSizeProperty = BindableProperty.Create(nameof(DescriptionFontSize), typeof(double), typeof(MaterialTopAppBar), defaultValue: DefaultDescriptionFontSize);

        /// <summary>
        /// The backing store for the <see cref="DescriptionFontFamily" /> bindable property.
        /// </summary>
        public static readonly BindableProperty DescriptionFontFamilyProperty = BindableProperty.Create(nameof(DescriptionFontFamily), typeof(string), typeof(MaterialTopAppBar), defaultValue: DefaultDescriptionFontFamily);

        /// <summary>
        /// The backing store for the <see cref="DescriptionFontAttributes" /> bindable property.
        /// </summary>
        public static readonly BindableProperty DescriptionFontAttributesProperty = BindableProperty.Create(nameof(DescriptionFontAttributes), typeof(FontAttributes), typeof(MaterialButton), defaultValue: DefaultDescriptionFontAttributes);

        /// <summary>
        /// The backing store for the <see cref="DescriptionMarginAdjustment" /> bindable property.
        /// </summary>
        public static readonly BindableProperty DescriptionMarginAdjustmentProperty = BindableProperty.Create(nameof(DescriptionMarginAdjustment), typeof(Thickness), typeof(MaterialTopAppBar), defaultValue: DefaultDescriptionMarginAdjustment, BindingMode.OneTime);

        /// <summary>
        /// The backing store for the <see cref="LeadingIcon" /> bindable property.
        /// </summary>
        public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(nameof(LeadingIcon), typeof(ImageSource), typeof(MaterialTopAppBar), defaultValue: null, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetLeadingIcon();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="TrailingIcon" /> bindable property.
        /// </summary>
        public static readonly BindableProperty TrailingIconProperty = BindableProperty.Create(nameof(TrailingIcon), typeof(ImageSource), typeof(MaterialTopAppBar), defaultValue: null, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetTrailingIcon();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="IconSize" /> bindable property.
        /// </summary>
        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(double), typeof(MaterialTopAppBar), defaultValue: DefaultIconSize);

        /// <summary>
        /// The backing store for the <see cref="LeadingIconCommand" /> bindable property.
        /// </summary>
        public static readonly BindableProperty LeadingIconCommandProperty = BindableProperty.Create(nameof(LeadingIconCommand), typeof(ICommand), typeof(MaterialTopAppBar), null, BindingMode.OneTime, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetLeadingIconCommand();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="TrailingIconCommand" /> bindable property.
        /// </summary>
        public static readonly BindableProperty TrailingIconCommandProperty = BindableProperty.Create(nameof(TrailingIconCommand), typeof(ICommand), typeof(MaterialTopAppBar), null, BindingMode.OneTime, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetTrailingIconCommand();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="IconButtonAnimation" /> bindable property.
        /// </summary>
        public static readonly BindableProperty IconButtonAnimationProperty = BindableProperty.Create(nameof(IconButtonAnimation), typeof(AnimationTypes), typeof(MaterialTopAppBar), defaultValue: DefaultIconButtonAnimationType);

        /// <summary>
        /// The backing store for the <see cref="IconButtonAnimationParameter" /> bindable property.
        /// </summary>
        public static readonly BindableProperty IconButtonAnimationParameterProperty = BindableProperty.Create(nameof(IconButtonAnimationParameter), typeof(double?), typeof(MaterialTopAppBar), defaultValue: DefaultIconButtonAnimationParameter);

        /// <summary>
        /// The backing store for the <see cref="IconButtonCustomAnimation" /> bindable property.
        /// </summary>
        public static readonly BindableProperty IconButtonCustomAnimationProperty = BindableProperty.Create(nameof(IconButtonCustomAnimation), typeof(ICustomAnimation), typeof(MaterialTopAppBar), defaultValue: null);

        /// <summary>
        /// The backing store for the <see cref="TrailingIconIsBusy" /> bindable property.
        /// </summary>
        public static readonly BindableProperty TrailingIconIsBusyProperty = BindableProperty.Create(nameof(TrailingIconIsBusy), typeof(bool), typeof(MaterialTopAppBar), defaultValue: false, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetTrailingIconIsBusy();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="LeadingIconIsBusy" /> bindable property.
        /// </summary>
        public static readonly BindableProperty LeadingIconIsBusyProperty = BindableProperty.Create(nameof(LeadingIconIsBusy), typeof(bool), typeof(MaterialTopAppBar), defaultValue: false, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetLeadingIconIsBusy();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="BusyIndicatorColor" /> bindable property.
        /// </summary>
        public static readonly BindableProperty BusyIndicatorColorProperty = BindableProperty.Create(nameof(BusyIndicatorColor), typeof(Color), typeof(MaterialTopAppBar), defaultValue: DefaultBusyIndicatorColor);

        /// <summary>
        /// The backing store for the <see cref="BusyIndicatorSize" /> bindable property.
        /// </summary>
        public static readonly BindableProperty BusyIndicatorSizeProperty = BindableProperty.Create(nameof(BusyIndicatorSize), typeof(double), typeof(MaterialTopAppBar), defaultValue: DefaultBusyIndicatorSize, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetBusyIndicatorSize();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="ScrollViewName" /> bindable property.
        /// </summary>
        public static readonly BindableProperty ScrollViewNameProperty = BindableProperty.Create(nameof(ScrollViewName), typeof(string), typeof(MaterialTopAppBar), null, BindingMode.OneTime);

        /// <summary>
        /// The backing store for the <see cref="ScrollViewAnimationLength" /> bindable property.
        /// </summary>
        public static readonly BindableProperty ScrollViewAnimationLengthProperty = BindableProperty.Create(nameof(ScrollViewAnimationLength), typeof(int), typeof(MaterialTopAppBar), defaultValue: DefaultScrollViewAnimationLength);

        #endregion Bindable Properties

        #region Properties

        /// <summary>
        /// Gets or sets the top app bar type according to <see cref="MaterialTopAppBarType"/> enum.
        /// The default value is <see cref="MaterialTopAppBarType.CenterAligned"/>. This is a bindable property.
        /// </summary>
        public MaterialTopAppBarType Type
        {
            get => (MaterialTopAppBarType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        /// <summary>
        /// Gets or sets the headline text displayed on the top app bar.
        /// The default value is <see langword="null"/>. This is a bindable property.
        /// </summary>
        public string Headline
        {
            get => (string)GetValue(HeadlineProperty);
            set => SetValue(HeadlineProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the headline text of the top app bar. This is a bindable property.
        /// </summary>
        public Color HeadlineColor
        {
            get => (Color)GetValue(HeadlineColorProperty);
            set => SetValue(HeadlineColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the font for the headline text of this top app bar. This is a bindable property.
        /// </summary>
        [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
        public double HeadlineFontSize
        {
            get { return (double)GetValue(HeadlineFontSizeProperty); }
            set { SetValue(HeadlineFontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font family for the headline text of this top app bar. This is a bindable property.
        /// </summary>
        public string HeadlineFontFamily
        {
            get { return (string)GetValue(HeadlineFontFamilyProperty); }
            set { SetValue(HeadlineFontFamilyProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the font of this top app bar headline text is bold, italic, or neither.
        /// This is a bindable property.
        /// </summary>
        public FontAttributes HeadlineFontAttributes
        {
            get => (FontAttributes)GetValue(HeadlineFontAttributesProperty);
            set => SetValue(HeadlineFontAttributesProperty, value);
        }

        /// <summary>
        /// Allows you to adjust the margins of the headline text. This is a bindable property.
        /// </summary>
        /// <remarks>This property does not take into account the Left and Right of the set <see cref="Thickness" />, it only applies the Top and Bottom values.</remarks>
        public Thickness HeadlineMarginAdjustment
        {
            get => (Thickness)GetValue(HeadlineMarginAdjustmentProperty);
            set => SetValue(HeadlineMarginAdjustmentProperty, value);
        }

        /// <summary>
        /// Gets or sets the description text displayed on the top app bar.
        /// The default value is <see langword="null"/>. This is a bindable property.
        /// </summary>
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the description text of the top app bar. This is a bindable property.
        /// </summary>
        public Color DescriptionColor
        {
            get => (Color)GetValue(DescriptionColorProperty);
            set => SetValue(DescriptionColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the font for the description text of this top app bar. This is a bindable property.
        /// </summary>
        [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
        public double DescriptionFontSize
        {
            get { return (double)GetValue(DescriptionFontSizeProperty); }
            set { SetValue(DescriptionFontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font family for the description text of this top app bar. This is a bindable property.
        /// </summary>
        public string DescriptionFontFamily
        {
            get { return (string)GetValue(DescriptionFontFamilyProperty); }
            set { SetValue(DescriptionFontFamilyProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the font of this top app bar description text is bold, italic, or neither.
        /// This is a bindable property.
        /// </summary>
        public FontAttributes DescriptionFontAttributes
        {
            get => (FontAttributes)GetValue(DescriptionFontAttributesProperty);
            set => SetValue(DescriptionFontAttributesProperty, value);
        }

        /// <summary>
        /// Allows you to adjust the margins of the description text. This is a bindable property.
        /// </summary>
        /// <remarks>This property does not take into account the Left and Right of the set <see cref="Thickness" />, it only applies the Top and Bottom values.</remarks>
        public Thickness DescriptionMarginAdjustment
        {
            get => (Thickness)GetValue(DescriptionMarginAdjustmentProperty);
            set => SetValue(DescriptionMarginAdjustmentProperty, value);
        }

        /// <summary>
        /// Allows you to display an icon button on the left side of the top app bar. This is a bindable property.
        /// </summary>
        public ImageSource LeadingIcon
        {
            get { return (ImageSource)GetValue(LeadingIconProperty); }
            set { SetValue(LeadingIconProperty, value); }
        }

        private bool LeadingIconIsVisible
            => LeadingIcon != null;

        /// <summary>
        /// Allows you to display an icon button on the right side of the top app bar. This is a bindable property.
        /// </summary>
        public ImageSource TrailingIcon
        {
            get { return (ImageSource)GetValue(TrailingIconProperty); }
            set { SetValue(TrailingIconProperty, value); }
        }

        private bool TrailingIconIsVisible
            => TrailingIcon != null;

        /// <summary>
        /// Gets or sets the size of the <see cref="MaterialTopAppBar.LeadingIcon"/> and <see cref="MaterialTopAppBar.TrailingIcon"/> of this top app bar.
        /// This is a bindable property.
        /// </summary>
        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command to invoke when the leading icon button is clicked. This is a bindable property.
        /// </summary>
        /// <remarks>This property is used to associate a command with an instance of a top app bar. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.</remarks>
        public ICommand LeadingIconCommand
        {
            get => (ICommand)GetValue(LeadingIconCommandProperty);
            set => SetValue(LeadingIconCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the command to invoke when the trailing icon button is clicked. This is a bindable property.
        /// </summary>
        /// <remarks>This property is used to associate a command with an instance of a top app bar. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.</remarks>
        public ICommand TrailingIconCommand
        {
            get => (ICommand)GetValue(TrailingIconCommandProperty);
            set => SetValue(TrailingIconCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets an animation to be executed when leading and trailing icon button are clicked.
        /// The default value is <see cref="AnimationTypes.Fade"/>.
        /// This is a bindable property.
        /// </summary>
        public AnimationTypes IconButtonAnimation
        {
            get { return (AnimationTypes)GetValue(IconButtonAnimationProperty); }
            set { SetValue(IconButtonAnimationProperty, value); }
        }

#nullable enable
        /// <summary>
        /// Gets or sets the parameter to pass to the <see cref="Animation"/> property.
        /// This is a bindable property.
        /// </summary>
        public double? IconButtonAnimationParameter
        {
            get { return (double?)GetValue(IconButtonAnimationParameterProperty); }
            set { SetValue(IconButtonAnimationParameterProperty, value); }
        }
#nullable disable

        /// <summary>
        /// Gets or sets a custom animation to be executed when leading and trailing icon button are clicked.
        /// The default value is <see langword="null"/>.
        /// This is a bindable property.
        /// </summary>
        public ICustomAnimation IconButtonCustomAnimation
        {
            get { return (ICustomAnimation)GetValue(IconButtonCustomAnimationProperty); }
            set { SetValue(IconButtonCustomAnimationProperty, value); }
        }

        /// <summary>
        /// Gets or sets if the trailing icon button is on busy state (executing Command).
        /// The default value is <see langword="false"/>.
        /// This is a bindable property.
        /// </summary>
        public bool TrailingIconIsBusy
        {
            get { return (bool)GetValue(TrailingIconIsBusyProperty); }
            set { SetValue(TrailingIconIsBusyProperty, value); }
        }

        /// <summary>
        /// Gets or sets if the leading icon button is on busy state (executing Command).
        /// The default value is <see langword="false"/>.
        /// This is a bindable property.
        /// </summary>
        public bool LeadingIconIsBusy
        {
            get { return (bool)GetValue(LeadingIconIsBusyProperty); }
            set { SetValue(LeadingIconIsBusyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the busy indicators.
        /// This is a bindable property.
        /// </summary>
        public Color BusyIndicatorColor
        {
            get { return (Color)GetValue(BusyIndicatorColorProperty); }
            set { SetValue(BusyIndicatorColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size for the busy indicators.
        /// This is a bindable property.
        /// </summary>
        public double BusyIndicatorSize
        {
            get { return (double)GetValue(BusyIndicatorSizeProperty); }
            set { SetValue(BusyIndicatorSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name of the <see cref="ScrollView" /> element to which the top app bar will be linked to run collapse or expand animations depending on the user's scroll.
        /// This is a bindable property.
        /// </summary>
        public string ScrollViewName
        {
            get => (string)GetValue(ScrollViewNameProperty);
            set => SetValue(ScrollViewNameProperty, value);
        }

        /// <summary>
        /// Gets or sets the duration of the collapse or expand animation bound to the <see cref="ScrollView" /> element. This is a bindable property.
        /// </summary>
        public int ScrollViewAnimationLength
        {
            get { return (int)GetValue(ScrollViewAnimationLengthProperty); }
            set { SetValue(ScrollViewAnimationLengthProperty, value); }
        }

        #endregion Properties

        #region Events

        #endregion Events

        #region Layout

        private MaterialLabel _headlineLabel;

        private MaterialLabel _descriptionLabel;

        private MaterialIconButton _leadingIconButton;

        private MaterialIconButton _trailingIconButton;

        private MaterialProgressIndicator _activityIndicatorTrailing;

        private MaterialProgressIndicator _activityIndicatorLeading;

        #endregion Layout

        #region Constructors

        public MaterialTopAppBar()
		{
            CreateLayout();
        }

        #endregion Constructors

        #region Methods

        private void CreateLayout()
        {
            VerticalOptions = LayoutOptions.Start;
            Padding = new Thickness(4, 8);
            RowSpacing = 0;
            ColumnSpacing = 0;

            RowDefinitions.Add(new RowDefinition { Height = SmallRowHeight });
            RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            ColumnDefinitions.Add(new ColumnDefinition { Width = SmallRowHeight });
            ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            ColumnDefinitions.Add(new ColumnDefinition { Width = SmallRowHeight });

            _headlineLabel = new MaterialLabel
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HeightRequest = SmallRowHeight
            };
            _headlineLabel.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Headline), source: this));
            _headlineLabel.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(HeadlineColor), source: this));
            _headlineLabel.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(HeadlineFontSize), source: this));
            _headlineLabel.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(HeadlineFontFamily), source: this));
            _headlineLabel.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(HeadlineFontAttributes), source: this));
            _headlineLabel.SetBinding(MaterialLabel.MarginProperty, new Binding(nameof(HeadlineMarginAdjustment), source: this));
            this.Add(_headlineLabel, 0, 0);
            Grid.SetColumnSpan(_headlineLabel, 3);

            _descriptionLabel = new MaterialLabel
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            _descriptionLabel.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(DescriptionColor), source: this));
            _descriptionLabel.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(DescriptionFontSize), source: this));
            _descriptionLabel.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(DescriptionFontFamily), source: this));
            _descriptionLabel.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(DescriptionFontAttributes), source: this));
            _descriptionLabel.SetBinding(MaterialLabel.MarginProperty, new Binding(nameof(DescriptionMarginAdjustment), source: this));

            _leadingIconButton = new MaterialIconButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = false
            };
            _leadingIconButton.SetBinding(MaterialIconButton.WidthRequestProperty, new Binding(nameof(IconSize), source: this));
            _leadingIconButton.SetBinding(MaterialIconButton.HeightRequestProperty, new Binding(nameof(IconSize), source: this));
            _leadingIconButton.SetBinding(MaterialIconButton.AnimationProperty, new Binding(nameof(IconButtonAnimation), source: this));
            _leadingIconButton.SetBinding(MaterialIconButton.AnimationParameterProperty, new Binding(nameof(IconButtonAnimationParameter), source: this));
            _leadingIconButton.SetBinding(MaterialIconButton.CustomAnimationProperty, new Binding(nameof(IconButtonCustomAnimation), source: this));
            this.Add(_leadingIconButton, 0, 0);

            var busyIndicatorMargin = GetBusyIndicatorMargin();

            _activityIndicatorLeading = new MaterialProgressIndicator
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = BusyIndicatorSize,
                HeightRequest = BusyIndicatorSize,
                Margin = busyIndicatorMargin,
                IsVisible = false
            };
            _activityIndicatorLeading.SetBinding(MaterialProgressIndicator.IndicatorColorProperty, new Binding(nameof(BusyIndicatorColor), source: this));
            this.Add(_activityIndicatorLeading, 0, 0);

            _trailingIconButton = new MaterialIconButton
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = false
            };
            _trailingIconButton.SetBinding(MaterialIconButton.WidthRequestProperty, new Binding(nameof(IconSize), source: this));
            _trailingIconButton.SetBinding(MaterialIconButton.HeightRequestProperty, new Binding(nameof(IconSize), source: this));
            _trailingIconButton.SetBinding(MaterialIconButton.AnimationProperty, new Binding(nameof(IconButtonAnimation), source: this));
            _trailingIconButton.SetBinding(MaterialIconButton.AnimationParameterProperty, new Binding(nameof(IconButtonAnimationParameter), source: this));
            _trailingIconButton.SetBinding(MaterialIconButton.CustomAnimationProperty, new Binding(nameof(IconButtonCustomAnimation), source: this));
            this.Add(_trailingIconButton, 2, 0);

            _activityIndicatorTrailing = new MaterialProgressIndicator
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = BusyIndicatorSize,
                HeightRequest = BusyIndicatorSize,
                Margin = busyIndicatorMargin,
                IsVisible = false
            };
            _activityIndicatorTrailing.SetBinding(MaterialProgressIndicator.IndicatorColorProperty, new Binding(nameof(BusyIndicatorColor), source: this));
            this.Add(_activityIndicatorTrailing, 2, 0);

            UpdateLayoutAfterTypeChanged(Type);
        }

        private void UpdateLayoutAfterTypeChanged(MaterialTopAppBarType type)
        {
            switch (type)
            {
                case MaterialTopAppBarType.Small:
                    _leadingIconButton.VerticalOptions = LayoutOptions.Center;
                    _activityIndicatorLeading.VerticalOptions = LayoutOptions.Center;
                    _trailingIconButton.VerticalOptions = LayoutOptions.Center;
                    _activityIndicatorTrailing.VerticalOptions = LayoutOptions.Center;
                    _headlineLabel.HorizontalTextAlignment = TextAlignment.Start;
                    _headlineLabel.Margin = new Thickness(SmallLabelLateralMargin, HeadlineMarginAdjustment.Top, SmallLabelLateralMargin, HeadlineMarginAdjustment.Bottom);
                    _descriptionLabel.HorizontalTextAlignment = TextAlignment.Start;
                    _descriptionLabel.Margin = new Thickness(_descriptionLateralMargin, DescriptionMarginAdjustment.Top, _descriptionLateralMargin, DescriptionMarginAdjustment.Bottom);
                    break;
                case MaterialTopAppBarType.Medium:
                    _leadingIconButton.VerticalOptions = LayoutOptions.Start;
                    _activityIndicatorLeading.VerticalOptions = LayoutOptions.Start;
                    _trailingIconButton.VerticalOptions = LayoutOptions.Start;
                    _activityIndicatorTrailing.VerticalOptions = LayoutOptions.Start;
                    _headlineLabel.HorizontalTextAlignment = TextAlignment.Start;
                    _headlineLabel.VerticalOptions = LayoutOptions.End;
                    _headlineLabel.Margin = new Thickness(MediumLabelLateralMargin, HeadlineMarginAdjustment.Top, MediumLabelLateralMargin, HeadlineMarginAdjustment.Bottom);
                    _headlineLabel.FontSize = MaterialFontSize.HeadlineSmall;
                    RowDefinitions[0].Height = new GridLength(MediumRowHeight);
                    _descriptionLabel.HorizontalTextAlignment = TextAlignment.Start;
                    _descriptionLabel.Margin = new Thickness(_descriptionLateralMargin, DescriptionMarginAdjustment.Top, _descriptionLateralMargin, DescriptionMarginAdjustment.Bottom);
                    break;
                case MaterialTopAppBarType.Large:
                    _leadingIconButton.VerticalOptions = LayoutOptions.Start;
                    _activityIndicatorLeading.VerticalOptions = LayoutOptions.Start;
                    _trailingIconButton.VerticalOptions = LayoutOptions.Start;
                    _activityIndicatorTrailing.VerticalOptions = LayoutOptions.Start;
                    _headlineLabel.HorizontalTextAlignment = TextAlignment.Start;
                    _headlineLabel.VerticalOptions = LayoutOptions.End;
                    _headlineLabel.Margin = new Thickness(LargeLabelLateralMargin, HeadlineMarginAdjustment.Top, LargeLabelLateralMargin, HeadlineMarginAdjustment.Bottom);
                    _headlineLabel.FontSize = MaterialFontSize.HeadlineMedium;
                    RowDefinitions[0].Height = new GridLength(LargeRowHeight);
                    _descriptionLabel.HorizontalTextAlignment = TextAlignment.Start;
                    _descriptionLabel.Margin = new Thickness(_descriptionLateralMargin, DescriptionMarginAdjustment.Top, _descriptionLateralMargin, DescriptionMarginAdjustment.Bottom);
                    break;
            }
        }

        private void SetDescription()
        {
            if (!string.IsNullOrEmpty(Description))
            {
                this.Add(_descriptionLabel, 0, 1);
                Grid.SetColumnSpan(_descriptionLabel, 3);
            }
            else
            {
                Children.Remove(_descriptionLabel);
            }

            _descriptionLabel.Text = Description;
        }

        private void SetLeadingIconCommand()
        {
            _leadingIconButton.Command = LeadingIconCommand;
            _leadingIconButton.IsVisible = LeadingIconIsVisible;
        }

        private void SetTrailingIconCommand()
        {
            _trailingIconButton.Command = TrailingIconCommand;
            _trailingIconButton.IsVisible = TrailingIconIsVisible;
        }

        private void SetLeadingIcon()
        {
            _leadingIconButton.ImageSource = LeadingIcon;
            _leadingIconButton.IsVisible = LeadingIcon != null;
        }

        private void SetTrailingIcon()
        {
            _trailingIconButton.ImageSource = TrailingIcon;
            _trailingIconButton.IsVisible = TrailingIcon != null;
        }

        private void SetLeadingIconIsBusy()
        {
            if (LeadingIconIsBusy)
            {
                _activityIndicatorLeading.IsVisible = true;
                _leadingIconButton.IsVisible = false;
            }
            else
            {
                _activityIndicatorLeading.IsVisible = false;
                _leadingIconButton.IsVisible = true;
            }
        }

        private void SetTrailingIconIsBusy()
        {
            if (TrailingIconIsBusy)
            {
                _activityIndicatorTrailing.IsVisible = true;
                _trailingIconButton.IsVisible = false;
            }
            else
            {
                _activityIndicatorTrailing.IsVisible = false;
                _trailingIconButton.IsVisible = true;
            }
        }

        private void SetBusyIndicatorSize()
        {
            var busyIndicatorMargin = GetBusyIndicatorMargin();
            _activityIndicatorLeading.Margin = busyIndicatorMargin;
            _activityIndicatorLeading.WidthRequest = BusyIndicatorSize;
            _activityIndicatorLeading.HeightRequest = BusyIndicatorSize;
            _activityIndicatorTrailing.Margin = busyIndicatorMargin;
            _activityIndicatorTrailing.WidthRequest = BusyIndicatorSize;
            _activityIndicatorTrailing.HeightRequest = BusyIndicatorSize;
        }

        private Thickness GetBusyIndicatorMargin()
        {
            return BusyIndicatorSize < SmallRowHeight ?
                new Thickness((SmallRowHeight - BusyIndicatorSize) / 2) :
                new Thickness(0);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == nameof(Window)
                && Window != null)
            {
                // Window property is setted with a value when the view is appearing
                if (!string.IsNullOrEmpty(ScrollViewName)
                        && (Type == MaterialTopAppBarType.Medium || Type == MaterialTopAppBarType.Large))
                {
                    SetScrollViewAnimation();
                }
            }

            base.OnPropertyChanged(propertyName);
        }

        private void SetScrollViewAnimation()
        {
            try
            {
                var viewByName = Parent.FindByName(ScrollViewName);
                if (viewByName != null && viewByName is ScrollView scrollView)
                {
                    int maxHeight = Type == MaterialTopAppBarType.Large ? LargeRowHeight : MediumRowHeight;
                    int minHeight = SmallRowHeight;

                    double maxFontSize = Type == MaterialTopAppBarType.Large ? MaterialFontSize.HeadlineMedium : MaterialFontSize.HeadlineSmall;
                    double minFontSize = HeadlineFontSize;

                    int maxLabelLateralMargin = Type == MaterialTopAppBarType.Large ? LargeLabelLateralMargin : MediumLabelLateralMargin;
                    int minLabelLateralMargin = SmallLabelLateralMargin;

                    //if (DeviceInfo.Platform == DevicePlatform.Android)
                    //{
                    //    scrollView.Effects.Add(new TouchReleaseEffect(() =>
                    //    {
                    //        ScrollAnimation(scrollView.ScrollY, maxHeight, minHeight, maxFontSize, minFontSize, maxLabelLateralMargin, minLabelLateralMargin);

                    //        Task.Run(async () =>
                    //        {
                    //            await Task.Delay(500);
                    //            if (_isCollapsed && scrollView.ScrollY <= 0)
                    //            {
                    //                ExpandTopAppBar(maxHeight, minHeight, maxFontSize, minFontSize, maxLabelLateralMargin, minLabelLateralMargin);
                    //            }
                    //        });
                    //    }));
                    //}
                    //else
                    //{
                    //    scrollView.Scrolled += (s, e) =>
                    //    {
                    //        ScrollAnimation(e.ScrollY, maxHeight, minHeight, maxFontSize, minFontSize, maxLabelLateralMargin, minLabelLateralMargin);
                    //    };
                    //}

                    scrollView.Scrolled += (s, e) =>
                    {
                        ScrollAnimation(e.ScrollY, maxHeight, minHeight, maxFontSize, minFontSize, maxLabelLateralMargin, minLabelLateralMargin);
                    };
                }
                else
                    Logger.Debug($"The view with name '{ScrollViewName}' wasn't found or it isn't a ScrollView");
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        private void ScrollAnimation(double scrollY, int maxHeight, int minHeight, double maxFontSize, double minFontSize, int maxLabelLateralMargin, int minLabelLateralMargin)
        {
            if (_isCollapsed && scrollY <= 0)
            {
                ExpandTopAppBar(maxHeight, minHeight, maxFontSize, minFontSize, maxLabelLateralMargin, minLabelLateralMargin);
            }
            else if (!_isCollapsed && scrollY >= 70)
            {
                CollapseTopAppBar(maxHeight, minHeight, maxFontSize, minFontSize, maxLabelLateralMargin, minLabelLateralMargin);
            }
        }

        private void ExpandTopAppBar(int maxHeight, int minHeight, double maxFontSize, double minFontSize, int maxLabelLateralMargin, int minLabelLateralMargin)
        {
            _isCollapsed = false;

            var mainAnimation = new Animation();
            mainAnimation.Add(0, 1, new Animation(v => RowDefinitions[0].Height = new GridLength(v), minHeight, maxHeight, Easing.Linear));
            mainAnimation.Add(0, 1, new Animation(v => _headlineLabel.FontSize = v, minFontSize, maxFontSize, Easing.Linear));
            mainAnimation.Add(0, 1, new Animation(v => _headlineLabel.Margin = new Thickness(v, HeadlineMarginAdjustment.Top, v, HeadlineMarginAdjustment.Bottom), minLabelLateralMargin, maxLabelLateralMargin, Easing.SinIn));
            mainAnimation.Commit(this, $"{nameof(MaterialTopAppBar)}{Id}", 16, (uint)ScrollViewAnimationLength, null);
        }

        private void CollapseTopAppBar(int maxHeight, int minHeight, double maxFontSize, double minFontSize, int maxLabelLateralMargin, int minLabelLateralMargin)
        {
            _isCollapsed = true;

            var mainAnimation = new Animation();
            mainAnimation.Add(0, 1, new Animation(v => RowDefinitions[0].Height = new GridLength(v), maxHeight, minHeight, Easing.Linear));
            mainAnimation.Add(0, 1, new Animation(v => _headlineLabel.FontSize = v, maxFontSize, minFontSize, Easing.Linear));
            mainAnimation.Add(0, 1, new Animation(v => _headlineLabel.Margin = new Thickness(v, HeadlineMarginAdjustment.Top, v, HeadlineMarginAdjustment.Bottom), maxLabelLateralMargin, minLabelLateralMargin, Easing.SinOut));
            mainAnimation.Commit(this, $"{nameof(MaterialTopAppBar)}{Id}", 16, (uint)ScrollViewAnimationLength, null);
        }

        #endregion Methods
    }
}