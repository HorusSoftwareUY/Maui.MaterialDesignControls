using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class ButtonPage : BaseContentPage<ButtonViewModel>
    {
        private int _clickedCount = 0;
        private int _pressedCount = 0;
        private int _releasedCount = 0;
        
        public ButtonPage(ButtonViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
        
        private void OnCounterClicked(object sender, EventArgs e)
        {
            _clickedCount++;

            if (_clickedCount == 1)
                ClickBtn.Text = $"Clicked {_clickedCount} time";
            else
                ClickBtn.Text = $"Clicked {_clickedCount} times";
            
            System.Diagnostics.Debug.WriteLine(ClickBtn.Text);
        }
        
        private void OnCounterPressed(object sender, EventArgs e)
        {
            _pressedCount++;

            if (_pressedCount == 1)
                PressBtn.Text = $"Pressed {_pressedCount} time";
            else
                PressBtn.Text = $"Pressed {_pressedCount} times";
            
            System.Diagnostics.Debug.WriteLine(PressBtn.Text);
        }
        
        private void OnCounterReleased(object sender, EventArgs e)
        {
            _releasedCount++;

            if (_releasedCount == 1)
                ReleaseBtn.Text = $"Released {_releasedCount} time";
            else
                ReleaseBtn.Text = $"Released {_releasedCount} times";
            
            System.Diagnostics.Debug.WriteLine(ReleaseBtn.Text);
        }

        void MaterialButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}