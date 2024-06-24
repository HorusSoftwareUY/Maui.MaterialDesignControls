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
        //I addded this try/catch because for some reason when update the value, minimum and maximum, the page is not loaded yet and it can throws exceptions.
        try
        {
			displayLabel.Text = String.Format("The Slider value is {0}", e.NewValue);
		}
		catch
		{
		}
    }
}