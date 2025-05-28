namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Event argument raised when when the selection of one of the segmented buttons changes.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class SegmentedButtonsSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the currently collection of selected items when <see cref="AllowMultiSelect"/> is <see langword="True"/>.
        /// </summary>
        public IEnumerable<MaterialSegmentedButtonsItem>? SelectedItems { get; private set; }

        /// <summary>
        /// Gets the currently selected item when <see cref="AllowMultiSelect"/> is <see langword="False"/>.
        /// </summary>
        public MaterialSegmentedButtonsItem? SelectedItem { get; private set; }

        public SegmentedButtonsSelectedEventArgs(IEnumerable<MaterialSegmentedButtonsItem> selectedItems)
        {
            SelectedItems = selectedItems;
        }

        public SegmentedButtonsSelectedEventArgs(MaterialSegmentedButtonsItem selectedItem)
        {
            SelectedItem = selectedItem;
        }
    }
}