using System;
namespace HorusStudio.Maui.MaterialDesignControls
{
	public static class MaterialElevation
	{
        public static Shadow Level1 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 5 : 1.5f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .3f : .35f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-0.5, 2) : new Point(0, 1.5)
        };

        public static Shadow Level2 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 7 : 1.8f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .35f : .38f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-1, 2.5) : new Point(0, 2)
        };

        public static Shadow Level3 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 10 : 2.4f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .4f : .38f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-1.5, 4) : new Point(0, 2.4)
        };

        public static Shadow Level4 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 12 : 2.8f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .45f : .4f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-2.5, 4.5) : new Point(0, 2.6)
        };

        public static Shadow Level5 { get; set; } = new Shadow
        {
            Brush = MaterialLightTheme.Shadow,
            Radius = DeviceInfo.Platform == DevicePlatform.Android ? 16 : 3.4f,
            Opacity = DeviceInfo.Platform == DevicePlatform.Android ? .5f : .45f,
            Offset = DeviceInfo.Platform == DevicePlatform.Android ? new Point(-3.5, 5) : new Point(0, 3)
        };
    }
}

