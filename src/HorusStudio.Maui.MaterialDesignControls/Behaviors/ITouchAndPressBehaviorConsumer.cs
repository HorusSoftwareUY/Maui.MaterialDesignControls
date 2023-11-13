﻿namespace HorusStudio.Maui.MaterialDesignControls.Behaviors
{
	public interface ITouchAndPressBehaviorConsumer
	{
        void ConsumeEvent(EventType gestureType);
        bool IsEnabled { get; set; }
        AnimationTypes Animation { get; set; }
        ICustomAnimation CustomAnimation { get; set; }
        double? AnimationParameter { get; set; }
        void ExecuteAction();
    }

    public enum EventType
    {
        Pressing,
        Released,
        Cancelled,
        Ignored
    }
}