using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Wrapper for items defined within a <see cref="MaterialNavigationDrawer"/>.
/// </summary>
public class MaterialNavigationDrawerItem : INotifyPropertyChanged
{
    private string _text;
    public string Text
    {
        get => _text;
        set => SetProperty(ref _text, value);
    }

    private string _badgeText;
    public string BadgeText
    {
        get => _badgeText;
        set => SetProperty(ref _badgeText, value);
    }

    private string _section;
    public string Section
    {
        get => _section;
        set => SetProperty(ref _section, value);
    }

    private string _selectedLeadingIcon;
    public string SelectedLeadingIcon
    {
        get => _selectedLeadingIcon;
        set => SetProperty(ref _selectedLeadingIcon, value);
    }

    private string _unselectedLeadingIcon;
    public string UnselectedLeadingIcon
    {
        get => _unselectedLeadingIcon;
        set => SetProperty(ref _unselectedLeadingIcon, value);
    }

    private string _selectedTrailingIcon;
    public string SelectedTrailingIcon
    {
        get => _selectedTrailingIcon;
        set => SetProperty(ref _selectedTrailingIcon, value);
    }

    private string _unselectedTrailingIcon;
    public string UnselectedTrailingIcon
    {
        get => _unselectedTrailingIcon;
        set => SetProperty(ref _unselectedTrailingIcon, value);
    }

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    private bool _showActiveIndicator = true;
    public bool ShowActiveIndicator
    {
        get => _showActiveIndicator;
        set => SetProperty(ref _showActiveIndicator, value);
    }

    private bool _isEnabled = true;
    public bool IsEnabled
    {
        get => _isEnabled;
        set => SetProperty(ref _isEnabled, value);
    }

    /*
    internal bool UnselectedLeadingIconIsVisible => !string.IsNullOrEmpty(UnselectedLeadingIcon); 

    internal bool SelectedLeadingIconIsVisible => !string.IsNullOrEmpty(SelectedLeadingIcon); 

    internal bool UnselectedTrailingIconIsVisible => !string.IsNullOrEmpty(UnselectedTrailingIcon); 

    internal bool SelectedTrailingIconIsVisible => !string.IsNullOrEmpty(SelectedTrailingIcon);
    */ 

    public override bool Equals(object obj)
    {
        if (obj is not MaterialNavigationDrawerItem toCompare)
            return false;
        
        var key = this.Section + "-" + this.Text;
        var keyToCompare = toCompare.Section + "-" + toCompare.Text;
        return key.Equals(keyToCompare, System.StringComparison.InvariantCultureIgnoreCase);
    }

    public override string ToString() =>
        string.IsNullOrWhiteSpace(Text) ? "No defined text" : Text;

    public event PropertyChangedEventHandler PropertyChanged;

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