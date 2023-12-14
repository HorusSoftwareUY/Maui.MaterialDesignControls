namespace HorusStudio.Maui.MaterialDesignControls.Behaviors
{
	public partial class TouchAndPressBehavior : PlatformBehavior<View>
    {
        private readonly Frame _buttonFrame;

        public TouchAndPressBehavior(): this(null)
        {
        }

        public TouchAndPressBehavior(Frame buttonFrame)
        {
            _buttonFrame = buttonFrame;
        }
    }
}