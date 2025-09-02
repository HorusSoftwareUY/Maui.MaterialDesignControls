using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;
using TouchEventArgs = HorusStudio.Maui.MaterialDesignControls.Behaviors.TouchEventArgs;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class CardPage : BaseContentPage<CardViewModel>
{
    private int _clickedCount = 0;
    
    public CardPage(CardViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }

    private void MaterialCard_OnTouch(object? sender, TouchEventArgs e)
    {
        lblTouchEvent.Text = $"Touch event: {e.TouchEventType}";
    }
    
    private void MaterialCard_OnClicked(object? sender, EventArgs e)
    {
        _clickedCount++;

        if (_clickedCount == 1)
            lblClickedEvent.Text = $"Clicked {_clickedCount} time";
        else
            lblClickedEvent.Text = $"Clicked {_clickedCount} times";
    }
}