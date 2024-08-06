using System;
namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// Elevation is the distance between two surfaces on the z-axis <see href="https://m3.material.io/styles/elevation/overview">see here.</see>
    /// </summary>
	public static class MaterialElevation
	{
        /// <default>
        /// Brush = Material.Shadow
        /// Radius = Android: 5 - MacOS,Windows,iOS: 1.5f
        /// Opacity = Android: 0.3f - MacOS,Windows,iOS: 0.35f
        /// Offset = Android: new Point(-0.5, 2) - MacOS,Windows,iOS: new Point(0, 1.5)
        /// </default>
        public static Shadow Level1 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 5 : 1.5f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .3f : .35f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-0.5, 2) : new Point(0, 1.5)
        };

        /// <default>
        /// Brush = Material.Shadow
        /// Radius = Android: 7 - MacOS,Windows,iOS: 1.8f
        /// Opacity = Android: 0.35f - MacOS,Windows,iOS: 0.38f
        /// Offset = Android: new Point(-1, 2.5) - MacOS,Windows,iOS: new Point(0, 2)
        /// </default>
        public static Shadow Level2 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 7 : 1.8f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .35f : .38f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-1, 2.5) : new Point(0, 2)
        };

        /// <default>
        /// Brush = Material.Shadow
        /// Radius = Android: 10 - MacOS,Windows,iOS: 2.4f
        /// Opacity = Android: 0.4f - MacOS,Windows,iOS: 0.38f
        /// Offset = Android: new Point(-1.5, 4) - MacOS,Windows,iOS: new Point(0, 2.4)
        /// </default>
        public static Shadow Level3 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 10 : 2.4f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .4f : .38f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-1.5, 4) : new Point(0, 2.4)
        };

        /// <default>
        /// Brush = Material.Shadow
        /// Radius = Android: 12 - MacOS,Windows,iOS: 2.8f
        /// Opacity = Android: 0.45f - MacOS,Windows,iOS: 0.4f
        /// Offset = Android: new Point(-2.5, 4.5) - MacOS,Windows,iOS: new Point(0, 2.6)
        /// </default>
        public static Shadow Level4 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 12 : 2.8f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .45f : .4f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-2.5, 4.5) : new Point(0, 2.6)
        };

        /// <default>
        /// Brush = Material.Shadow
        /// Radius = Android: 16 - MacOS,Windows,iOS: 3.4f
        /// Opacity = Android: 0.5f - MacOS,Windows,iOS: 0.45f
        /// Offset = Android: new Point(-3.5, 5) - MacOS,Windows,iOS: new Point(0, 3)
        /// </default>
        public static Shadow Level5 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 16 : 3.4f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .5f : .45f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-3.5, 5) : new Point(0, 3)
        };
    }
}

