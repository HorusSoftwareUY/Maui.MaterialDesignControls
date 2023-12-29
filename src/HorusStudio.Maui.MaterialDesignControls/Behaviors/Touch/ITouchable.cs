namespace HorusStudio.Maui.MaterialDesignControls.Behaviors
{
	public interface ITouchable
	{
        void OnTouch(TouchType gestureType);
        bool IsEnabled { get; set; }
        AnimationTypes Animation { get; set; }
        ICustomAnimation CustomAnimation { get; set; }
        double? AnimationParameter { get; set; }
    }

    public enum TouchType
    {
        Pressed,
        Released,
        Cancelled,
        Ignored
    }
}