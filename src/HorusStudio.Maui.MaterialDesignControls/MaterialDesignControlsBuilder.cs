namespace HorusStudio.Maui.MaterialDesignControls
{
    public static class MaterialDesignControlsBuilder
    {
        public static void Configure(MauiAppBuilder builder)
        {
            builder.ConfigureEffects(effects =>
            {
#if ANDROID
                effects.Add<TouchReleaseEffect, TouchReleasePlatformEffect>();
#endif
            });
        }
    }
}