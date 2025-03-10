# IMaterialSnackbar

Abstraction to handle Material Snackbar component that follows [Material Design Guidelines](https://m3.material.io/components/snackbar/overview).

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
