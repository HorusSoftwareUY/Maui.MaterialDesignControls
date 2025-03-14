using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;
using HorusStudio.Maui.MaterialDesignControls.Sample.Views;

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
            //if (!Resources.TryGetValue(controlName, out var control)) return;
            var control = this.FindByName(controlName);
            switch (control)
            {
                case MaterialBottomSheet2 legacySheet:
                    await legacySheet.Open();
                    break;
                case MaterialBottomSheet materialSheet:
                    await materialSheet.ShowAsync();
                    break;
            }
        }

        private async Task CloseBottomSheet(string controlName)
        {
            var control = this.FindByName(controlName);
            switch (control)
            {
                case MaterialBottomSheet2 legacySheet:
                    await legacySheet.Close();
                    break;
                case MaterialBottomSheet materialSheet:
                    await materialSheet.DismissAsync();
                    break;
            }
        }

        void materialBottomSheet4_Opened(System.Object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Opened!!!!!");
        }

        void materialBottomSheet4_Closed(System.Object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Closed!!!!!");
        }

        private async void MaterialButton_OnClicked(object? sender, EventArgs e)
        {
            var sheet = new MaterialBottomSheet
            {
                Content = BottomSheetContent,
                BackgroundColor = Colors.WhiteSmoke,
                CornerRadius = 16,
                HasHandle = true,
                Detents = [
                    new FullscreenDetent(),
                    new ContentDetent { IsDefault = true }
                ]
            };
            await sheet.ShowAsync(Window);
        }
    }
}
