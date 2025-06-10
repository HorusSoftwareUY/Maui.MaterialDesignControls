using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class ChipsPage : BaseContentPage<ChipsViewModel>
    {
        public ChipsPage(ChipsViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        void MaterialChipsGroup_SelectionChanged(System.Object sender, HorusStudio.Maui.MaterialDesignControls.MaterialChipsGroupSelectionEventArgs e)
        {
            if (e.SelectedItem != null && e.SelectedItem is string text)
            {
                eventChipsGroup.LabelText = $"ValueChanged event - {text}";
            }
            else
            {
                eventChipsGroup.LabelText = $"ValueChanged event";
            }
        }
    }
}