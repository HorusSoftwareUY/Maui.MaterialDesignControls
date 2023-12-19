using System;
using System.Runtime.CompilerServices;
using HorusStudio.Maui.MaterialDesignControls.Implementations;

namespace HorusStudio.Maui.MaterialDesignControls
{
	public class MaterialTextField : MaterialInputBase
	{
        private Entry _entry;

        protected override View CreateView()
        {
            _entry = new BorderlessEntry();
            return _entry;
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName.Equals(MaterialInputBase.TypeProperty.PropertyName))
            {
                if (Type == MaterialInputType.Filled && base.Resources.TryGetValue("FilledEntry", out object filledEntry) && filledEntry is Style filledEntryStyle)
                {
                    _entry.Style = filledEntryStyle;
                }
                else if (Type == MaterialInputType.Outlined && base.Resources.TryGetValue("OutlinedEntry", out object outlinedEntry) && outlinedEntry is Style outlinedEntryStyle)
                {
                    _entry.Style = outlinedEntryStyle;
                }
            }
        }
    }
}

