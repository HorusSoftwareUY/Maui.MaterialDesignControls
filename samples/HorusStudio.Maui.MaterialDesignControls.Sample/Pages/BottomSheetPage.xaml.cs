using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class BottomSheetPage : BaseContentPage<BottomSheetViewModel>
    {
        public BottomSheetPage(BottomSheetViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();

            viewModel.OpenBottomSheetControl = async (controlName) => await OpenBottomSheet(controlName);
            viewModel.CloseBottomSheetControl = async (controlName) => await CloseBottomSheet(controlName);
        }
        
        private async Task OpenBottomSheet(string controlName)
        {
            var control = this.FindByName(controlName);
            switch (control)
            {
                case MaterialBottomSheet materialSheet:
                    materialSheet.BindingContext = BindingContext;
                    await materialSheet.ShowAsync();
                    break;
            }
        }

        private async Task CloseBottomSheet(string controlName)
        {
            var control = this.FindByName(controlName);
            switch (control)
            {
                case MaterialBottomSheet materialSheet:
                    await materialSheet.DismissAsync();
                    break;
            }
        }

        private void CustomSheet_OnShown(object? sender, EventArgs e)
        {
            DisplayAlert("Custom Sheet", "Custom Sheet is shown.", "OK");
        }

        private void CustomSheet_OnDismissed(object? sender, DismissOrigin e)
        {
            DisplayAlert("Custom Sheet", $"Custom Sheet is dismissed from {e.ToString()}.", "OK");
        }
    }
}
