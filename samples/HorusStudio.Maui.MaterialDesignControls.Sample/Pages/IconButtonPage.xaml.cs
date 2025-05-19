using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class IconButtonPage : BaseContentPage<IconButtonViewModel>
    {
        private int _clickedCount = 0;
        private int _pressedCount = 0;
        private int _releasedCount = 0;
        
        public IconButtonPage(IconButtonViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
        
        private void OnCounterClicked(object sender, EventArgs e)
        {
            _clickedCount++;
            var msg = $"Clicked {_clickedCount} time{(_clickedCount == 1 ? string.Empty : "s")}";
            System.Diagnostics.Debug.WriteLine(msg);
            DisplayAlert("Events", msg, "OK");
        }
        
        private void OnCounterPressed(object sender, EventArgs e)
        {
            _pressedCount++;
            var msg = $"Pressed {_pressedCount} time{(_pressedCount == 1 ? string.Empty : "s")}";
            System.Diagnostics.Debug.WriteLine(msg);
            DisplayAlert("Events", msg, "OK");
        }
        
        private void OnCounterReleased(object sender, EventArgs e)
        {
            _releasedCount++;
            var msg = $"Released {_releasedCount} time{(_releasedCount == 1 ? string.Empty : "s")}";
            System.Diagnostics.Debug.WriteLine(msg);
            DisplayAlert("Events", msg, "OK");
        }
    }
}