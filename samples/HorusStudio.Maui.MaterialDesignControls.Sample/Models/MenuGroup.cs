using HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Models
{
    public class MenuGroup : List<MenuItemViewModel>
    {
        public string GroupName { get; set; }
        public bool IsBorderVisible { get; set; } = true;
    }
}
