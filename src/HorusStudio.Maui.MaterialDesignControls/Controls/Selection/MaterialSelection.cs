﻿using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A selection <see cref="View" /> Selection controls allow the user to select options.
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialSelection.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialSelection
///         Command="{Binding TapCommand}"
///         CommandParameter="User selection"
///         Label="User"
///         LeadingIconSource="ic_floating.png"
///         Placeholder="Select user"
///         Text="{Binding SelectedText}" /&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var selection = new MaterialSelection
/// {
///         Command = TapCommand,
///         CommandParameter = "User selection",
///         Label = "User",
///         LeadingIconSource = "ic_floating.png",
///         Placeholder = "Select user",
///         Text = SelectedText
/// };
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SelectionPage.xaml)
/// 
/// </example>
public class MaterialSelection : MaterialInputBase
{
    #region Layout

    private readonly MaterialLabel _label;

    #endregion Layout

    #region Constructor

    public MaterialSelection()
    {
        _label = new MaterialLabel
        {
            HorizontalOptions = LayoutOptions.Fill,
            HeightRequest = -1.0
        };

        _label.SetBinding(MaterialLabel.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        _label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(TextColor), source: this));
        _label.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Text), source: this));
        _label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));

        InputTapCommand = new Command(() => {
            IsFocused = false;
            if (IsEnabled && (Command?.CanExecute(CommandParameter) ?? false))
            {
                Command.Execute(CommandParameter);
            }
        });

        Content = _label;
    }

    #endregion Constructor

    #region Bindable Properties

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

    #endregion Bindable Properties

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
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    #endregion Properties

    #region Methods
    
    protected override void SetControlIsEnabled() {}
    
    protected override void OnControlAppearing() {}
    
    protected override void OnControlDisappearing() {}

    protected override void SetControlTemplate(MaterialInputType type)
    {
#if ANDROID
        var hOffset = 4;
        var vOffset = 2;
        switch (type)
        {
            case MaterialInputType.Filled:
                _label.Margin = new Thickness(hOffset, 0, 0, vOffset);
                break;
            case MaterialInputType.Outlined:
                _label.Margin = new Thickness(hOffset, 0, 0, 0);
                break;
        }
#endif
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
