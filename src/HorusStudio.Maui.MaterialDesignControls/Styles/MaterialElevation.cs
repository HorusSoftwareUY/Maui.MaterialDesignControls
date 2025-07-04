using System;
namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Elevation is the distance between two surfaces on the z-axis <see href="https://m3.material.io/styles/elevation/overview">see here.</see>
    /// </summary>
	public static class MaterialElevation
    {
        /// <default>
        /// No shadow
        /// </default>
        public static Shadow Level0 { get; set; } = null!;

        /// <default>
        /// <br> Brush = Material.Shadow </br>
        /// <br> Radius = Android: 5 / MacOS,Windows,iOS: 1.5f </br>
        /// <br> Opacity = Android: 0.3f / MacOS,Windows,iOS: 0.35f </br>
        /// <br> Offset = Android: new Point(-0.5, 2) / MacOS,Windows,iOS: new Point(0, 1.5) </br>
        /// </default>
        public static Shadow Level1 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 5 : 1.5f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .3f : .35f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-0.5, 2) : new Point(0, 1.5)
        };

        /// <default>
        /// <br> Brush = Material.Shadow </br>
        /// <br> Radius = Android: 7 / MacOS,Windows,iOS: 1.8f </br>
        /// <br> Opacity = Android: 0.35f / MacOS,Windows,iOS: 0.38f </br>
        /// <br> Offset = Android: new Point(-1, 2.5) / MacOS,Windows,iOS: new Point(0, 2) </br>
        /// </default>
        public static Shadow Level2 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 7 : 1.8f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .35f : .38f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-1, 2.5) : new Point(0, 2)
        };

        /// <default>
        /// <br> Brush = Material.Shadow </br>
        /// <br> Radius = Android: 10 / MacOS,Windows,iOS: 2.4f </br>
        /// <br> Opacity = Android: 0.4f / MacOS,Windows,iOS: 0.38f </br>
        /// <br> Offset = Android: new Point(-1.5, 4) / MacOS,Windows,iOS: new Point(0, 2.4) </br>
        /// </default>
        public static Shadow Level3 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 10 : 2.4f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .4f : .38f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-1.5, 4) : new Point(0, 2.4)
        };

        /// <default>
        /// <br> Brush = Material.Shadow </br>
        /// <br> Radius = Android: 12 / MacOS,Windows,iOS: 2.8f </br>
        /// <br> Opacity = Android: 0.45f / MacOS,Windows,iOS: 0.4f </br>
        /// <br> Offset = Android: new Point(-2.5, 4.5) / MacOS,Windows,iOS: new Point(0, 2.6) </br>
        /// </default>
        public static Shadow Level4 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 12 : 2.8f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .45f : .4f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-2.5, 4.5) : new Point(0, 2.6)
        };

        /// <default>
        /// <br> Brush = Material.Shadow </br>
        /// <br> Radius = Android: 16 / MacOS,Windows,iOS: 3.4f </br>
        /// <br> Opacity = Android: 0.5f / MacOS,Windows,iOS: 0.45f </br>
        /// <br> Offset = Android: new Point(-3.5, 5) / MacOS,Windows,iOS: new Point(0, 3) </br>
        /// </default>
        public static Shadow Level5 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 16 : 3.4f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .5f : .45f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-3.5, 5) : new Point(0, 3)
        };

        internal static void Configure(MaterialElevationOptions options)
        {
            if (options.Level1 != null) Level1 = options.Level1;
            if (options.Level2 != null) Level2 = options.Level2;
            if (options.Level3 != null) Level3 = options.Level3;
            if (options.Level4 != null) Level4 = options.Level4;
            if (options.Level5 != null) Level5 = options.Level5;
        }
    }
}

