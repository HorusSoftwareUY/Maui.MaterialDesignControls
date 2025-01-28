namespace HorusStudio.Maui.MaterialDesignControls.Sample.Views;

public partial class ControlsTabsView : ContentView
{
    #region Attributes & Properties

    public static readonly BindableProperty IsCustomizeProperty =
           BindableProperty.Create(nameof(IsCustomize), typeof(bool), typeof(ControlsTabsView), default(bool));

    public bool IsCustomize
    {
        get => (bool)GetValue(IsCustomizeProperty);
        set => SetValue(IsCustomizeProperty, value);
    }

    public Command<string> ChangeTabCommand { get; }

    #endregion

    public ControlsTabsView()
	{
		InitializeComponent();
        ChangeTabCommand = new Command<string>(ExecuteChangeTabCommand);
        Content.BindingContext = this;
    }

    private void ExecuteChangeTabCommand(string from)
    {
        var isFromCustomize = from == "Customize";

        if (isFromCustomize == IsCustomize) return;

        IsCustomize = isFromCustomize;
    }
}