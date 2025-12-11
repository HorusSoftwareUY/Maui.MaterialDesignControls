namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

public partial class HomeCardView : ContentView
{
    #region Attributes & Properties

    public static readonly BindableProperty ImageSourceProperty =
      BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(HomeCardView), default(string), propertyChanged: (bindable, _, newValue) =>
      {
          if (bindable is HomeCardView self && newValue is string source)
          {
              self.previewImage.Source = source;
          }
      });

    public static readonly BindableProperty ImageAspectProperty =
           BindableProperty.Create(nameof(ImageAspect), typeof(Aspect), typeof(HomeCardView), default(Aspect), propertyChanged: (bindable, _, newValue) =>
           {
               if (bindable is HomeCardView self && newValue is Aspect aspect)
               {
                   self.previewImage.Aspect = aspect;
               }
           });

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(HomeCardView), default(string), propertyChanged: (bindable, _, newValue) =>
        {
            if (bindable is HomeCardView self && newValue is string title)
            {
                self.lblTitle.Text = title;
            }
        });

    public static readonly BindableProperty DescriptionProperty =
        BindableProperty.Create(nameof(Description), typeof(string), typeof(HomeCardView), default(string), propertyChanged: (bindable, _, newValue) =>
        {
            if (bindable is HomeCardView self && newValue is string description)
            {
                self.lblDescription.Text = description;
            }
        });
    
    public static readonly BindableProperty NavigationParameterProperty =
        BindableProperty.Create(nameof(NavigationParameter), typeof(string), typeof(HomeCardView), default(string), propertyChanged: (bindable, _, newValue) =>
        {
            if (bindable is HomeCardView self && newValue is string navigationParameter)
            {
                self.card.CommandParameter = navigationParameter;
            }
        });
    
    public static readonly BindableProperty PreviewContentProperty = BindableProperty.Create(nameof(PreviewContent), typeof(View), typeof(HomeCardView), propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is HomeCardView self && newValue is View view)
        {
            self.previewImage.IsVisible = false;
            self.previewContent.Content = view;
            self.previewContent.IsVisible = true;
        }
    });
    
    public static readonly BindableProperty PreviewLayoutOptionsProperty = BindableProperty.Create(nameof(PreviewLayoutOptions), typeof(LayoutOptions), typeof(HomeCardView), propertyChanged: (bindable, _, newValue) =>
    {
        if (bindable is HomeCardView self && newValue is LayoutOptions layoutOptions)
        {
            self.previewContent.HorizontalOptions = layoutOptions;
            self.previewContent.VerticalOptions = layoutOptions;
        }
    });

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
    
    public View? PreviewContent
    {
        get => (View?)GetValue(PreviewContentProperty);
        set => SetValue(PreviewContentProperty, value);
    }
    
    public LayoutOptions PreviewLayoutOptions
    {
        get => (LayoutOptions)GetValue(PreviewLayoutOptionsProperty);
        set => SetValue(PreviewLayoutOptionsProperty, value);
    }

    #endregion

    public HomeCardView()
	{
		InitializeComponent();
    }
}