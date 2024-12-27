using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class SegmentedPage : BaseContentPage<SegmentedViewModel>
    {
        public SegmentedPage(SegmentedViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        private void MaterialSegmented_OnIsSelectedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("test");
        }
    }
}