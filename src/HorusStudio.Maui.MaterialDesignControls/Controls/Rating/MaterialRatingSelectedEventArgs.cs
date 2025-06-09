namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Event argument raised when when the value changed on the MaterialRating control.
    /// </summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class MaterialRatingSelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the new value selected.
        /// </summary>
        public int NewValue { get; }

		public MaterialRatingSelectedEventArgs(int newValue)
		{
            NewValue = newValue;
        }
	}
}