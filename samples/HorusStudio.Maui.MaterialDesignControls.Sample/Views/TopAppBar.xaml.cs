using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

public partial class TopAppBar : ContentView
{
    #region Attributes & Properties

    public static readonly BindableProperty HasrightIconsProperty =
           BindableProperty.Create(nameof(HasrightIcons), typeof(bool), typeof(TopAppBar), default(bool));

    public static readonly BindableProperty TitleProperty =
       BindableProperty.Create(nameof(Title), typeof(string), typeof(TopAppBar), default(string));

    public static readonly BindableProperty BackCommandProperty =
       BindableProperty.Create(nameof(BackCommand), typeof(ICommand), typeof(TopAppBar), default(ICommand));

    public bool HasrightIcons
    {
        get => (bool)GetValue(HasrightIconsProperty);
        set => SetValue(HasrightIconsProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public ICommand BackCommand
    {
        get => (ICommand)GetValue(BackCommandProperty);
        set => SetValue(BackCommandProperty, value);
    }

    public Command<string> ChangeTabCommand { get; }

    #endregion

    public TopAppBar()
	{
		InitializeComponent();
        Content.BindingContext = this;
    }
}