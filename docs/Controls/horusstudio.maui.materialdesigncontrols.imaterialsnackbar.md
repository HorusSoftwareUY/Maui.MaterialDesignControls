# IMaterialSnackbar

Abstraction to handle Snackbars that follow Material Design Guidelines. [See more](https://m3.material.io/components/snackbar/overview).

Namespace: HorusStudio.Maui.MaterialDesignControls

<br>

### C# sample

```csharp
private readonly IMaterialSnackbar _snackbar;

public SnackbarViewModel(IMaterialSnackbar snackbar)
{
    _snackbar = snackbar;
    Subtitle = "Snackbars show short updates about app processes at the bottom of the screen";
}

private async void DefaultSnackbar()
{
    _snackbar.Show(new MaterialSnackbarConfig("Default snackbar with custom action")
    {
        Action = new MaterialSnackbarConfig.ActionConfig("Close action", SnackbarAction)
    });
}
```

[See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SnackbarPage.xaml)

## Known issues and pending features

* [macOS] MaterialSnackbar with icons are not shown due to an icon rendering issue
