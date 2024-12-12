using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

public partial class ThemeSelectionView : ContentView
{
    #region Attributes & Properties

    public static readonly BindableProperty DataSourceProperty =
           BindableProperty.Create(nameof(DataSource), typeof(List<CustomizationColor>), typeof(ThemeSelectionView), default(List<CustomizationItem>));

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(ThemeSelectionView), default(string));

    public static readonly BindableProperty BindingPropertyProperty =
        BindableProperty.Create(nameof(LabelText), typeof(Color), typeof(ThemeSelectionView), default(Color));

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ThemeSelectionView), default(ICommand));

    public List<CustomizationColor> DataSource
    {
        get => (List<CustomizationColor>)GetValue(DataSourceProperty);
        set => SetValue(DataSourceProperty, value);
    }

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public object BindingProperty
    {
        get => (Color)GetValue(BindingPropertyProperty);
        set => SetValue(BindingPropertyProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public ICommand ItemTappedCommand { get; }

    #endregion

    public ThemeSelectionView()
    {
        InitializeComponent();
        ItemTappedCommand = new Command<CustomizationColor>(OnItemTapped);
        Content.BindingContext = this;
    }

    private void OnItemTapped(CustomizationColor item)
    {
        BindingProperty = item.Color;
        DataSource.ForEach(x => x.IsSelected = item.Color == x.Color);

        if (Command?.CanExecute(null) == true)
        {
            Command.Execute(item.TextColor);
        }
    }
}