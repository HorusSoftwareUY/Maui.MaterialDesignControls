using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class SliderPage : BaseContentPage<SliderViewModel>
{
    public SliderPage(SliderViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }

    private void MaterialSlider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        displayLabel.Text = String.Format("The Slider value is {0}", e.NewValue);
    }
}