using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class PickerPage : BaseContentPage<PickerViewModel>
    {
        public PickerPage(PickerViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        private void MaterialPicker_Focused(object sender, FocusEventArgs e)
        {
            Labelfocused.Text = e.IsFocused ? "Focused" : "Unfocused";
        }

        private void MaterialPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelSelectedIndex.Text = $"Selected Index: {picker.SelectedIndex}";
        }
    }
}