namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

/// <summary>
/// ContentView that defers the inflation of its content until <see cref="Load"/> is called.
/// The content is declared as a <see cref="DataTemplate"/>, which MAUI does not instantiate
/// at XAML parse time, keeping it out of the first-frame cost.
/// </summary>
public class LazyContentView : ContentView
{
    public static readonly BindableProperty DeferredContentProperty =
        BindableProperty.Create(nameof(DeferredContent), typeof(DataTemplate), typeof(LazyContentView), default(DataTemplate));

    public DataTemplate DeferredContent
    {
        get => (DataTemplate)GetValue(DeferredContentProperty);
        set => SetValue(DeferredContentProperty, value);
    }

    public bool HasLoaded { get; private set; }

    public void Load()
    {
        if (HasLoaded || DeferredContent is null)
            return;

        HasLoaded = true;

        if (DeferredContent.CreateContent() is View view)
        {
            Content = view;
        }
    }
}
