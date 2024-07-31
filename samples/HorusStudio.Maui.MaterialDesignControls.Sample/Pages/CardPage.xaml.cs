using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class CardPage : BaseContentPage<CardViewModel>
{
    public CardPage(CardViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}