namespace HorusStudio.Maui.MaterialDesignControls
{
    public static class ShakeAnimation
    {
        public static void Animate(View view)
        {
            Task.Run(async () =>
            {
                const uint timeout = 50;
                await view.TranslateTo(-15, 0, timeout);
                await view.TranslateTo(15, 0, timeout);
                await view.TranslateTo(-10, 0, timeout);
                await view.TranslateTo(10, 0, timeout);
                await view.TranslateTo(-5, 0, timeout);
                await view.TranslateTo(5, 0, timeout);
                view.TranslationX = 0;
            });
        }
    }
}