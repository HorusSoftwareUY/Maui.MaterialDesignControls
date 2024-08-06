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
        /// <summary> Pressed </summary>
        Pressed,
        /// <summary> Released </summary>
        Released,
        /// <summary> Cancelled </summary>
        Cancelled,
        /// <summary> Ignored </summary>
        Ignored
    }
}