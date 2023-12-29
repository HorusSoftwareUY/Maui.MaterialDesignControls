namespace HorusStudio.Maui.MaterialDesignControls
{
    public static class MaterialDesignControlsBuilder
    {
        public static MauiAppBuilder ConfigureMaterialDesignControls(this MauiAppBuilder builder)
        {
            builder.ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler(typeof(CustomButton), typeof(Handlers.CustomButtonHandler));
            });

            return builder;
        }
    }
}