using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

public partial class AboutCardView : ContentView
{
    public static readonly BindableProperty FirstIconSourceProperty =
           BindableProperty.Create(nameof(FirstIconSource), typeof(string), typeof(ConnectionItemView), default(string));

    public static readonly BindableProperty SecondIconSourceProperty =
           BindableProperty.Create(nameof(SecondIconSource), typeof(string), typeof(ConnectionItemView), default(string));

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(ConnectionItemView), default(string));

    public static readonly BindableProperty DescriptionProperty =
        BindableProperty.Create(nameof(Description), typeof(string), typeof(ConnectionItemView), default(string));    

    public static readonly BindableProperty TextButtonProperty =
       BindableProperty.Create(nameof(TextButton), typeof(string), typeof(ConnectionItemView), default(string));

    public static readonly BindableProperty TypeButtonProperty =
       BindableProperty.Create(nameof(TypeButton), typeof(MaterialButtonType), typeof(ConnectionItemView), default(MaterialButtonType));

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ConnectionItemView), default(ICommand));

    public static readonly BindableProperty CommandParameterProperty =
      BindableProperty.Create(nameof(CommandParameter), typeof(string), typeof(ConnectionItemView), default(string));

    public string FirstIconSource
    {
        get => (string)GetValue(FirstIconSourceProperty);
        set => SetValue(FirstIconSourceProperty, value);
    }

    public string SecondIconSource
    {
        get => (string)GetValue(SecondIconSourceProperty);
        set => SetValue(SecondIconSourceProperty, value);
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

    public string TextButton
    {
        get => (string)GetValue(TextButtonProperty);
        set => SetValue(TextButtonProperty, value);
    }

    public MaterialButtonType TypeButton
    {
        get => (MaterialButtonType)GetValue(TypeButtonProperty);
        set => SetValue(TypeButtonProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public string CommandParameter
    {
        get => (string)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public AboutCardView()
	{
		InitializeComponent();
        Content.BindingContext = this;
    }
}