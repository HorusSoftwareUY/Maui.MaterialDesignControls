using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;
public partial class CheckboxViewModel : BaseViewModel
{
    #region Attributes & Properties

    public override string Title => Models.Pages.Checkbox;

    [ObservableProperty]
    private bool _isCustomize;

    [ObservableProperty]
    private bool _isCheckboxEnabled;

    [ObservableProperty]
    private bool _value;

    #endregion

    public CheckboxViewModel()
    {
        Subtitle = "Checkboxes let users select one or more items from a list, or turn an item on or off";
    }

    [ICommand]
    private async Task CheckedChanged(object message)
    {
        await DisplayAlert(Title + " from Command", message.ToString(), "OK");
    }

    [ICommand]
    private async Task CheckValue()
    {
        await DisplayAlert("Checkbox", $"Value = {Value}", "OK");
    }
}
