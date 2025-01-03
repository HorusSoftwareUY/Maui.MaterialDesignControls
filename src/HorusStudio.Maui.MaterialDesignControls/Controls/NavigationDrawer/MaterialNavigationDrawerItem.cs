using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// This class is a defined classs that is used to show the material navigation drawer items.
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

    public string Section { get; set; }

    public string SelectedLeadingIcon { get; set; }

    public string UnselectedLeadingIcon { get; set; }

    public string SelectedTrailingIcon { get; set; }

    public string UnselectedTrailingIcon { get; set; }

    public bool IsSelected { get; set; }

    public bool ShowActiveIndicator { get; set; } = true;

    public bool IsEnabled { get; set; } = true;

    internal bool UnselectedLeadingIconIsVisible
    {
        get { return !string.IsNullOrEmpty(UnselectedLeadingIcon); }
    }

    internal bool SelectedLeadingIconIsVisible
    {
        get { return !string.IsNullOrEmpty(SelectedLeadingIcon); }
    }

    internal bool UnselectedTrailingIconIsVisible
    {
        get { return !string.IsNullOrEmpty(UnselectedTrailingIcon); }
    }

    internal bool SelectedTrailingIconIsVisible
    {
        get { return !string.IsNullOrEmpty(SelectedTrailingIcon); }
    }


    public override bool Equals(object obj)
    {
        if (obj is not MaterialNavigationDrawerItem toCompare)
            return false;
        else
        {
            var key = this.Section + "-" + this.Text;
            var keyToCompare = toCompare.Section + "-" + toCompare.Text;
            return key.Equals(keyToCompare, System.StringComparison.InvariantCultureIgnoreCase);
        }
    }

    public override string ToString() =>
        string.IsNullOrWhiteSpace(Text) ? "No defined text" : Text;

    public event PropertyChangedEventHandler PropertyChanged;

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
