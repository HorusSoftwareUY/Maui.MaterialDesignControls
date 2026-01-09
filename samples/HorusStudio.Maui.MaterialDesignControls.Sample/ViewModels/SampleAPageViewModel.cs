using System.Collections.ObjectModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

public partial class SampleAPageViewModel : BaseViewModel
{
    public override string Title => Models.Pages.SampleA;
    
    [ObservableProperty] 
    private ObservableCollection<MaterialSegmentedButtonItem> _segmentedButtonItems = new ObservableCollection<MaterialSegmentedButtonItem>()
    {
        new MaterialSegmentedButtonItem("Songs")
        {
            SelectedIcon = "ic_checkbox.png"
        },
        new MaterialSegmentedButtonItem("Albums")
        {
            SelectedIcon = "ic_checkbox.png"
        },
        new MaterialSegmentedButtonItem("Podcasts")
        {
            SelectedIcon = "ic_checkbox.png"
        }
    };
    
    [ObservableProperty]
    private MaterialSegmentedButtonItem _selectedSegmentedButtonItem;

    public SampleAPageViewModel()
    {
        Subtitle = "A single-page overview of multiple Material Design controls.";
        
        SelectedSegmentedButtonItem = SegmentedButtonItems[0];
    }
}
