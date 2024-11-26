using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

public partial class ConnectionItemView : ContentView
{
    public static readonly BindableProperty IconSourceProperty =
           BindableProperty.Create(nameof(IconSource), typeof(string), typeof(ConnectionItemView), default(string));

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(ConnectionItemView), default(string));

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ConnectionItemView), default(ICommand));

    public static readonly BindableProperty CommandParameterProperty =
      BindableProperty.Create(nameof(CommandParameter), typeof(string), typeof(ConnectionItemView), default(string));

    public string IconSource
    {
        get => (string)GetValue(IconSourceProperty);
        set => SetValue(IconSourceProperty, value);
    }

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
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

    public ConnectionItemView()
    {
        InitializeComponent();
        Content.BindingContext = this;
    }
}