using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class CheckboxPage :  BaseContentPage<CheckboxViewModel>
{
    public CheckboxPage(CheckboxViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }

    private void MaterialCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        DisplayAlert("CheckedChanged event", $"Value: {e.Value.ToString()}", "OK");
    }
}