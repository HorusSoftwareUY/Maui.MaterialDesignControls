using System.Collections.Frozen;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Typography helps make writing legible and beautiful <see href="https://m3.material.io/styles/typography/type-scale-tokens">see here.</see>
/// </summary>
public static class MaterialFontFamily
{
    internal static FrozenSet<MaterialFont>? Fonts { get; private set; }
    
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    public static string Default { get; set; } = null;

    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    public static string Regular { get; set; } = null;

    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    public static string Medium { get; set; } = null;

    internal static void Configure(IFontCollection fonts, MaterialFontOptions options)
    {
        Fonts = fonts.Select(f => new MaterialFont(f.Filename, f.Alias)).ToFrozenSet();
        Default = options.Default;
        Regular = options.Regular;
        Medium = options.Medium;
    }
}