using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class DatePickerPage : BaseContentPage<DatePickerViewModel>
    {
        public DatePickerPage(DatePickerViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        private void DatePicker_Focused(object sender, FocusEventArgs e)
        {
            //Labelfocused.Text = e.IsFocused ? "Focused" : "Unfocused";
        }
    }
}