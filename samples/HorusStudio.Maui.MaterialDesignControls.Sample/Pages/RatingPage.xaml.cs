using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class RatingPage : BaseContentPage<RatingViewModel>
    {
        public RatingPage(RatingViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        void MaterialRating_ValueChanged(System.Object sender, HorusStudio.Maui.MaterialDesignControls.MaterialRatingSelectedEventArgs e)
        {
            if (ratingWithEvent != null)
                ratingWithEvent.Label = $"ValueChanged event - Value: {e.NewValue}";
        }
    }
}