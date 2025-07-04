using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class SegmentedButtonsPage : BaseContentPage<SegmentedButtonsViewModel>
    {
        public SegmentedButtonsPage(SegmentedButtonsViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        void MaterialSegmentedButtons_SelectionChanged(System.Object sender, HorusStudio.Maui.MaterialDesignControls.SegmentedButtonSelectedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                lblSelectedItems.Text = $"SelectedItem: {e.SelectedItem.Text}";
            }
            else
            {
                lblSelectedItems.Text = $"SelectedItem: -";
            }
        }
    }
}