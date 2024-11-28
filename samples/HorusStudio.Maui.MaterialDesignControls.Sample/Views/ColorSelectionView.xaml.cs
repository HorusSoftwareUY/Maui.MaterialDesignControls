using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

public partial class ColorSelectionView : ContentView
{
    #region Attributes & Properties

    public static readonly BindableProperty DataSourceProperty =
           BindableProperty.Create(nameof(DataSource), typeof(List<CustomizationColor>), typeof(ColorSelectionView), default(List<CustomizationItem>));

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(ColorSelectionView), default(string));

    public static readonly BindableProperty BindingPropertyProperty =
        BindableProperty.Create(nameof(LabelText), typeof(Color), typeof(ColorSelectionView), default(Color));

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

    public ICommand ItemTappedCommand { get; }

    #endregion

    public ColorSelectionView()
    {
        InitializeComponent();
        ItemTappedCommand = new Command<CustomizationColor>(OnItemTapped);
        Content.BindingContext = this;
    }

    private void OnItemTapped(CustomizationColor item)
    {
        BindingProperty = item.Color;
        DataSource.ForEach(x => x.IsSelected = item.Color == x.Color);
    }
}