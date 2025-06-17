namespace HorusStudio.Maui.MaterialDesignControls.Animations
{
	internal static class ErrorAnimationManager
	{
        internal static async Task AnimateAsync(IValidableView validableView)
        {
            if (validableView == null)
            {
                return;
            }

            if (validableView.ErrorAnimation != null)
            {
                await validableView.ErrorAnimation.BeginAnimation(validableView);
            }
            else if (validableView.ErrorAnimationType != ErrorAnimationTypes.None)
            {
                switch (validableView.ErrorAnimationType)
                {
                    case ErrorAnimationTypes.Shake:
                        var shakeAnimation = new ShakeAnimation() { Duration = 1500 };
                        await shakeAnimation.BeginAnimation(validableView);
                        break;
                    case ErrorAnimationTypes.Heart:
                        var heartAnimation = new HeartAnimation() { Duration = 1200 };
                        await heartAnimation.BeginAnimation(validableView);
                        break;
                    case ErrorAnimationTypes.Jump:
                        var jumpAnimation = new JumpAnimation() { Duration = 1200 };
                        await jumpAnimation.BeginAnimation(validableView);
                        break;
                }
            }
        }
    }
}