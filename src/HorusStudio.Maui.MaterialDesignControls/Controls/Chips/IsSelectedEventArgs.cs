namespace HorusStudio.Maui.MaterialDesignControls;

[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public class IsSelectedEventArgs : EventArgs
{
    public bool IsSelected { get; private set; }

    public IsSelectedEventArgs(bool isSelected)
    {
        IsSelected = isSelected;
    }
}