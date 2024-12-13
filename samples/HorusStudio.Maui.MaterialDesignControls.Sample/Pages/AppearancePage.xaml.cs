using HorusStudio.Maui.MaterialDesignControls.Behaviors;
using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class AppearancePage : BaseContentPage<AppearanceViewModel>
{
    private AppearanceViewModel viewModel;
    public AppearancePage(AppearanceViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();

        this.viewModel = viewModel;

        var lightIconTintColor = new IconTintColorBehavior();
        lightIconTintColor.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(viewModel.LightFillColor), source: this.viewModel));

        var darkIconTintColor = new IconTintColorBehavior();
        darkIconTintColor.SetBinding(IconTintColorBehavior.TintColorProperty, new Binding(nameof(viewModel.DarkFillColor), source: this.viewModel));

        LightImage.Behaviors.Add(lightIconTintColor);
        DarkImage.Behaviors.Add(darkIconTintColor);
    }
}