using HorusStudio.Maui.MaterialDesignControls.Utils;
using System.Runtime.CompilerServices;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A bottom sheet <see cref="View" /> show secondary content anchored to the bottom of the screen and follows Material Design Guidelines <see href="https://m3.material.io/components/bottom-sheets/overview" />.
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialBottomSheet.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialBottomSheet x:Name="materialBottomSheet1"&gt;
///                &lt;material:MaterialBottomSheet.Content&gt;
///                    &lt;VerticalStackLayout Spacing = "10"
///                                 Padding="22,44,22,12">
///                        &lt;material:MaterialLabel FontFamily = "{StaticResource BoldFont}"
///                                                 FontSize="{StaticResource H4FontSize}"
///                                                 Text="Material Design"
///                                                 TextColor="{StaticResource GradientColor1}" /&gt;
///                        &lt;material:MaterialLabel FontAttributes = "Bold"
///                                                 FontFamily="{StaticResource SemiBoldFont}"
///                                                 FontSize="{StaticResource H5FontSize}"
///                                                 Text="Plugin for Xamarin Forms"
///                                                 TextColor="{StaticResource BlackColor}" /&gt;
///                        &lt;material:MaterialLabel FontSize = "{StaticResource Body2FontSize}"
///                                                 Text="Material Design is an adaptable system of guidelines, components, and tools that support the best practices of user interface design. Backed by open-source code, Material Design streamlines collaboration between designers and developers, and helps teams quickly build beautiful products."
///                                                 TextColor="{StaticResource DarkGrayColor}" /&gt;
///                    &lt;/VerticalStackLayout&gt;
///                &lt;/material:MaterialBottomSheet.Content&gt;
///            &lt;/material:MaterialBottomSheet&gt;
/// </xaml>
/// </code>
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/BottomSheetPage.xaml)
/// 
/// </example>
public class MaterialBottomSheet : ContentView
{
    #region Attributes

    private readonly static Color DefaultScrimColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Scrim, Dark = MaterialDarkTheme.Scrim }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerLow, Dark = MaterialDarkTheme.SurfaceContainerLow }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultDragHandleColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();

    private double _currentPosition = 0;

    private double _bottomSafeArea = 0;

    private double _dragHandleMargin = 22;

    private double _translationYClosedCorrection = 10;

    private double _openPosition = 0;

    private BoxView _scrimBoxView;

    private ContentView _containerView;

    private MaterialCard _sheetView;

    private Grid _sheetViewLayout;

    private BoxView _dragHandleView;

    private double ContainerHeightWithBottomSafeArea => ContainerHeight + _bottomSafeArea;

    private double TranslationYClosed => ContainerHeight + _translationYClosedCorrection + (_bottomSafeArea * 2);

    #endregion Attributes

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="ContainerHeight" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ContainerHeightProperty = BindableProperty.Create(nameof(ContainerHeight), typeof(double), typeof(MaterialBottomSheet), defaultValue: -1.0, propertyChanged: (bindableObject, oldValue, newValue) => 
    { 
        if (bindableObject is MaterialBottomSheet self)
        {
            self.SetInitialState();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="ContainerRelativeHeight" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ContainerRelativeHeightProperty = BindableProperty.Create(nameof(ContainerRelativeHeight), typeof(double), typeof(MaterialBottomSheet), defaultValue: -1.0, propertyChanged: (bindableObject, oldValue, newValue) =>
    {
        if (bindableObject is MaterialBottomSheet self)
        {
            self.SetInitialState();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="MaximumContainerHeightRequest" /> bindable property.
    /// </summary>
    public static readonly BindableProperty MaximumContainerHeightRequestProperty = BindableProperty.Create(nameof(MaximumContainerHeightRequest), typeof(double), typeof(MaterialBottomSheet), defaultValue: -1.0, propertyChanged: (bindableObject, oldValue, newValue) =>
    {
        if (bindableObject is MaterialBottomSheet self)
        {
            self.SetInitialState();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Content" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(MaterialBottomSheet), defaultValue: default(View), propertyChanged: (bindableObject, oldValue, newValue) =>
    {
        if (bindableObject is MaterialBottomSheet self)
        {
            if (self._sheetViewLayout.Children.Count > 0)
                self._sheetViewLayout.Children.Clear();

            var containerContentView = (View)newValue;

            if (containerContentView.Margin != new Thickness(0))
                Logger.Debug("Avoid utilizing the Margin property within the root element of the MaterialBottomSheet's content, as its usage may result in errors or unexpected behaviors.");

            containerContentView.VerticalOptions = self.ContentVerticalOptions;

            self._sheetViewLayout.Add(containerContentView, 0, 0);
            self._sheetViewLayout.Add(self._dragHandleView, 0, 0);

            self.ApplyContainerHeight(containerContentView);
        }
    });

    /// <summary>
    /// The backing store for the <see cref="ContentVerticalOptions" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ContentVerticalOptionsProperty = BindableProperty.Create(nameof(ContentVerticalOptions), typeof(LayoutOptions), typeof(MaterialBottomSheet), defaultValue: LayoutOptions.Start);

    /// <summary>
    /// The backing store for the <see cref="ScrimColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ScrimColorProperty = BindableProperty.Create(nameof(ScrimColor), typeof(Color), typeof(MaterialBottomSheet), defaultValue: DefaultScrimColor);

    /// <summary>
    /// The backing store for the <see cref="ScrimOpacity" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ScrimOpacityProperty = BindableProperty.Create(nameof(ScrimColor), typeof(double), typeof(MaterialBottomSheet), defaultValue: 0.4);

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialBottomSheet), defaultValue: DefaultBackgroundColor);

    /// <summary>
    /// The backing store for the <see cref="CornerRadius" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(MaterialBottomSheet), defaultValue: 28.0);

    /// <summary>
    /// The backing store for the <see cref="DragHandleColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DragHandleColorProperty = BindableProperty.Create(nameof(DragHandleColor), typeof(Color), typeof(MaterialBottomSheet), defaultValue: DefaultDragHandleColor);

    /// <summary>
    /// The backing store for the <see cref="DragHandleIsVisible" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DragHandleIsVisibleProperty = BindableProperty.Create(nameof(DragHandleIsVisible), typeof(bool), typeof(MaterialBottomSheet), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="DragHandleWidth" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DragHandleWidthProperty = BindableProperty.Create(nameof(DragHandleWidth), typeof(double), typeof(MaterialBottomSheet), defaultValue: 40.0);

    /// <summary>
    /// The backing store for the <see cref="DragHandleHeight" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DragHandleHeightProperty = BindableProperty.Create(nameof(DragHandleHeight), typeof(double), typeof(MaterialBottomSheet), defaultValue: 5.0);

    /// <summary>
    /// The backing store for the <see cref="IsOpened" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IsOpenedProperty = BindableProperty.Create(nameof(IsOpened), typeof(bool), typeof(MaterialBottomSheet), defaultValue: false, defaultBindingMode: BindingMode.OneWayToSource);

    /// <summary>
    /// The backing store for the <see cref="AnimationDuration" /> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationDurationProperty = BindableProperty.Create(nameof(AnimationDuration), typeof(int), typeof(MaterialBottomSheet), defaultValue: 250);

    /// <summary>
    /// The backing store for the <see cref="DismissThreshold" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DismissThresholdProperty = BindableProperty.Create(nameof(DismissThreshold), typeof(double), typeof(MaterialBottomSheet), defaultValue: 0.4);

    /// <summary>
    /// The backing store for the <see cref="IsSwipeEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty IsSwipeEnabledProperty = BindableProperty.Create(nameof(IsSwipeEnabled), typeof(bool), typeof(MaterialBottomSheet), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="DismissWhenScrimIsTapped" /> bindable property.
    /// </summary>
    public static readonly BindableProperty DismissWhenScrimIsTappedProperty = BindableProperty.Create(nameof(DismissWhenScrimIsTapped), typeof(bool), typeof(MaterialBottomSheet), defaultValue: true);

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the container height. This is a bindable property.
    /// </summary>
    /// <default>
    /// -1.0
    /// </default>
    public double ContainerHeight
    {
        get => (double)GetValue(ContainerHeightProperty);
        set => SetValue(ContainerHeightProperty, value);
    }

    /// <summary>
    /// Gets or sets the container relative height. This is a bindable property.
    /// </summary>
    /// <default>
    /// -1.0
    /// </default>
    public double ContainerRelativeHeight
    {
        get => (double)GetValue(ContainerRelativeHeightProperty);
        set => SetValue(ContainerRelativeHeightProperty, value);
    }

    /// <summary>
    /// Gets or sets the maximum container height. This is a bindable property.
    /// </summary>
    /// <default>
    /// -1.0
    /// </default>
    public double MaximumContainerHeightRequest
    {
        get => (double)GetValue(MaximumContainerHeightRequestProperty);
        set => SetValue(MaximumContainerHeightRequestProperty, value);
    }

    /// <summary>
    /// Gets or sets the content. This is a bindable property.
    /// </summary>
    /// <default>
    /// -1.0
    /// </default>
    public new View Content
    {
        get => (View)GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    /// <summary>
    /// Gets or sets the Content Vertical Options property.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="LayoutOptions.Start"/>. LayoutOptions.Start
    /// </default>
    public LayoutOptions ContentVerticalOptions
    {
        get => (LayoutOptions)GetValue(ContentVerticalOptionsProperty);
        set => SetValue(ContentVerticalOptionsProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the control's scrim.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.Scrim">MaterialLightTheme.Scrim</see> - Dark: <see cref="MaterialDarkTheme.Scrim">MaterialDarkTheme.Scrim</see>
    /// </default>
    public Color ScrimColor
    {
        get => (Color)GetValue(ScrimColorProperty);
        set => SetValue(ScrimColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the scrim opacity. This is a bindable property.
    /// </summary>
    /// <default>
    /// 0.4
    /// </default>
    public double ScrimOpacity
    {
        get => (double)GetValue(ScrimOpacityProperty);
        set => SetValue(ScrimOpacityProperty, value);
    }

    /// <summary>
    /// Gets or sets a color that describes the background color of the card.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light: <see cref="MaterialLightTheme.SurfaceContainerLow">MaterialLightTheme.SurfaceContainerLow</see> - Dark: <see cref="MaterialDarkTheme.SurfaceContainerLow">MaterialDarkTheme.SurfaceContainerLow</see>
    /// </default>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the maximum container height. This is a bindable property.
    /// </summary>
    /// <default>
    /// 28.0
    /// </default>
    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    /// <summary>
    /// Gets or sets a drag handle color.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Light: <see cref="MaterialLightTheme.OnSurfaceVariant">MaterialLightTheme.OnSurfaceVariant</see> - Dark: <see cref="MaterialDarkTheme.OnSurfaceVariant">MaterialDarkTheme.OnSurfaceVariant</see>
    /// </default>
    public Color DragHandleColor
    {
        get => (Color)GetValue(DragHandleColorProperty);
        set => SetValue(DragHandleColorProperty, value);
    }

    /// <summary>
    /// Gets or sets if show the drag handle. This is a bindable property.
    /// </summary>
    /// <default>
    /// true
    /// </default>
    public bool DragHandleIsVisible
    {
        get => (bool)GetValue(DragHandleIsVisibleProperty);
        set => SetValue(DragHandleIsVisibleProperty, value);
    }

    /// <summary>
    /// Gets or sets the drag handle width. This is a bindable property.
    /// </summary>
    /// <default>
    /// 40.0
    /// </default>
    public double DragHandleWidth
    {
        get => (double)GetValue(DragHandleWidthProperty);
        set => SetValue(DragHandleWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the drag handle height. This is a bindable property.
    /// </summary>
    /// <default>
    /// 5.0
    /// </default>
    public double DragHandleHeight
    {
        get => (double)GetValue(DragHandleHeightProperty);
        set => SetValue(DragHandleHeightProperty, value);
    }

    /// <summary>
    /// Gets or sets if control is opened. This is a bindable property.
    /// </summary>
    /// <default>
    /// false
    /// </default>
    public bool IsOpened
    {
        get => (bool)GetValue(IsOpenedProperty);
        set => SetValue(IsOpenedProperty, value);
    }

    /// <summary>
    /// Gets or sets the anmiation's duration. This is a bindable property.
    /// </summary>
    /// <default>
    /// 250
    /// </default>
    public int AnimationDuration
    {
        get => (int)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }

    /// <summary>
    /// Gets or sets the dismiss threshold. This is a bindable property.
    /// </summary>
    /// <default>
    /// 0.4
    /// </default>
    public double DismissThreshold
    {
        get => (double)GetValue(DismissThresholdProperty);
        set => SetValue(DismissThresholdProperty, value);
    }

    /// <summary>
    /// Gets or sets if the swipe is enabled. This is a bindable property.
    /// </summary>
    /// <default>
    /// true
    /// </default>
    public bool IsSwipeEnabled
    {
        get => (bool)GetValue(IsSwipeEnabledProperty);
        set => SetValue(IsSwipeEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets if dismiss when scrim is tapped. This is a bindable property.
    /// </summary>
    /// <default>
    /// true
    /// </default>
    public bool DismissWhenScrimIsTapped
    {
        get => (bool)GetValue(DismissWhenScrimIsTappedProperty);
        set => SetValue(DismissWhenScrimIsTappedProperty, value);
    }

    #endregion Properties

    #region Events

    public event EventHandler Opened;

    public event EventHandler Closed;

    #endregion Events

    #region Constructors

    public MaterialBottomSheet()
    {
        var mainLayout = new Grid();

        _scrimBoxView = new BoxView
        {
            Color = ScrimColor,
            Opacity = 0,
            InputTransparent = true
        };

        _scrimBoxView.SetBinding(BoxView.ColorProperty, new Binding(nameof(ScrimColor), source: this));

        var scrimTapGestureRecognizer = new TapGestureRecognizer();
        scrimTapGestureRecognizer.Tapped += async (s, e) =>
        {
            if (DismissWhenScrimIsTapped)
                await Close();
        };
        _scrimBoxView.GestureRecognizers.Add(scrimTapGestureRecognizer);

        mainLayout.Children.Add(_scrimBoxView);

        _containerView = new ContentView
        {
            VerticalOptions = LayoutOptions.End,
            HeightRequest = ContainerHeightWithBottomSafeArea
        };

        var containerTapGestureRecognizer = new PanGestureRecognizer();
        containerTapGestureRecognizer.PanUpdated += Container_PanUpdated;
        _containerView.GestureRecognizers.Add(containerTapGestureRecognizer);

        _sheetView = new MaterialCard
        {
            VerticalOptions = LayoutOptions.End,
            Shadow = null,
            BackgroundColor = BackgroundColor,
            CornerRadius = new CornerRadius(CornerRadius, CornerRadius, 0, 0),
            Content = Content,
            HeightRequest = ContainerHeightWithBottomSafeArea,
            Padding = new Thickness(0)
        };

        _sheetView.SetBinding(MaterialCard.BackgroundColorProperty, new Binding(nameof(BackgroundColor), source: this));
        _sheetView.SetBinding(MaterialCard.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));

        // Remove MaterialCard effects to avoid a pan gesture lose
        _sheetView.Effects.Clear();

        _sheetViewLayout = new Grid();

        _dragHandleView = new BoxView
        {
            Color = DragHandleColor,
            CornerRadius = DragHandleHeight / 2,
            HeightRequest = DragHandleHeight,
            WidthRequest = DragHandleWidth,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(_dragHandleMargin),
            VerticalOptions = LayoutOptions.Start,
            IsVisible = DragHandleIsVisible
        };

        _dragHandleView.SetBinding(BoxView.WidthRequestProperty, new Binding(nameof(DragHandleWidth), source: this));
        _dragHandleView.SetBinding(BoxView.HeightRequestProperty, new Binding(nameof(DragHandleHeight), source: this));
        _dragHandleView.SetBinding(BoxView.ColorProperty, new Binding(nameof(DragHandleColor), source: this));
        _dragHandleView.SetBinding(BoxView.IsVisibleProperty, new Binding(nameof(DragHandleIsVisible), source: this));

        _sheetView.Content = _sheetViewLayout;

        _containerView.Content = _sheetView;

        mainLayout.Children.Add(_containerView);

        base.Content = mainLayout;
        InputTransparent = true;
    }

    #endregion Constructors

    #region Methods

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        switch (propertyName)
        {
            case nameof(DragHandleHeight):
                _dragHandleView.CornerRadius = DragHandleHeight / 2;
                break;
            case nameof(ContentVerticalOptions):
                if (Content != null)
                    Content.VerticalOptions = ContentVerticalOptions;
                break;
            default:
                base.OnPropertyChanged(propertyName);
                break;
        }
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        SetInitialState();
    }

    private void SetInitialState()
    {
        _containerView.TranslationY = _bottomSafeArea;
        _containerView.Content.TranslationY = TranslationYClosed;
        _containerView.HeightRequest = ContainerHeightWithBottomSafeArea;
        _sheetView.HeightRequest = ContainerHeightWithBottomSafeArea;
    }

    private void ApplyContainerHeight(View containerContentView)
    {
        if (ContainerRelativeHeight != (double)ContainerRelativeHeightProperty.DefaultValue
            && ContainerHeight == (double)ContainerHeightProperty.DefaultValue)
        {
            if (Height > 0)
            {
                ContainerHeight = Height * ContainerRelativeHeight;
            }
            else
            {
                SizeChanged += (s, e) =>
                {
                    var totalHeight = ((View)s).Height;
                    ContainerHeight = totalHeight * ContainerRelativeHeight;
                };
            }
        }
        else if (ContainerHeight == (double)ContainerHeightProperty.DefaultValue)
        {
            if (containerContentView.Height > 0)
            {
                if (MaximumContainerHeightRequest > 0)
                {
                    ContainerHeight = containerContentView.Height > MaximumContainerHeightRequest ?
                        MaximumContainerHeightRequest : containerContentView.Height;
                }
                else
                    ContainerHeight = containerContentView.Height;
            }
            else
            {
                containerContentView.SizeChanged += (s, e) =>
                {
                    var containerContentViewHeight = ((View)s).Height;
                    if (MaximumContainerHeightRequest > 0)
                    {
                        ContainerHeight = containerContentViewHeight > MaximumContainerHeightRequest ?
                            MaximumContainerHeightRequest : containerContentViewHeight;
                    }
                    else
                        ContainerHeight = containerContentViewHeight;
                };
            }
        }
    }

    private async void Container_PanUpdated(object sender, PanUpdatedEventArgs e)
    {
        if (!IsSwipeEnabled)
            return;

        try
        {
            if (e.StatusType == GestureStatus.Running)
            {
                _currentPosition = e.TotalY;
                if (e.TotalY > 0)
                    _containerView.Content.TranslationY = _openPosition + e.TotalY;
            }
            else if (e.StatusType == GestureStatus.Completed)
            {
                var threshold = ContainerHeightWithBottomSafeArea * DismissThreshold;
                if (_currentPosition < threshold)
                    await Open();
                else
                    await Close();
            }
        }
        catch (Exception ex)
        {
            Logger.Log(ex);
        }
    }

    public async Task Open()
    {
        try
        {
            IsVisible = true;
            await Task.WhenAll
            (
                _scrimBoxView.FadeTo(ScrimOpacity, length: (uint)AnimationDuration),
                _sheetView.TranslateTo(0, _openPosition, length: (uint)AnimationDuration, easing: Easing.SinIn)
            );

            var raiseOpened = !IsOpened;

            IsOpened = true;

            if (raiseOpened)
            {
                Opened?.Invoke(this, null);
            }

            InputTransparent = _scrimBoxView.InputTransparent = false;
        }
        catch (Exception ex)
        {
            Logger.Log(ex);
        }
    }

    public async Task Close()
    {
        try
        {
            await Task.WhenAll
            (
                _scrimBoxView.FadeTo(0, length: (uint)AnimationDuration),
                _containerView.Content.TranslateTo(x: 0, y: TranslationYClosed, length: (uint)AnimationDuration, easing: Easing.SinIn)
            );
            IsVisible = false;

            var raiseClosed = IsOpened;

            IsOpened = false;

            if (raiseClosed)
            {
                Closed?.Invoke(this, null);
            }

            InputTransparent = _scrimBoxView.InputTransparent = true;
        }
        catch (Exception ex)
        {
            Logger.Log(ex);
        }
    }

    public void SetBottomSafeArea(double bottomSafeArea)
    {
        var tabBarIsVisible = TabBarIsVisible();
        _bottomSafeArea = tabBarIsVisible ? 0 : bottomSafeArea;
        SetInitialState();
    }

    private bool TabBarIsVisible()
    {
        if (DeviceInfo.Platform == DevicePlatform.Android)
            return false;

        Element currentElement = this;

        while (!(currentElement is Page) && currentElement != null)
            currentElement = currentElement.Parent;

        if (currentElement is Page currentPage)
        {
            var currentPageIsModal = currentPage.Navigation.ModalStack.Contains(currentPage);
            if (currentPageIsModal)
            {
                foreach (var page in currentPage.Navigation.ModalStack)
                {
                    if (page is TabbedPage || page.Parent is TabbedPage
                        || (page.Parent is NavigationPage navigationPage && navigationPage.Parent is TabbedPage))
                        return true;
                }
            }
            else
            {
                foreach (var page in currentPage.Navigation.NavigationStack)
                {
                    if (page is TabbedPage || page.Parent is TabbedPage
                        || (page.Parent is NavigationPage navigationPage && navigationPage.Parent is TabbedPage))
                        return true;
                }
            }
        }

        return false;
    }

    #endregion Methods

    #region Styles

    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = ButtonCommonStates.Disabled };
        disabledState.Setters.Add(
            MaterialBottomSheet.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Surface,
                Dark = MaterialDarkTheme.Surface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        disabledState.Setters.Add(MaterialBottomSheet.ShadowProperty, null);

        //disabledState.Setters.Add(
        //    MaterialBottomSheet.BorderColorProperty,
        //    new AppThemeBindingExtension
        //    {
        //        Light = MaterialLightTheme.Surface,
        //        Dark = MaterialDarkTheme.Surface
        //    }
        //    .GetValueForCurrentTheme<Color>()
        //    .WithAlpha(0.38f));

        //disabledState.Setters.Add(MaterialBottomSheet.OpacityProperty, 0.38f);

        var pressedState = new VisualState { Name = ButtonCommonStates.Pressed };
        //pressedState.Setters.Add(MaterialBottomSheet.OpacityProperty, 1f);

        var normalState = new VisualState { Name = ButtonCommonStates.Normal };
        //normalState.Setters.Add(MaterialBottomSheet.OpacityProperty, 1f);

        commonStatesGroup.States.Add(normalState);
        commonStatesGroup.States.Add(disabledState);
        commonStatesGroup.States.Add(pressedState);

        var style = new Style(typeof(MaterialBottomSheet));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }

    #endregion Styles
}
