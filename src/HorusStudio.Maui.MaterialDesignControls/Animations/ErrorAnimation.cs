namespace HorusStudio.Maui.MaterialDesignControls.Animations
{
	internal static class ErrorAnimation
	{
        internal static async Task AnimateAsync(View view, ErrorAnimationTypes errorAnimationType)
        {
            switch (errorAnimationType)
            {
                case ErrorAnimationTypes.Shake:
                    await ShakeAnimation.BeginAnimation(view, duration: 1500);
                    break;
                case ErrorAnimationTypes.Breath:
                    await BreathAnimation.BeginAnimation(view, duration: 1200);
                    break;
                case ErrorAnimationTypes.Jump:
                    await JumpAnimation.BeginAnimation(view, duration: 500);
                    break;
            }
        }
    }
}