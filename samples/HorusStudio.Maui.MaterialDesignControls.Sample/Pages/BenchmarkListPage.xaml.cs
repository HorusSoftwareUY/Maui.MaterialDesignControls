using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages;

public partial class BenchmarkListPage : BaseContentPage<BenchmarkListViewModel>
{
    private bool _richRows = true;

    public BenchmarkListPage(BenchmarkListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        ApplyRowTemplate();
    }

    private BenchmarkListViewModel Vm => BindingContext as BenchmarkListViewModel;

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Arrive with data: run the benchmark once, so the page is never shown
        // as an empty list. Re-entering the page keeps whatever was loaded.
        if (Vm is { IsLoading: false, Items.Count: 0 } vm)
            vm.LoadItemsCommand.Execute(null);
    }

    private void OnToggleRowsClicked(object sender, EventArgs e)
    {
        _richRows = !_richRows;
        ApplyRowTemplate();
    }

    private void ApplyRowTemplate()
    {
        var key = _richRows ? "RichRowTemplate" : "SimpleRowTemplate";
        if (Resources.TryGetValue(key, out var template) && template is DataTemplate dataTemplate)
            CollectionViewRef.ItemTemplate = dataTemplate;

        RowsToggleButton.Text = _richRows ? "Rows: rich" : "Rows: simple";
    }
}
