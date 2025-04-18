namespace HorusStudio.Maui.MaterialDesignControls;

[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public class TimeSelectedEventArgs : EventArgs
{
    public TimeSpan? OldValue { get; private set; }
    public TimeSpan? NewValue { get; private set; }

    public TimeSelectedEventArgs(TimeSpan? oldValue, TimeSpan? newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }
}