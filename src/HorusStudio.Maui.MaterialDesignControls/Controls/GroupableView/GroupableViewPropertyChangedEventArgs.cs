namespace HorusStudio.Maui.MaterialDesignControls;

public class GroupableViewPropertyChangedEventArgs : EventArgs
{
    public string PropertyName { get; private set; }
    public object OldValue { get; private set; }
    public object NewValue { get; private set; }

    public GroupableViewPropertyChangedEventArgs(string propertyName, object oldValue, object newValue)
    {
        PropertyName = propertyName;
        OldValue = oldValue;
        NewValue = newValue;
    }
}