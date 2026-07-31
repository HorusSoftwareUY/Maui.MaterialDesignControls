using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;
using HorusStudio.Maui.MaterialDesignControls.Sample.Views;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class MainPage
{
    private bool _lazyCardsLoadStarted;

    public MainPage(MainViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();

#if USE_MONO
        RuntimeLabel.Text = "Running with Mono";
        RuntimeLabel.TextColor = Color.FromArgb("#29B6F6");
#endif
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        // Defer the inflation of cards 6+ until after the first frame.
        // The first 5 cards are eager and give the page enough scrollable content;
        // the rest are materialized here in small batches so the UI thread stays responsive.
        if (_lazyCardsLoadStarted)
            return;

        _lazyCardsLoadStarted = true;

        var lazyCards = CardsContainer.Children.OfType<LazyContentView>().ToList();
        if (lazyCards.Count == 0)
            return;

        Dispatcher.Dispatch(async () =>
        {
            // Let the first frame settle (and the nav-timing/startup markers fire) before inflating.
            await Task.Delay(150);

            const int batchSize = 4;
            for (var i = 0; i < lazyCards.Count; i += batchSize)
            {
                foreach (var lazyCard in lazyCards.Skip(i).Take(batchSize))
                {
                    lazyCard.Load();
                }

                // Yield a frame between batches to avoid a post-first-frame jank spike.
                await Task.Delay(50);
            }
        });
    }
}
