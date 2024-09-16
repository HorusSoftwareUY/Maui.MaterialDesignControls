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
            await this.FindByName<MaterialBottomSheet>(controlName)?.Open();
        }

        private async Task CloseBottomSheet(string controlName)
        {
            await this.FindByName<MaterialBottomSheet>(controlName)?.Close();
        }

        void materialBottomSheet4_Opened(System.Object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Opened!!!!!");
        }

        void materialBottomSheet4_Closed(System.Object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Closed!!!!!");
        }
    }
}
