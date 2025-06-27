using System.ComponentModel;
using System.Runtime.CompilerServices;
using HorusStudio.Maui.MaterialDesignControls.Utils;

namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Represents the model for a segmented button to be rendered inside the MaterialSegmentedButton control.
    /// </summary>
    public class MaterialSegmentedButtonItem : INotifyPropertyChanged
    {
        #region Attributes

        private string? _text;
        private ImageSource? _selectedIcon;
        private ImageSource? _unselectedIcon;
        private bool _applyIconTintColor = true;
        private bool _isSelected = false;

        #endregion Attributes

        #region Properties

        /// <summary>
        /// Gets or sets the text displayed as the content of the button.
        /// </summary>
        public string? Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        /// <summary>
        /// Gets or sets selected icon in segmented button.
        /// </summary>
        /// <default>
        /// <see langword="Null"/>
        /// </default>
        public ImageSource? SelectedIcon
        {
            get => _selectedIcon;
            set => SetProperty(ref _selectedIcon, value);
        }

        /// <summary>
        /// Gets or sets unselected icon in segmented button.
        /// </summary>
        /// <default>
        /// <see langword="Null"/>
        /// </default>
        public ImageSource? UnselectedIcon
        {
            get => _unselectedIcon;
            set => SetProperty(ref _unselectedIcon, value);
        }

        /// <summary>
        /// Gets or sets if the icon applies the tint color.
        /// </summary>
        /// <default>
        /// <see langword="True"/>
        /// </default>
        public bool ApplyIconTintColor
        {
            get => _applyIconTintColor;
            set => SetProperty(ref _applyIconTintColor, value);
        }

        /// <summary>
        /// Gets if segmented button is selected.
        /// </summary>
        /// <default>
        /// <see langword="False"/>
        /// </default>
        public bool IsSelected
        {
            get => _isSelected;
            internal set => SetProperty(ref _isSelected, value);
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion Properties

        public MaterialSegmentedButtonItem(string text)
        {
            Text = text;

            if (string.IsNullOrEmpty(Text))
                Logger.Debug($"The {nameof(MaterialSegmentedButtonItem)} has no assigned text. It is recommended to set a non-null and non-empty value.");
        }

        public override bool Equals(object? obj)
        {
            if (obj is not MaterialSegmentedButtonItem toCompare)
                return false;

            var key = Text != null ? Text : string.Empty;
            var keyToCompare = toCompare.Text;
            return key.Equals(keyToCompare, System.StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Text);
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
}