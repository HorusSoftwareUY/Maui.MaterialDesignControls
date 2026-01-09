using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

public partial class SampleBPageViewModel : BaseViewModel
{
    private readonly IMaterialSnackbar _materialSnackbar;
    
    public override string Title => Models.Pages.SampleB;

    [ObservableProperty]
    private string? _email;

    [ObservableProperty]
    private string? _emailSupportingText;

    [ObservableProperty]
    private string? _password;

    public string AppVersion => $"v{AppInfo.Current.VersionString}";

    public SampleBPageViewModel(IMaterialSnackbar materialSnackbar)
    {
        _materialSnackbar = materialSnackbar;
    }

    [ICommand]
    private async Task Login()
    {
        var email = Email?.Trim();
        const string emailPattern = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";

        if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, emailPattern, RegexOptions.CultureInvariant))
        {
            EmailSupportingText = "The email entered doesn't have a valid format.";
            return;
        }

        EmailSupportingText = null;

        await Task.Delay(TimeSpan.FromSeconds(2));

        _ = _materialSnackbar.ShowAsync(new MaterialSnackbarConfig($"Email: {Email}")
        {
            LeadingIcon = new MaterialSnackbarConfig.IconConfig("ic_button.png")
        });
    }
    
    [ICommand]
    private async Task RecoverPassword()
    {
        await _materialSnackbar.ShowAsync(new MaterialSnackbarConfig("Recovery flow not implemented (sample).")
        {
            LeadingIcon = new MaterialSnackbarConfig.IconConfig("info.png")
        });
    }

    [ICommand]
    private async Task SocialLogin(string provider)
    {
        await _materialSnackbar.ShowAsync(new MaterialSnackbarConfig($"Social login, provider: {provider}")
        {
            LeadingIcon = new MaterialSnackbarConfig.IconConfig("info.png")
        });
    }
}
