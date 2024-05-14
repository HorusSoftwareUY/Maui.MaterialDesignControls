﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels;

public partial class RadioButtonViewModel : BaseViewModel
{
    #region Attributes & Properties

    public override string Title => "Radio Buttons";

    [ObservableProperty]
    public ObservableCollection<CustomColor> _itemsSourceColors;

    [ObservableProperty]
    public CustomColor _checkedColor;


    [ObservableProperty]
    public CustomColor _firstColor;


    [ObservableProperty]
    public CustomColor _secondColor;


    [ObservableProperty]
    public CustomColor _thirdColor;

    #endregion

    public RadioButtonViewModel()
    {
        Subtitle = "Radio buttons let people select one option from a set of options";

        ItemsSourceColors = new ObservableCollection<CustomColor>
            {
                new CustomColor()
                {
                    Color = "Red",
                    Id = 1
                },
                new CustomColor()
                {
                    Color = "Blue",
                    Id = 2
                },
                new CustomColor()
                {
                    Color = "Green",
                    Id = 3
                }
            };

        CheckedColor = ItemsSourceColors.FirstOrDefault();
        FirstColor = ItemsSourceColors[0];
        SecondColor = ItemsSourceColors[1];
        ThirdColor = ItemsSourceColors[2];
    }

    [ICommand]
    private async Task CheckedChanged(string message)
    {
        await Task.Delay(TimeSpan.FromSeconds(2));
        await DisplayAlert(Title, CheckedColor.Color ?? "none", "OK");
    }

}

public class CustomColor
{
    public int Id { get; set; }

    public string Color { get; set; }

    // We override this method only to show a Custom Object without set PropertyPath in Full API example.
    public override string ToString()
    {
        return Color;
    }

    public override bool Equals(object obj)
    {
        if(obj is CustomColor color)
        {
            return Color.Equals(color.Color);
        }
        return false;
    }
}
