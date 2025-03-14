using System.ComponentModel;

namespace HorusStudio.Maui.MaterialDesignControls;

public enum MaterialBottomSheetType
{
    Standard,
    Modal
}

public enum DismissOrigin
{
    Gesture,
    Programmatic,
}

/// <summary>
/// From The49.Maui.BottomSheet
/// </summary>
public partial class MaterialBottomSheet : ContentView
{
    #region Attributes
    
    private const MaterialBottomSheetType DefaultType = MaterialBottomSheetType.Standard;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultBackgroundColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.SurfaceContainerLow, Dark = MaterialDarkTheme.SurfaceContainerLow }.GetValueForCurrentTheme<Color>();
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultHandleColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurfaceVariant, Dark = MaterialDarkTheme.OnSurfaceVariant }.GetValueForCurrentTheme<Color>();
    private const float DefaultHandleOpacity = 0.4f;
    private const double DefaultCornerRadius = 28;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultScrimColor = _ => new AppThemeBindingExtension { Light = MaterialLightTheme.Scrim, Dark = MaterialDarkTheme.Scrim }.GetValueForCurrentTheme<Color>();
    private const float DefaultScrimOpacity = 0.5f;
    private static readonly IList<Detent> DefaultDetents = [];
    private DismissOrigin _dismissOrigin = DismissOrigin.Gesture;
    private const bool DefaultHasHandle = true;
    private const bool DefaultIsCancelable = true;
    private bool _isShown = false;
    
    #endregion Attributes
    
    #region Bindable Properties
    
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialBottomSheetType), typeof(MaterialBottomSheet), DefaultType);
    public new static readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(MaterialBottomSheet), defaultValue: null);
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialBottomSheet), defaultValueCreator: DefaultBackgroundColor);
    public static readonly BindableProperty DetentsProperty = BindableProperty.Create(nameof(Detents), typeof(IList<Detent>), typeof(MaterialBottomSheet),defaultValue: DefaultDetents);
    public static readonly BindableProperty ScrimColorProperty = BindableProperty.Create(nameof(ScrimColor), typeof(Color), typeof(MaterialBottomSheet), defaultValueCreator: DefaultScrimColor);
    public static readonly BindableProperty ScrimOpacityProperty = BindableProperty.Create(nameof(ScrimOpacity), typeof(float), typeof(MaterialBottomSheet), defaultValue: DefaultScrimOpacity);
    public static readonly BindableProperty HasHandleProperty = BindableProperty.Create(nameof(HasHandle), typeof(bool), typeof(MaterialBottomSheet), DefaultHasHandle);
    public static readonly BindableProperty HandleColorProperty = BindableProperty.Create(nameof(HandleColor), typeof(Color), typeof(MaterialBottomSheet), defaultValueCreator: DefaultHandleColor);
    public static readonly BindableProperty HandleOpacityProperty = BindableProperty.Create(nameof(HandleOpacity), typeof(float), typeof(MaterialBottomSheet), defaultValue: DefaultHandleOpacity);
    public static readonly BindableProperty IsCancelableProperty = BindableProperty.Create(nameof(IsCancelable), typeof(bool), typeof(MaterialBottomSheet), DefaultIsCancelable);
    public static readonly BindableProperty SelectedDetentProperty = BindableProperty.Create(nameof(SelectedDetent), typeof(Detent), typeof(MaterialBottomSheet), null, BindingMode.TwoWay);
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(MaterialBottomSheet), DefaultCornerRadius);

    #endregion Bindable Properties
    
    #region Properties

    public MaterialBottomSheetType Type
    {
        get => (MaterialBottomSheetType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }
    
    [TypeConverter(typeof(BrushTypeConverter))]
    public new Brush Background
    {
        get => (Brush)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }
    
    internal Brush? BackgroundBrush
    {
        get
        {
            if (Background?.IsEmpty == false)
            {
                return Background;
            }
            if (BackgroundColor.IsNotDefault())
            {
                return new SolidColorBrush(BackgroundColor);
            }
            return null;
        }
    }
    
    public IList<Detent> Detents
    {
        get => (IList<Detent>)GetValue(DetentsProperty);
        set => SetValue(DetentsProperty, value);
    }

    public Color? ScrimColor
    {
        get => (Color?)GetValue(ScrimColorProperty);
        set => SetValue(ScrimColorProperty, value);
    }
    
    public float? ScrimOpacity
    {
        get => (float?)GetValue(ScrimOpacityProperty);
        set => SetValue(ScrimOpacityProperty, value);
    }
    
    public bool HasHandle
    {
        get => (bool)GetValue(HasHandleProperty);
        set => SetValue(HasHandleProperty, value);
    }

    public Color? HandleColor
    {
        get => (Color?)GetValue(HandleColorProperty);
        set => SetValue(HandleColorProperty, value);
    }
    
    public float? HandleOpacity
    {
        get => (float?)GetValue(HandleOpacityProperty);
        set => SetValue(HandleOpacityProperty, value);
    }

    public bool IsCancelable
    {
        get => (bool)GetValue(IsCancelableProperty);
        set => SetValue(IsCancelableProperty, value);
    }

    public Detent? SelectedDetent
    {
        get => (Detent?)GetValue(SelectedDetentProperty);
        set => SetValue(SelectedDetentProperty, value);
    }

    public double CornerRadius
    {
        get => (double)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    #endregion Properties
    
    #region Events
    
    public event EventHandler<DismissOrigin>? Dismissed;
    public event EventHandler? Showing;
    public event EventHandler? Shown;
    
    #endregion Events
    
    public MaterialBottomSheet()
    {
        Resources.Add(new Style(typeof(Label)));
    }

    public Task ShowAsync(bool animated = true)
    {
        var window = Application.Current?.Windows[0];
        if (window is null)
        {
            return Task.CompletedTask;
        }
        return ShowAsync(window, animated);
    }

    public Task ShowAsync(Window window, bool animated = true)
    {
        if (_isShown) return Task.CompletedTask;
        
        var completionSource = new TaskCompletionSource();
        void OnShown(object? sender, EventArgs e)
        {
            Shown -= OnShown;
            completionSource.SetResult();
        }
        Shown += OnShown;

        if (SelectedDetent is null)
        {
            SelectedDetent = GetDefaultDetent();
        }
        window.AddLogicalChild(this);
        BottomSheetManager.Show(window, this, animated);
        return completionSource.Task;
    }

    public Task DismissAsync(bool animated = true)
    {
        _dismissOrigin = DismissOrigin.Programmatic;
        var completionSource = new TaskCompletionSource();
        void OnDismissed(object? sender, DismissOrigin origin)
        {
            Dismissed -= OnDismissed;
            completionSource.SetResult();
        }
        Dismissed += OnDismissed;
        Handler?.Invoke(nameof(DismissAsync), animated);
        return completionSource.Task;
    }

    internal IEnumerable<Detent> GetEnabledDetents()
    {
        var enabledDetents = Detents.Where(d => d.IsEnabled).ToList();
        return enabledDetents.Count == 0 ? [ new ContentDetent() ] : enabledDetents;
    }

    internal Detent? GetDefaultDetent()
    {
        if (SelectedDetent is not null) return SelectedDetent;
        
        var detents = GetEnabledDetents();
        return detents.FirstOrDefault(d => d.IsDefault);
    }

    internal void NotifyDismissed()
    {
        Parent?.RemoveLogicalChild(this);
        Dismissed?.Invoke(this, _dismissOrigin);
        _isShown = false;
    }

    internal void NotifyShowing()
    {
        Showing?.Invoke(this, EventArgs.Empty);
    }
    internal void NotifyShown()
    {
        Shown?.Invoke(this, EventArgs.Empty);
        _isShown = true;
    }
}
