using System.ComponentModel;

namespace HorusStudio.Maui.MaterialDesignControls;

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
    public new static readonly BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(MaterialBottomSheet), defaultValue: null);
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialBottomSheet), defaultValue: null);
    public static readonly BindableProperty DetentsProperty = BindableProperty.Create(nameof(Detents), typeof(IList<Detent>), typeof(MaterialBottomSheet),
        defaultValueCreator: bindable =>
        {
            return new List<Detent>();
        });
    public static readonly BindableProperty HasBackdropProperty = BindableProperty.Create(nameof(HasBackdrop), typeof(bool), typeof(MaterialBottomSheet), false);
    public static readonly BindableProperty HasHandleProperty = BindableProperty.Create(nameof(HasHandle), typeof(bool), typeof(MaterialBottomSheet), false);
    public static readonly BindableProperty HandleColorProperty = BindableProperty.Create(nameof(HandleColor), typeof(Color), typeof(MaterialBottomSheet), null);
    public static readonly BindableProperty IsCancelableProperty = BindableProperty.Create(nameof(IsCancelable), typeof(bool), typeof(MaterialBottomSheet), true);
    public static readonly BindableProperty SelectedDetentProperty = BindableProperty.Create(nameof(SelectedDetent), typeof(Detent), typeof(MaterialBottomSheet), null, BindingMode.TwoWay);
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(double), typeof(MaterialBottomSheet), -1d);

    //public event EventHandler<float> Sliding;
    public event EventHandler<DismissOrigin>? Dismissed;
    public event EventHandler? Showing;
    public event EventHandler? Shown;

    DismissOrigin _dismissOrigin = DismissOrigin.Gesture;

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

    public IList<Detent> Detents
    {
        get => (IList<Detent>)GetValue(DetentsProperty);
        set => SetValue(DetentsProperty, value);
    }

    public bool HasBackdrop
    {
        get => (bool)GetValue(HasBackdropProperty);
        set => SetValue(HasBackdropProperty, value);
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

    public MaterialBottomSheet() : base()
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

    internal void NotifyShowing()
    {
        Showing?.Invoke(this, EventArgs.Empty);
    }
    internal void NotifyShown()
    {
        Shown?.Invoke(this, EventArgs.Empty);
    }
}
