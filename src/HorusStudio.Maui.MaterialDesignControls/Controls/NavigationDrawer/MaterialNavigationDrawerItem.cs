using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Wrapper for items defined within a <see cref="MaterialNavigationDrawer">material navigation drawer</see>.
/// </summary>
public class MaterialNavigationDrawerItem : INotifyPropertyChanged
{
    #region Attributes
    
    private string _text = null!;
    private string? _badgeText;
    private string? _headline;
    private ImageSource? _selectedLeadingIcon;
    private ImageSource? _leadingIcon;
    private bool _applyLeadingIconTintColor = true;
    private ImageSource? _selectedTrailingIcon;
    private ImageSource? _trailingIcon;
    private bool _applyTrailingIconTintColor = true;
    private bool _isSelected;
    private bool _isEnabled = true;
    private string _automationId = null!;
    
    #endregion Attributes
    
    #region Properties
    
    /// <summary>
    /// Gets or sets text to be displayed for item.
    /// </summary>
    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    /// <summary>
    /// Gets or set text for badge, if present. Optional.
    /// </summary>
    public string? BadgeText
    {
        get => _badgeText;
        set => SetProperty(ref _badgeText, value);
    }

    /// <summary>
    /// Gets or sets headline text for item. Optional.
    /// </summary>
    public string? Headline
    {
        get => _headline;
        set => SetProperty(ref _headline, value);
    }

    /// <summary>
    /// Gets or sets an icon source for leading icon when item is selected. Optional.
    /// </summary>
    public ImageSource? SelectedLeadingIcon
    {
        get => _selectedLeadingIcon;
        set => SetProperty(ref _selectedLeadingIcon, value);
    }
    
    /// <summary>
    /// Gets or sets an icon source for leading icon. Optional.
    /// </summary>
    public ImageSource? LeadingIcon
    {
        get => _leadingIcon;
        set => SetProperty(ref _leadingIcon, value);
    }

    /// <summary>
    /// Gets or sets if the leading icon applies the tint color. The default value is True.
    /// </summary>
    public bool ApplyLeadingIconTintColor
    {
        get => _applyLeadingIconTintColor;
        set => SetProperty(ref _applyLeadingIconTintColor, value);
    }

    /// <summary>
    /// Gets or sets an icon source for trailing icon when item is selected. Optional.
    /// </summary>
    public ImageSource? SelectedTrailingIcon
    {
        get => _selectedTrailingIcon;
        set => SetProperty(ref _selectedTrailingIcon, value);
    }
    
    /// <summary>
    /// Gets or sets an icon source for trailing icon. Optional.
    /// </summary>
    public ImageSource? TrailingIcon
    {
        get => _trailingIcon;
        set => SetProperty(ref _trailingIcon, value);
    }

    /// <summary>
    /// Gets or sets if the trailing icon applies the tint color. The default value is True.
    /// </summary>
    public bool ApplyTrailingIconTintColor
    {
        get => _applyTrailingIconTintColor;
        set => SetProperty(ref _applyTrailingIconTintColor, value);
    }

    /// <summary>
    /// Gets or sets if item is selected or not.
    /// </summary>
    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }
    
    /// <summary>
    /// Gets or sets if item is enabled or not.
    /// </summary>
    public bool IsEnabled
    {
        get => _isEnabled;
        set => SetProperty(ref _isEnabled, value);
    }
    
    /// <summary>
    /// Gets or sets a value that allows the automation framework to find and interact with this element.
    /// </summary>
    /// <remarks>
    /// This value may only be set once on an element.
    /// 
    /// When set on this control, the <see cref="AutomationId">AutomationId</see> is also used as a base identifier for its internal elements:
    /// - The text label uses the identifier "{AutomationId}_Text".
    /// - The leading icon uses the identifier "{AutomationId}_LeadingIcon".
    /// - The trailing icon uses the identifier "{AutomationId}_TrailingIcon".
    /// - The badge label uses the identifier "{AutomationId}_Badge".
    /// - The headline label uses the identifier "{AutomationId}_Headline".
    /// - The headline section layout uses the identifier "{AutomationId}_Section".
    /// - The item layout uses the identifier "{AutomationId}_Item".
    /// 
    /// This convention allows automated tests and accessibility tools to consistently locate all subelements of the control.
    /// </remarks>
    public string AutomationId
    {
        get => _automationId;
        set => SetProperty(ref _automationId, value);
    }
    
    /// <inheritdoc />
    public event PropertyChangedEventHandler? PropertyChanged;
    
    #endregion Properties
    
    public override bool Equals(object? obj)
    {
        if (obj is not MaterialNavigationDrawerItem toCompare)
            return false;
        
        var key = this.Headline + "-" + this.Text;
        var keyToCompare = toCompare.Headline + "-" + toCompare.Text;
        return key.Equals(keyToCompare, System.StringComparison.InvariantCultureIgnoreCase);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Headline, Text);
    }

    public override string ToString() =>
        string.IsNullOrWhiteSpace(Text) ? "No defined text" : Text;

    private void SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return;

        backingStore = value;
        OnPropertyChanged(propertyName);
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}