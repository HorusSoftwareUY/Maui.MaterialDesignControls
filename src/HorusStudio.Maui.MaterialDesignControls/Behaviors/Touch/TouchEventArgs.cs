namespace HorusStudio.Maui.MaterialDesignControls.Behaviors;

[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public class TouchEventArgs : EventArgs
{
    public TouchEventType TouchEventType { get; private set; }

    public TouchEventArgs(TouchEventType touchEventType)
    {
        TouchEventType = touchEventType;
    }
}