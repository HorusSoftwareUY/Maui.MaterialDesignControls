using System.Collections;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public enum MaterialTopAppBarType
    {
        /// <summary>Center and aligned headline</summary>
        CenterAligned,
        /// <summary>Small headline below the leading icon</summary>
        Small,
        /// <summary>Medium headline below the leading icon</summary>
        Medium,
        /// <summary>Large headline below the leading icon</summary>
        Large
    }

    /// <summary>
    /// A top app bar <see cref="View" /> that display navigation, actions, and text at the top of a screen, and follows Material Design Guidelines <see href="https://m3.material.io/components/top-app-bar/overview" >see here.</see>
    /// </summary>
    /// <example>
    ///
    /// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialTopAppbar.gif</img>
    ///
    /// <h3>XAML sample</h3>
    /// <code>
    /// <xaml>
    /// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
    /// 
    /// &lt; material:MaterialTopAppBar
    ///         Headline="Large type"
    ///         Description="Description text"
    ///         LeadingIconCommand="{Binding LeadingIconTapCommand}"
    ///         LeadingIcon="ic_back_b.png"
    ///         ScrollViewName="scrollView"
    ///         Type="Large" /&gt;
    /// </xaml>
    /// </code>
    /// 
    /// <h3>C# sample</h3>
    /// <code>
    /// var topAppBar = new MaterialTopAppBar
    /// {
    ///     Headline = "Large type",
    ///     Description = "Description text",
    ///     LeadingIconCommand = LeadingIconTap,
    ///     LeadingIcon = "ic_back_b.png",
    ///     ScrollViewName = "scrollView",
    ///     Type = MaterialTopAppBarType.Large,
    /// };
    ///</code>
    ///
    /// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/TopAppBarPage.xaml)
    /// 
    /// </example>
    /// <todoList>
    /// * [iOS] The scroll animation has lag by the headline size change
    /// </todoList>
    public class MaterialTopAppBar : Grid
    {
        #region Attributes

        private static readonly MaterialTopAppBarType DefaultType = MaterialTopAppBarType.CenterAligned;
        private static readonly string DefaultHeadline = null;
        private static readonly Color DefaultHeadlineColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
        private static readonly double DefaultHeadlineFontSize = MaterialFontSize.TitleLarge;
        private static readonly string DefaultHeadlineFontFamily = MaterialFontFamily.Default;
        private static readonly FontAttributes DefaultHeadlineFontAttributes = FontAttributes.None;
        private static readonly Thickness DefaultHeadlineMarginAdjustment = default(Thickness);
        private static readonly string DefaultDescription = null;
        private static readonly Color DefaultDescriptionColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
        private static readonly double DefaultDescriptionFontSize = MaterialFontSize.TitleMedium;
        private static readonly string DefaultDescriptionFontFamily = MaterialFontFamily.Default;
        private static readonly FontAttributes DefaultDescriptionFontAttributes = FontAttributes.None;
        private static readonly Thickness DefaultDescriptionMarginAdjustment = new Thickness(DescriptionLateralMargin, 0, DescriptionLateralMargin, 0);
        private static readonly ImageSource DefaultLeadingIcon = null;
        private static readonly ICommand DefaultLeadingIconCommand = null;
        private static readonly bool DefaultLeadingIconIsBusy = false;
        private static readonly IList DefaultTrailingIcons = null;
        private static readonly double DefaultIconSize = 48.0;
        private static readonly AnimationTypes DefaultIconButtonAnimationType = MaterialAnimation.Type;
#nullable enable
        private static readonly double? DefaultIconButtonAnimationParameter = MaterialAnimation.Parameter;
#nullable disable
        private static readonly ICustomAnimation DefaultIconButtonCustomAnimation = null;
        private static readonly Color DefaultBusyIndicatorColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
        private static readonly double DefaultBusyIndicatorSize = 24.0;
        private static readonly string DefaultScrollViewName = null;
        private static readonly int DefaultScrollViewAnimationLength = 250;

        private const double DescriptionLateralMargin = 10;
        private const int SmallRowHeight = 48;
        private const int MediumRowHeight = 106;
        private const int LargeRowHeight = 106;
        private const int SmallLabelLateralMargin = 48;
        private const int MediumLabelLateralMargin = 10;
        private const int LargeLabelLateralMargin = 10;

        private bool _isCollapsed = false;
        private IList _trailingIcons;

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
        public static readonly BindableProperty HeadlineProperty = BindableProperty.Create(nameof(Headline), typeof(string), typeof(MaterialTopAppBar), defaultValue: DefaultHeadline, BindingMode.OneTime);

        /// <summary>
        /// The backing store for the <see cref="HeadlineColor" /> bindable property.
        /// </summary>
        public static readonly BindableProperty HeadlineColorProperty = BindableProperty.Create(nameof(HeadlineColor), typeof(Color), typeof(MaterialTopAppBar), defaultValue: DefaultHeadlineColor, BindingMode.OneTime);

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
        public static readonly BindableProperty HeadlineMarginAdjustmentProperty = BindableProperty.Create(nameof(HeadlineMarginAdjustment), typeof(Thickness), typeof(MaterialTopAppBar), defaultValue: DefaultHeadlineMarginAdjustment, BindingMode.OneTime);

        /// <summary>
        /// The backing store for the <see cref="Description" /> bindable property.
        /// </summary>
        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(MaterialTopAppBar), defaultValue: DefaultDescription, BindingMode.OneTime, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetDescription();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="DescriptionColor" /> bindable property.
        /// </summary>
        public static readonly BindableProperty DescriptionColorProperty = BindableProperty.Create(nameof(DescriptionColor), typeof(Color), typeof(MaterialTopAppBar), defaultValue: DefaultDescriptionColor, BindingMode.OneTime);

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
        public static readonly BindableProperty LeadingIconProperty = BindableProperty.Create(nameof(LeadingIcon), typeof(ImageSource), typeof(MaterialTopAppBar), defaultValue: DefaultLeadingIcon, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetLeadingIcon();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="LeadingIconCommand" /> bindable property.
        /// </summary>
        public static readonly BindableProperty LeadingIconCommandProperty = BindableProperty.Create(nameof(LeadingIconCommand), typeof(ICommand), typeof(MaterialTopAppBar), defaultValue: DefaultLeadingIconCommand, BindingMode.OneTime, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetLeadingIconCommand();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="LeadingIconIsBusy" /> bindable property.
        /// </summary>
        public static readonly BindableProperty LeadingIconIsBusyProperty = BindableProperty.Create(nameof(LeadingIconIsBusy), typeof(bool), typeof(MaterialTopAppBar), defaultValue: DefaultLeadingIconIsBusy, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetLeadingIconIsBusy();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="TrailingIcons" /> bindable property.
        /// </summary>
        public static readonly BindableProperty TrailingIconsProperty = BindableProperty.Create(nameof(TrailingIcons), typeof(IList), typeof(MaterialTopAppBar), defaultValue: DefaultTrailingIcons, propertyChanged: (bindable, o, n) =>
        {
            if (bindable is MaterialTopAppBar self)
            {
                self.SetTrailingIcons();
            }
        });

        /// <summary>
        /// The backing store for the <see cref="IconSize" /> bindable property.
        /// </summary>
        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(nameof(IconSize), typeof(double), typeof(MaterialTopAppBar), defaultValue: DefaultIconSize);

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
        public static readonly BindableProperty IconButtonCustomAnimationProperty = BindableProperty.Create(nameof(IconButtonCustomAnimation), typeof(ICustomAnimation), typeof(MaterialTopAppBar), defaultValue: DefaultIconButtonCustomAnimation);

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
        public static readonly BindableProperty ScrollViewNameProperty = BindableProperty.Create(nameof(ScrollViewName), typeof(string), typeof(MaterialTopAppBar), defaultValue: DefaultScrollViewName, BindingMode.OneTime);

        /// <summary>
        /// The backing store for the <see cref="ScrollViewAnimationLength" /> bindable property.
        /// </summary>
        public static readonly BindableProperty ScrollViewAnimationLengthProperty = BindableProperty.Create(nameof(ScrollViewAnimationLength), typeof(int), typeof(MaterialTopAppBar), defaultValue: DefaultScrollViewAnimationLength);

        #endregion Bindable Properties

        #region Properties

        /// <summary>
        /// Gets or sets the top app bar type according to <see cref="MaterialTopAppBarType"/> enum.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="MaterialTopAppBarType.CenterAligned">MaterialTopAppBarType.CenterAligned</see>
        /// </default>
        /// <remarks>
        /// <para>CenterAligned: Center and aligned headline.</para>
        /// <para>Small: Small headline below the leading icon.</para>
        /// <para>Medium: Medium headline below the leading icon.</para>
        /// <para>Large: Large headline below the leading icon.</para>
        /// </remarks>
        public MaterialTopAppBarType Type
        {
            get => (MaterialTopAppBarType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        /// <summary>
        /// Gets or sets the headline text displayed on the top app bar.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see langword="null"/>
        /// </default>
        public string Headline
        {
            get => (string)GetValue(HeadlineProperty);
            set => SetValue(HeadlineProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the headline text of the top app bar. 
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// Theme: Light = <see cref="MaterialLightTheme.Text">MaterialLightTheme.Text</see> - Dark = <see cref="MaterialDarkTheme.Text">MaterialDarkTheme.Text</see>
        /// </default>
        public Color HeadlineColor
        {
            get => (Color)GetValue(HeadlineColorProperty);
            set => SetValue(HeadlineColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the font for the headline text of this top app bar. 
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="MaterialFontSize.LabelLarge">MaterialFontSize.LabelLarge</see> / Tablet: 14 - Phone: 11
        /// <default>
        [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
        public double HeadlineFontSize
        {
            get { return (double)GetValue(HeadlineFontSizeProperty); }
            set { SetValue(HeadlineFontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font family for the headline text of this top app bar. 
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
        /// </default>
        public string HeadlineFontFamily
        {
            get { return (string)GetValue(HeadlineFontFamilyProperty); }
            set { SetValue(HeadlineFontFamilyProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the font of this top app bar headline text is bold, italic, or neither.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="FontAttributes.None">FontAttributes.None</see>
        /// </default>
        public FontAttributes HeadlineFontAttributes
        {
            get => (FontAttributes)GetValue(HeadlineFontAttributesProperty);
            set => SetValue(HeadlineFontAttributesProperty, value);
        }

        /// <summary>
        /// Allows you to adjust the margins of the headline text. 
        /// This is a bindable property.
        /// </summary>
        /// <remarks>This property does not take into account the Left and Right of the set <see cref="Thickness" />, it only applies the Top and Bottom values.</remarks>
        /// <default>
        /// <see cref="Thickness">default(Thickness)</see>
        /// </default>
        public Thickness HeadlineMarginAdjustment
        {
            get => (Thickness)GetValue(HeadlineMarginAdjustmentProperty);
            set => SetValue(HeadlineMarginAdjustmentProperty, value);
        }

        /// <summary>
        /// Gets or sets the description text displayed on the top app bar.   
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see langword="null"/>
        /// </default>
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the description text of the top app bar. 
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// Theme: Light = <see cref="MaterialLightTheme.Text">MaterialLightTheme.Text</see> - Dark = <see cref="MaterialDarkTheme.Text">MaterialDarkTheme.Text</see>
        /// </default>
        public Color DescriptionColor
        {
            get => (Color)GetValue(DescriptionColorProperty);
            set => SetValue(DescriptionColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the font for the description text of this top app bar. 
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="MaterialFontSize.TitleMedium">MaterialFontSize.TitleMedium</see> / Tablet = 19 / Phone = 16
        /// </default>
        [System.ComponentModel.TypeConverter(typeof(FontSizeConverter))]
        public double DescriptionFontSize
        {
            get { return (double)GetValue(DescriptionFontSizeProperty); }
            set { SetValue(DescriptionFontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font family for the description text of this top app bar. 
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="MaterialFontFamily.Default">MaterialFontFamily.Default</see>
        /// </default>
        public string DescriptionFontFamily
        {
            get { return (string)GetValue(DescriptionFontFamilyProperty); }
            set { SetValue(DescriptionFontFamilyProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the font of this top app bar description text is bold, italic, or neither.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="FontAttributes.None">FontAttributes.None</see>
        /// </default>
        public FontAttributes DescriptionFontAttributes
        {
            get => (FontAttributes)GetValue(DescriptionFontAttributesProperty);
            set => SetValue(DescriptionFontAttributesProperty, value);
        }

        /// <summary>
        /// Allows you to adjust the margins of the description text. 
        /// This is a bindable property.
        /// </summary>
        /// <remarks>This property does not take into account the Left and Right of the set <see cref="Thickness" />, it only applies the Top and Bottom values.</remarks>
        /// <default>
        /// new Thickness(10, 0, 10, 0)
        /// </default>
        public Thickness DescriptionMarginAdjustment
        {
            get => (Thickness)GetValue(DescriptionMarginAdjustmentProperty);
            set => SetValue(DescriptionMarginAdjustmentProperty, value);
        }

        /// <summary>
        /// Allows you to display an icon button on the left side of the top app bar. 
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see langword="null"/>
        /// </default>
        public ImageSource LeadingIcon
        {
            get { return (ImageSource)GetValue(LeadingIconProperty); }
            set { SetValue(LeadingIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command to invoke when the leading icon button is clicked. 
        /// This is a bindable property.
        /// </summary>
        /// <remarks>This property is used to associate a command with an instance of a top app bar. This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel.</remarks>
        /// <default>
        /// <see langword="null"/>
        /// </default>
        public ICommand LeadingIconCommand
        {
            get => (ICommand)GetValue(LeadingIconCommandProperty);
            set => SetValue(LeadingIconCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets if the leading icon button is on busy state (executing Command).        
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see langword="false"/>
        /// </default>
        public bool LeadingIconIsBusy
        {
            get { return (bool)GetValue(LeadingIconIsBusyProperty); }
            set { SetValue(LeadingIconIsBusyProperty, value); }
        }

        private IList<TrailingIcon> trailingIcons;

        /// <summary>
        /// Allows you to display a list of icon buttons on the right side of the top app bar. 
        /// This is a bindable property.
        /// </summary>
        /// <remarks>This property supports a maximum of 3 icon buttons.</remarks>
        /// <default>
        /// <see langword="null"/>
        /// </default>
        public IList TrailingIcons
        {
            get { return (IList)GetValue(TrailingIconsProperty); }
            set { SetValue(TrailingIconsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size of the <see cref="MaterialTopAppBar.LeadingIcon"/> and <see cref="MaterialTopAppBar.TrailingIcon"/> of this top app bar.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// 48.0
        /// </default>
        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets an animation to be executed when leading and trailing icon button are clicked.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see cref="AnimationTypes.Fade">AnimationTypes.Fade</see>
        /// </default>
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
        /// <default>
        /// 0.7
        /// </default>
        public double? IconButtonAnimationParameter
        {
            get { return (double?)GetValue(IconButtonAnimationParameterProperty); }
            set { SetValue(IconButtonAnimationParameterProperty, value); }
        }
#nullable disable

        /// <summary>
        /// Gets or sets a custom animation to be executed when leading and trailing icon button are clicked.        
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see langword="null"/>
        /// </default>
        public ICustomAnimation IconButtonCustomAnimation
        {
            get { return (ICustomAnimation)GetValue(IconButtonCustomAnimationProperty); }
            set { SetValue(IconButtonCustomAnimationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="Color" /> for the busy indicators.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// Theme: Light = <see cref="MaterialLightTheme.Primary">MaterialLightTheme.Primary</see> - Dark = <see cref="MaterialDarkTheme.Primary">MaterialDarkTheme.Primary</see>
        /// </default>
        public Color BusyIndicatorColor
        {
            get { return (Color)GetValue(BusyIndicatorColorProperty); }
            set { SetValue(BusyIndicatorColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the size for the busy indicators.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// 24.0
        /// </default>
        public double BusyIndicatorSize
        {
            get { return (double)GetValue(BusyIndicatorSizeProperty); }
            set { SetValue(BusyIndicatorSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name of the <see cref="ScrollView" /> element to which the top app bar will be linked to run collapse or expand animations depending on the user's scroll.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see langword="null"/>
        /// </default>
        public string ScrollViewName
        {
            get => (string)GetValue(ScrollViewNameProperty);
            set => SetValue(ScrollViewNameProperty, value);
        }

        /// <summary>
        /// Gets or sets the duration of the collapse or expand animation bound to the <see cref="ScrollView" /> element. 
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// 250
        /// </default>
        public int ScrollViewAnimationLength
        {
            get { return (int)GetValue(ScrollViewAnimationLengthProperty); }
            set { SetValue(ScrollViewAnimationLengthProperty, value); }
        }

        #endregion Properties

        #region Layout

        private MaterialLabel _headlineLabel;

        private MaterialLabel _descriptionLabel;

        private MaterialIconButton _leadingIconButton;

        private MaterialProgressIndicator _leadingActivityIndicator;

        private List<MaterialIconButton> _trailingIconButtons;

        private List<MaterialProgressIndicator> _trailingActivityIndicators;

        private ColumnDefinition _secondTrailingColumnDefinition;

        private ColumnDefinition _thirdTrailingColumnDefinition;

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
            TrailingIcons = new List<TrailingIcon>();

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
                HeightRequest = SmallRowHeight,
                LineBreakMode = LineBreakMode.TailTruncation
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
                VerticalTextAlignment = TextAlignment.Center,
                LineBreakMode = LineBreakMode.WordWrap
            };
            _descriptionLabel.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(DescriptionColor), source: this));
            _descriptionLabel.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(DescriptionFontSize), source: this));
            _descriptionLabel.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(DescriptionFontFamily), source: this));
            _descriptionLabel.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(DescriptionFontAttributes), source: this));
            _descriptionLabel.SetBinding(MaterialLabel.MarginProperty, new Binding(nameof(DescriptionMarginAdjustment), source: this));
            Grid.SetColumnSpan(_descriptionLabel, 3);

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

            _leadingActivityIndicator = new MaterialProgressIndicator
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = BusyIndicatorSize,
                HeightRequest = BusyIndicatorSize,
                Margin = busyIndicatorMargin,
                IsVisible = false
            };
            _leadingActivityIndicator.SetBinding(MaterialProgressIndicator.IndicatorColorProperty, new Binding(nameof(BusyIndicatorColor), source: this));
            this.Add(_leadingActivityIndicator, 0, 0);

            UpdateLayoutAfterTypeChanged(Type);
        }

        private void UpdateLayoutAfterTypeChanged(MaterialTopAppBarType type)
        {
            switch (type)
            {
                case MaterialTopAppBarType.Small:
                    _leadingIconButton.VerticalOptions = LayoutOptions.Center;
                    _leadingActivityIndicator.VerticalOptions = LayoutOptions.Center;

                    if (_trailingIconButtons != null)
                    {
                        foreach (var trailingIconButton in _trailingIconButtons)
                        {
                            trailingIconButton.VerticalOptions = LayoutOptions.Center;
                        }
                    }

                    if (_trailingActivityIndicators != null)
                    {
                        foreach (var trailingActivityIndicator in _trailingActivityIndicators)
                        {
                            trailingActivityIndicator.VerticalOptions = LayoutOptions.Center;
                        }
                    }

                    _headlineLabel.HorizontalTextAlignment = TextAlignment.Start;
                    _headlineLabel.Margin = new Thickness(SmallLabelLateralMargin, HeadlineMarginAdjustment.Top, SmallLabelLateralMargin, HeadlineMarginAdjustment.Bottom);
                    _descriptionLabel.HorizontalTextAlignment = TextAlignment.Start;
                    _descriptionLabel.Margin = new Thickness(DescriptionLateralMargin, DescriptionMarginAdjustment.Top, DescriptionLateralMargin, DescriptionMarginAdjustment.Bottom);
                    break;
                case MaterialTopAppBarType.Medium:
                    _leadingIconButton.VerticalOptions = LayoutOptions.Start;
                    _leadingActivityIndicator.VerticalOptions = LayoutOptions.Start;

                    if (_trailingIconButtons != null)
                    {
                        foreach (var trailingIconButton in _trailingIconButtons)
                        {
                            trailingIconButton.VerticalOptions = LayoutOptions.Start;
                        }
                    }

                    if (_trailingActivityIndicators != null)
                    {
                        foreach (var trailingActivityIndicator in _trailingActivityIndicators)
                        {
                            trailingActivityIndicator.VerticalOptions = LayoutOptions.Start;
                        }
                    }

                    _headlineLabel.HorizontalTextAlignment = TextAlignment.Start;
                    _headlineLabel.VerticalOptions = LayoutOptions.End;
                    _headlineLabel.Margin = new Thickness(MediumLabelLateralMargin, HeadlineMarginAdjustment.Top, MediumLabelLateralMargin, HeadlineMarginAdjustment.Bottom);
                    _headlineLabel.FontSize = MaterialFontSize.HeadlineSmall;
                    RowDefinitions[0].Height = new GridLength(MediumRowHeight);
                    _descriptionLabel.HorizontalTextAlignment = TextAlignment.Start;
                    _descriptionLabel.Margin = new Thickness(DescriptionLateralMargin, DescriptionMarginAdjustment.Top, DescriptionLateralMargin, DescriptionMarginAdjustment.Bottom);
                    break;
                case MaterialTopAppBarType.Large:
                    _leadingIconButton.VerticalOptions = LayoutOptions.Start;
                    _leadingActivityIndicator.VerticalOptions = LayoutOptions.Start;

                    if (_trailingIconButtons != null)
                    {
                        foreach (var trailingIconButton in _trailingIconButtons)
                        {
                            trailingIconButton.VerticalOptions = LayoutOptions.Start;
                        }
                    }

                    if (_trailingActivityIndicators != null)
                    {
                        foreach (var trailingActivityIndicator in _trailingActivityIndicators)
                        {
                            trailingActivityIndicator.VerticalOptions = LayoutOptions.Start;
                        }
                    }

                    _headlineLabel.HorizontalTextAlignment = TextAlignment.Start;
                    _headlineLabel.VerticalOptions = LayoutOptions.End;
                    _headlineLabel.Margin = new Thickness(LargeLabelLateralMargin, HeadlineMarginAdjustment.Top, LargeLabelLateralMargin, HeadlineMarginAdjustment.Bottom);
                    _headlineLabel.FontSize = MaterialFontSize.HeadlineMedium;
                    RowDefinitions[0].Height = new GridLength(LargeRowHeight);
                    _descriptionLabel.HorizontalTextAlignment = TextAlignment.Start;
                    _descriptionLabel.Margin = new Thickness(DescriptionLateralMargin, DescriptionMarginAdjustment.Top, DescriptionLateralMargin, DescriptionMarginAdjustment.Bottom);
                    break;
            }
        }

        private void SetDescription()
        {
            if (!string.IsNullOrEmpty(Description))
            {
                this.Add(_descriptionLabel, 0, 1);
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
            _leadingIconButton.IsVisible = LeadingIcon != null;
        }

        private void SetLeadingIcon()
        {
            _leadingIconButton.ImageSource = LeadingIcon;
            _leadingIconButton.IsVisible = LeadingIcon != null;
        }

        private void SetLeadingIconIsBusy()
        {
            if (LeadingIconIsBusy)
            {
                _leadingActivityIndicator.IsVisible = true;
                _leadingIconButton.IsVisible = false;
            }
            else
            {
                _leadingActivityIndicator.IsVisible = false;
                _leadingIconButton.IsVisible = true;
            }
        }

        private void SetBusyIndicatorSize()
        {
            var busyIndicatorMargin = GetBusyIndicatorMargin();
            _leadingActivityIndicator.Margin = busyIndicatorMargin;
            _leadingActivityIndicator.WidthRequest = BusyIndicatorSize;
            _leadingActivityIndicator.HeightRequest = BusyIndicatorSize;

            if (_trailingActivityIndicators != null)
            {
                foreach (var trailingActivityIndicator in _trailingActivityIndicators)
                {
                    trailingActivityIndicator.Margin = busyIndicatorMargin;
                    trailingActivityIndicator.WidthRequest = BusyIndicatorSize;
                    trailingActivityIndicator.HeightRequest = BusyIndicatorSize;
                }
            }
        }

        private Thickness GetBusyIndicatorMargin()
        {
            return BusyIndicatorSize < SmallRowHeight ?
                new Thickness((SmallRowHeight - BusyIndicatorSize) / 2) :
                new Thickness(0);
        }

        private void SetIconSize()
        {
            _leadingIconButton.WidthRequest = IconSize;
            _leadingIconButton.HeightRequest = IconSize;

            if (_trailingIconButtons != null)
            {
                foreach (var trailingIconButton in _trailingIconButtons)
                {
                    trailingIconButton.WidthRequest = IconSize;
                    trailingIconButton.HeightRequest = IconSize;
                }
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            SetBindingContextToTrailingIcons();
        }

        private void SetBindingContextToTrailingIcons()
        {
            if (TrailingIcons != null)
            {
                foreach (var item in TrailingIcons)
                {
                    if (item is TrailingIcon trailingIcon)
                    {
                        trailingIcon.BindingContext = this.BindingContext;
                    }
                }
            }
        }

        private void SetTrailingIcons()
        {
            RemoveTrailingIcons();

            _trailingIcons = TrailingIcons;

            if (TrailingIcons != null && TrailingIcons.Count > 0)
            {
                _trailingIconButtons = new List<MaterialIconButton>();
                _trailingActivityIndicators = new List<MaterialProgressIndicator>();

                SetBindingContextToTrailingIcons();

                if (Type == MaterialTopAppBarType.CenterAligned)
                {
                    if (TrailingIcons.Count > 1)
                    {
                        Logger.Debug("CenterAligned type only allows one TrailingIcon, only the first icon button will be displayed");
                    }

                    if (TrailingIcons[0] is TrailingIcon trailingIcon)
                    {
                        AddTrailingIcon(trailingIcon, 0);
                    }

                    Grid.SetColumnSpan(_headlineLabel, 3);
                    Grid.SetColumnSpan(_descriptionLabel, 3);
                }
                else
                {
                    if (TrailingIcons.Count > 3)
                    {
                        Logger.Debug("TrailingIcons supports a maximum of 3 icon buttons, only the 3 first icons button will be displayed");
                    }

                    if (TrailingIcons.Count >= 3)
                    {
                        _secondTrailingColumnDefinition = new ColumnDefinition { Width = SmallRowHeight };
                        ColumnDefinitions.Add(_secondTrailingColumnDefinition);
                        _thirdTrailingColumnDefinition = new ColumnDefinition { Width = SmallRowHeight };
                        ColumnDefinitions.Add(_thirdTrailingColumnDefinition);

                        Grid.SetColumnSpan(_headlineLabel, 5);
                        Grid.SetColumnSpan(_descriptionLabel, 5);
                    }
                    else if (TrailingIcons.Count == 2)
                    {
                        _secondTrailingColumnDefinition = new ColumnDefinition { Width = SmallRowHeight };
                        ColumnDefinitions.Add(_secondTrailingColumnDefinition);

                        Grid.SetColumnSpan(_headlineLabel, 4);
                        Grid.SetColumnSpan(_descriptionLabel, 4);
                    }

                    var trailingIconIndex = 0;
                    foreach (var item in TrailingIcons)
                    {
                        if (item is TrailingIcon trailingIcon)
                        {
                            AddTrailingIcon(trailingIcon, trailingIconIndex);
                            trailingIconIndex += 1;
                        }
                    }
                }
            }
            else if (_headlineLabel != null)
            {
                Grid.SetColumnSpan(_headlineLabel, 3);
                Grid.SetColumnSpan(_descriptionLabel, 3);
            }
        }

        private void AddTrailingIcon(TrailingIcon trailingIcon, int trailingIconIndex)
        {
            if (trailingIcon == null)
            {
                return;
            }

            trailingIcon.Index = trailingIconIndex;

            var trailingIconButtonsVerticalOptions = LayoutOptions.Center;

            if (Type == MaterialTopAppBarType.Medium
                || Type == MaterialTopAppBarType.Large)
            {
                trailingIconButtonsVerticalOptions = LayoutOptions.Start;
            }

            var trailingIconButton = new MaterialIconButton
            {
                ImageSource = trailingIcon.Icon,
                Command = trailingIcon.Command,
                IsBusy = trailingIcon.IsBusy,
                VerticalOptions = trailingIconButtonsVerticalOptions,
                HorizontalOptions = LayoutOptions.Center,
                IsVisible = true
            };
            trailingIconButton.SetBinding(MaterialIconButton.WidthRequestProperty, new Binding(nameof(IconSize), source: this));
            trailingIconButton.SetBinding(MaterialIconButton.HeightRequestProperty, new Binding(nameof(IconSize), source: this));
            trailingIconButton.SetBinding(MaterialIconButton.AnimationProperty, new Binding(nameof(IconButtonAnimation), source: this));
            trailingIconButton.SetBinding(MaterialIconButton.AnimationParameterProperty, new Binding(nameof(IconButtonAnimationParameter), source: this));
            trailingIconButton.SetBinding(MaterialIconButton.CustomAnimationProperty, new Binding(nameof(IconButtonCustomAnimation), source: this));
            _trailingIconButtons.Add(trailingIconButton);
            this.Add(trailingIconButton, trailingIconIndex + 2, 0);

            var busyIndicatorMargin = GetBusyIndicatorMargin();
            var activityIndicatorTrailing = new MaterialProgressIndicator
            {
                VerticalOptions = trailingIconButtonsVerticalOptions,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = BusyIndicatorSize,
                HeightRequest = BusyIndicatorSize,
                Margin = busyIndicatorMargin,
                IsVisible = false
            };
            activityIndicatorTrailing.SetBinding(MaterialProgressIndicator.IndicatorColorProperty, new Binding(nameof(BusyIndicatorColor), source: this));
            _trailingActivityIndicators.Add(activityIndicatorTrailing);
            this.Add(activityIndicatorTrailing, trailingIconIndex + 2, 0);

            trailingIcon.PropertyChanged += TrailingIcon_PropertyChanged;
        }

        private void TrailingIcon_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (sender is TrailingIcon trailingIcon
                && trailingIcon.Index.HasValue)
            {
                if (e.PropertyName == TrailingIcon.IsBusyProperty.PropertyName)
                {
                    if (trailingIcon.IsBusy)
                    {
                        _trailingActivityIndicators[trailingIcon.Index.Value].IsVisible = true;
                        _trailingIconButtons[trailingIcon.Index.Value].IsVisible = false;
                    }
                    else
                    {
                        _trailingActivityIndicators[trailingIcon.Index.Value].IsVisible = false;
                        _trailingIconButtons[trailingIcon.Index.Value].IsVisible = true;
                    }
                }
                else if (e.PropertyName == TrailingIcon.IsVisibleProperty.PropertyName)
                {
                    _trailingIconButtons[trailingIcon.Index.Value].IsVisible = trailingIcon.IsVisible;
                }
                else if (e.PropertyName == TrailingIcon.IsEnabledProperty.PropertyName)
                {
                    _trailingIconButtons[trailingIcon.Index.Value].IsEnabled = trailingIcon.IsEnabled;
                }
            }
        }

        private void RemoveTrailingIcons()
        {
            if (_trailingIconButtons != null)
            {
                foreach (var trailingIconButton in _trailingIconButtons)
                {
                    this.Remove(trailingIconButton);
                }
            }

            _trailingIconButtons = null;

            if (_trailingActivityIndicators != null)
            {
                foreach (var trailingActivityIndicator in _trailingActivityIndicators)
                {
                    this.Remove(trailingActivityIndicator);
                }
            }

            _trailingActivityIndicators = null;

            if (_secondTrailingColumnDefinition != null)
            {
                ColumnDefinitions.Remove(_secondTrailingColumnDefinition);
            }

            _secondTrailingColumnDefinition = null;

            if (_thirdTrailingColumnDefinition != null)
            {
                ColumnDefinitions.Remove(_thirdTrailingColumnDefinition);
            }

            _thirdTrailingColumnDefinition = null;

            if (_trailingIcons != null)
            {
                foreach (var item in _trailingIcons)
                {
                    if (item is TrailingIcon trailingIcon)
                    {
                        trailingIcon.PropertyChanged -= TrailingIcon_PropertyChanged;
                    }
                }
            }
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

                if (TrailingIcons != null
                    && (_trailingIconButtons == null || !_trailingIconButtons.Any()))
                {
                    SetTrailingIcons();
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

            if (TrailingIcons != null)
            {
                if (TrailingIcons.Count == 3)
                {
                    Grid.SetColumnSpan(_headlineLabel, 5);
                }
                else if (TrailingIcons.Count == 2)
                {
                    Grid.SetColumnSpan(_headlineLabel, 4);
                }
            }

            var mainAnimation = new Animation();
            mainAnimation.Add(0, 1, new Animation(v => RowDefinitions[0].Height = new GridLength(v), minHeight, maxHeight, Easing.Linear));
            mainAnimation.Add(0, 1, new Animation(v => _headlineLabel.FontSize = v, minFontSize, maxFontSize, Easing.Linear));
            mainAnimation.Add(0, 1, new Animation(v => _headlineLabel.Margin = new Thickness(v, HeadlineMarginAdjustment.Top, v, HeadlineMarginAdjustment.Bottom), minLabelLateralMargin, maxLabelLateralMargin, Easing.SinIn));
            mainAnimation.Commit(this, $"{nameof(MaterialTopAppBar)}{Id}", 16, (uint)ScrollViewAnimationLength, null);
        }

        private void CollapseTopAppBar(int maxHeight, int minHeight, double maxFontSize, double minFontSize, int maxLabelLateralMargin, int minLabelLateralMargin)
        {
            _isCollapsed = true;

            if (TrailingIcons != null)
            {
                Grid.SetColumnSpan(_headlineLabel, 3);
            }

            var mainAnimation = new Animation();
            mainAnimation.Add(0, 1, new Animation(v => RowDefinitions[0].Height = new GridLength(v), maxHeight, minHeight, Easing.Linear));
            mainAnimation.Add(0, 1, new Animation(v => _headlineLabel.FontSize = v, maxFontSize, minFontSize, Easing.Linear));
            mainAnimation.Add(0, 1, new Animation(v => _headlineLabel.Margin = new Thickness(v, HeadlineMarginAdjustment.Top, v, HeadlineMarginAdjustment.Bottom), maxLabelLateralMargin, minLabelLateralMargin, Easing.SinOut));
            mainAnimation.Commit(this, $"{nameof(MaterialTopAppBar)}{Id}", 16, (uint)ScrollViewAnimationLength, null);
        }

        #endregion Methods
    }
}