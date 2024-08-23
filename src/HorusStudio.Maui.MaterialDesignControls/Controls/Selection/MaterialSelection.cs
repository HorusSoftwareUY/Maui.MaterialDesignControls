
using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

public class MaterialSelection : MaterialInputBase
{
    #region Attributes

    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.OnSurface, Dark = MaterialLightTheme.OnSurface }.GetValueForCurrentTheme<Color>();

    #endregion Attributes

    #region Layout

    private MaterialLabel _lbl;

    #endregion Layout

    #region Constructor

    public MaterialSelection()
    {
        _lbl = new MaterialLabel
        {
            HorizontalOptions = LayoutOptions.FillAndExpand,
            HeightRequest = -1.0
        };

        _lbl.SetBinding(MaterialLabel.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        _lbl.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _lbl.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Text), source: this));
        _lbl.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _lbl.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));

        InputTapCommand = new Command(() => {
            IsFocused = false;
            if (IsEnabled && (Command?.CanExecute(CommandParameter) ?? false))
            {
                Command.Execute(CommandParameter);
            }
        });

        Content = _lbl;
    }

    #endregion Constructor

    #region BindableProperties

    /// <summary>
    /// The backing store for the <see cref="Text" /> bindable property.
    /// </summary>
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MaterialSelection), defaultBindingMode: BindingMode.TwoWay);

    /// <summary>
    /// The backing store for the <see cref="Command" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialSelection), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="CommandParameter" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialSelection), defaultValue: null);

    #endregion BindableProperties

    #region Properties

    /// <summary>
    /// Gets or sets the text displayed as the content of the input.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// null
    /// </default>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }


    /// <summary>
    /// Gets or sets selection command. This is a bindable property.
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the command parameter. This is a bindable property.
    /// </summary>
    public object CommandParameter
    {
        get => (object)GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    #endregion Properties

    #region Methods

    protected override void SetControlIsEnabled()
    {
        if (_lbl != null)
            _lbl.IsEnabled = IsEnabled;
    }

    protected override void OnControlAppearing()
    {
        // Setup events/animations
        _lbl.Focused += ContentFocusChanged;
        _lbl.Unfocused += ContentFocusChanged;
    }

    protected override void OnControlDisappearing()
    {
        // Cleanup events/animations
        _lbl.Focused -= ContentFocusChanged;
        _lbl.Unfocused -= ContentFocusChanged;
    }

    private void ContentFocusChanged(object sender, FocusEventArgs e)
    {
        IsFocused = e.IsFocused;
        VisualStateManager.GoToState(this, GetCurrentVisualState());
        UpdateLayoutAfterTypeChanged(Type);
    }

    #endregion Methods

    #region Styles
    internal static IEnumerable<Style> GetStyles()
    {
        var style = new Style(typeof(MaterialSelection)) { ApplyToDerivedTypes = true };

        var baseStyles = MaterialInputBase.GetBaseStyles();

        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, baseStyles);

        return new List<Style> { style };
    }

    #endregion Styles
}
