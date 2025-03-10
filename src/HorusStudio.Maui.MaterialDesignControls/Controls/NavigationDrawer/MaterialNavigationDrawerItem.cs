﻿using System.ComponentModel;
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

    private string _headline;
    public string Headline
    {
        get => _headline;
        set => SetProperty(ref _headline, value);
    }

    private string _selectedLeadingIcon;
    public string SelectedLeadingIcon
    {
        get => _selectedLeadingIcon;
        set => SetProperty(ref _selectedLeadingIcon, value);
    }

    private string _leadingIcon;
    public string LeadingIcon
    {
        get => _leadingIcon;
        set => SetProperty(ref _leadingIcon, value);
    }

    private string _selectedTrailingIcon;
    public string SelectedTrailingIcon
    {
        get => _selectedTrailingIcon;
        set => SetProperty(ref _selectedTrailingIcon, value);
    }

    private string _trailingIcon;
    public string TrailingIcon
    {
        get => _trailingIcon;
        set => SetProperty(ref _trailingIcon, value);
    }

    private bool _isSelected;
    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }
    
    private bool _isEnabled = true;
    public bool IsEnabled
    {
        get => _isEnabled;
        set => SetProperty(ref _isEnabled, value);
    }
    
    public override bool Equals(object obj)
    {
        if (obj is not MaterialNavigationDrawerItem toCompare)
            return false;
        
        var key = this.Headline + "-" + this.Text;
        var keyToCompare = toCompare.Headline + "-" + toCompare.Text;
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