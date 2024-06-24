using Microsoft.Maui.Handlers;

namespace HorusStudio.Maui.MaterialDesignControls;
partial class CustomSliderHandler : SliderHandler
{
    public CustomSliderHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CustomSlider.MinimumTrackColor), MapDesignProperties);
        Mapper.Add(nameof(CustomSlider.MaximumTrackColor), MapDesignProperties);
        Mapper.Add(nameof(CustomSlider.TrackHeight), MapDesignProperties);
        Mapper.Add(nameof(CustomSlider.TrackCornerRadius), MapDesignProperties);
        Mapper.Add(nameof(CustomSlider.ThumbImageSource), MapDesignProperties);
        Mapper.Add(nameof(CustomSlider.ThumbColor), MapDesignProperties);
        Mapper.Add(nameof(CustomSlider.ThumbBackgroundColor), MapDesignProperties);
        Mapper.Add(nameof(CustomSlider.ThumbHeight), MapDesignProperties);
        Mapper.Add(nameof(CustomSlider.ThumbWidth), MapDesignProperties);
        Mapper.Add(nameof(CustomSlider.UserInteractionEnabled), MapDesignProperties);
    }
}
