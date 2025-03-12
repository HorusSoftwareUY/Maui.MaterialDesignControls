using System.Runtime.CompilerServices;

namespace HorusStudio.Maui.MaterialDesignControls.Models;

public abstract class BindableObjectBase : BindableObject
{
    protected void SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return;

        backingStore = value;
        OnPropertyChanged(propertyName);
    }
}