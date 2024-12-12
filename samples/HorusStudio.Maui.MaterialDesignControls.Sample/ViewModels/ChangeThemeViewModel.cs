using HorusStudio.Maui.MaterialDesignControls.Sample.Helpers;
using HorusStudio.Maui.MaterialDesignControls.Sample.Pages;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class ChangeThemeViewModel : BaseViewModel, IQueryAttributable
    {
        #region Attributes & Properties

        [ObservableProperty]
        Color _indicatorColor;

        public override string Title => string.Empty;

        #endregion

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("color", out var colorValue) && colorValue is string color)
            {
                IndicatorColor = ColorHelper.GetColorByKey($"{color}Primary");
            }

            await Task.Delay(500);
            await Shell.Current.Navigation.PushAsync(new MainPage(new MainViewModel()), false);
        }
    }
}
