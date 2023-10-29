using Microsoft.Maui.Controls;

namespace HorusStudio.Maui.MaterialDesignControls
{
    public static class MaterialDesignControlsBuilder
    {
        public static void Configure(MauiAppBuilder builder)
        {
            builder.ConfigureEffects(effects =>
            {
#if ANDROID
                effects.Add<TouchAndPressEffect, TouchAndPressPlatformEffect>();
                effects.Add<TouchReleaseEffect, TouchReleasePlatformEffect>();
#elif IOS
                effects.Add<TouchAndPressEffect, TouchAndPressPlatformEffect>();
#endif
            });
        }
    }
}