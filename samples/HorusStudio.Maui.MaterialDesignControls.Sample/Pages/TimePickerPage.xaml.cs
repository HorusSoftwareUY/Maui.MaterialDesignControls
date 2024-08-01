using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class TimePickerPage : BaseContentPage<TimePickerViewModel>
{
    public TimePickerPage(TimePickerViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }

    private void TimePicker_Focused(object sender, FocusEventArgs e)
    {
        //Labelfocused.Text = e.IsFocused ? "Focused" : "Unfocused";
    }
}