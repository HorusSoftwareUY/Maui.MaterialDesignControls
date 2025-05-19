namespace HorusStudio.Maui.MaterialDesignControls;

[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public class DateSelectedEventArgs : EventArgs
{
    public DateTime? OldValue { get; private set; }
    public DateTime? NewValue { get; private set; }

    public DateSelectedEventArgs(DateTime? oldValue, DateTime? newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }
}