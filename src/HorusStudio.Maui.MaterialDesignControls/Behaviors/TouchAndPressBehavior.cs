namespace HorusStudio.Maui.MaterialDesignControls.Behaviors
{
	public partial class TouchAndPressBehavior : PlatformBehavior<View>
    {
        private Frame _buttonFrame;

        public TouchAndPressBehavior(Frame buttonFrame)
        {
            _buttonFrame = buttonFrame;
        }
    }
}