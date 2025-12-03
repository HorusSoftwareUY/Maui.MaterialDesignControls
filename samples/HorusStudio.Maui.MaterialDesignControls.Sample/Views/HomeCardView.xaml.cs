using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

public partial class HomeCardView : ContentView
{
    #region Attributes & Properties

    public static readonly BindableProperty ImageSourceProperty =
      BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(HomeCardView), default(string));

    public static readonly BindableProperty ImageAspectProperty =
           BindableProperty.Create(nameof(ImageAspect), typeof(Aspect), typeof(HomeCardView), default(Aspect));

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(HomeCardView), default(string));

    public static readonly BindableProperty DescriptionProperty =
        BindableProperty.Create(nameof(Description), typeof(string), typeof(HomeCardView), default(string));
    
    public static readonly BindableProperty NavigationParameterProperty =
        BindableProperty.Create(nameof(NavigationParameter), typeof(string), typeof(HomeCardView), default(string));

    public string ImageSource
    {
        get => (string)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public Aspect ImageAspect
    {
        get => (Aspect)GetValue(ImageAspectProperty);
        set => SetValue(ImageAspectProperty, value);
    }

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
    
    public string NavigationParameter
    {
        get => (string)GetValue(NavigationParameterProperty);
        set => SetValue(NavigationParameterProperty, value);
    }

    #endregion

    public HomeCardView()
	{
		InitializeComponent();
        Content.BindingContext = this;
    }

    protected override void OnBindingContextChanged()
    {
        var mainViewModel = BindingContext as ViewModels.MainViewModel;
        if (mainViewModel != null)
        {
            card.Command = mainViewModel.CardClickCommand;
        }
    }
}