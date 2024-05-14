using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class RadioButtonPage : BaseContentPage<RadioButtonViewModel>
{
    public RadioButtonPage(RadioButtonViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }

    private void MaterialRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Console.WriteLine("Hola");
    }
}