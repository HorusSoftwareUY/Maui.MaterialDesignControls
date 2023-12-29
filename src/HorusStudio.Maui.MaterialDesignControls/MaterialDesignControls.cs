namespace HorusStudio.Maui.MaterialDesignControls;

public static class MaterialDesignControls
{
    public static void Init(Application application)
    {
        RegisterDefaultStyles(application);
    }

    private static void RegisterDefaultStyles(Application application)
    {
        // Button
        application.Resources.AddStyles(MaterialButton.GetStyles());
        // Icon Button
        application.Resources.AddStyles(MaterialIconButton.GetStyles());
    }

    private static void AddStyles(this ResourceDictionary resources, IEnumerable<Style> styles)
    {
        if (styles == null) return;

        foreach (var style in styles)
        {
            resources.Add(style);
        }
    }
}