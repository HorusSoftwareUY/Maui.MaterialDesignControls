using HorusStudio.Maui.MaterialDesignControls.Sample.Models;
using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

public partial class ChipsGroupView : ContentView
{
    #region Attributes & Properties

    public static readonly BindableProperty DataSourceProperty =
           BindableProperty.Create(nameof(DataSource), typeof(List<CustomizationItem>), typeof(ChipsGroupView), default(List<CustomizationItem>));

    public static readonly BindableProperty LabelTextProperty =
        BindableProperty.Create(nameof(LabelText), typeof(string), typeof(ChipsGroupView), default(string));

    public static readonly BindableProperty BindingPropertyProperty =
        BindableProperty.Create(nameof(LabelText), typeof(object), typeof(ChipsGroupView), default(object));

    public List<CustomizationItem> DataSource
    {
        get => (List<CustomizationItem>)GetValue(DataSourceProperty);
        set => SetValue(DataSourceProperty, value);
    }

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public object BindingProperty
    {
        get => (object)GetValue(BindingPropertyProperty);
        set => SetValue(BindingPropertyProperty, value);
    }

    public ICommand ItemTappedCommand { get; }

    #endregion


    public ChipsGroupView()
	{
		InitializeComponent();
        ItemTappedCommand = new Command<CustomizationItem>(OnItemTapped);
        Content.BindingContext = this;
    }

    private void OnItemTapped(CustomizationItem item)
    {
        BindingProperty = item.Value;
        DataSource.ForEach(x => x.IsSelected = item.Name == x.Name);
    }
}