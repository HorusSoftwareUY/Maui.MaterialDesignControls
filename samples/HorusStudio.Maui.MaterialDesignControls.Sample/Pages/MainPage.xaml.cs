using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;
using Microsoft.Maui.Layouts;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Pages
{
    public partial class MainPage : BaseContentPage<MainViewModel>
    {
        public MainPage(MainViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }

        private void AddContent()
        {
            var mainLayout = this.Content as VerticalStackLayout;

            var flexLayout = new FlexLayout
            {
                Wrap = FlexWrap.Wrap,
                Direction = FlexDirection.Row,
                JustifyContent = FlexJustify.Start,
                BackgroundColor = Colors.AliceBlue
            };

            var border = new Border
            {
                Background = Colors.LightGreen,
                Margin = 2,
                Content = new Label { Text = "border", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }
            };

            var card = new MaterialCard
            {
                Margin = 2,
                Background = Colors.LightGreen,
                Content = new Label { Text = "card", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }
            };

            var chips = new MaterialChips
            {
                Background = Colors.LightGreen,
                Margin = 2,
                Content = new Label { Text = "chips", HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center }
            };

            flexLayout.Children.Add(border);
            flexLayout.Children.Add(card);
            flexLayout.Children.Add(chips);

            FlexLayout.SetBasis(border, new FlexBasis(1, true));
            FlexLayout.SetBasis(card, new FlexBasis(1, true));
            FlexLayout.SetBasis(chips, new FlexBasis(1, true));

            mainLayout.Children.Add(flexLayout);
        }
    }
}