namespace HorusStudio.Maui.MaterialDesignControls
{
    public static class MaterialDesignControlsBuilder
    {
        public static MauiAppBuilder ConfigureMaterialDesignControls(this MauiAppBuilder builder)
        {
            builder.ConfigureEffects(effects =>
            {
#if ANDROID
                effects.Add<TouchReleaseEffect, TouchReleasePlatformEffect>();
#endif
            });

            return builder;
        }
    }
}