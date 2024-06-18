using Microsoft.Maui.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorusStudio.Maui.MaterialDesignControls;
partial class CustomSliderHandler : SliderHandler
{
    public CustomSliderHandler() : base(Mapper, CommandMapper)
    {
        Mapper.Add(nameof(CustomSlider.TrackHeight), MapDesignProperties);
    }
}
