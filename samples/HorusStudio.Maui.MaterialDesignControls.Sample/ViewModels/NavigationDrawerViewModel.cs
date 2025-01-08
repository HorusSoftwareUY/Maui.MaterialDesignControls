using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.ViewModels
{
    public partial class NavigationDrawerViewModel : BaseViewModel
    {
        #region Attributes & Properties

        private bool _includeAllItems = true;
        private int _counter;

        private MaterialNavigationDrawerItem _variantItem;

        public override string Title => "Navigation Drawer";

        [ObservableProperty]
        public ObservableCollection<MaterialNavigationDrawerItem> _items;

        #endregion

        public NavigationDrawerViewModel()
        {
            Subtitle = "Navigation drawers let people switch between UI views on larger devices";

            LoadItems(_includeAllItems);
        }

        #region Commands

        [ICommand]
        private async Task Test(MaterialNavigationDrawerItem selectedItem)
        {
            if (_variantItem.Equals(selectedItem))
            {
                IncrementBadgetText();
            }
            else
            {
                await DisplayAlert.Invoke("Navigation Item", $"{selectedItem}", "Ok");
            }
        }

        [ICommand]
        private void ChangeItemsSource()
        {
            _includeAllItems = !_includeAllItems;
            LoadItems(_includeAllItems);
        }

        #endregion Commands

        #region Methods

        private void LoadItems(bool includeAllItems)
        {
            _variantItem = new MaterialNavigationDrawerItem()
            {
                SelectedLeadingIcon = "email.png",
                UnselectedLeadingIcon = "email.png",
                Text = "Outbox",
                BadgeText = "100+",
            };

            var list = new List<MaterialNavigationDrawerItem>
            {
                _variantItem,

                new MaterialNavigationDrawerItem
                {
                    SelectedLeadingIcon = "email.png",
                    UnselectedLeadingIcon = "email.png",
                    SelectedTrailingIcon = "arrow_drop_down.png",
                    UnselectedTrailingIcon = "arrow_drop_down.png",
                    Text = "Inbox"
                },
                new MaterialNavigationDrawerItem
                {
                    Text = "Favorites (Different icons)",
                    SelectedLeadingIcon = "star_selected.png",
                    UnselectedLeadingIcon = "star_unselected.png",
                },
                new MaterialNavigationDrawerItem
                {
                    SelectedLeadingIcon = "trash.png",
                    UnselectedLeadingIcon = "trash.png",
                    Text = "Trash",
                },
                new MaterialNavigationDrawerItem
                {
                    Section = "Other samples",
                    SelectedLeadingIcon = "card.png",
                    UnselectedLeadingIcon = "card.png",
                    Text = "Selected by default",
                    IsSelected = true
                },
            };

            if (includeAllItems)
            {
                list.AddRange(new List<MaterialNavigationDrawerItem>
                {
                    new MaterialNavigationDrawerItem
                    {
                        Section = "Other samples",
                        SelectedLeadingIcon = "card.png",
                        UnselectedLeadingIcon = "card.png",
                        Text = "Disabled",
                        IsEnabled = false
                    },
                    new MaterialNavigationDrawerItem
                    {
                        Section = "Other samples",
                        SelectedLeadingIcon = "card.png",
                        UnselectedLeadingIcon = "card.png",
                        Text = "Don't show active indicator",
                        ShowActiveIndicator = false
                    },
                    new MaterialNavigationDrawerItem
                    {
                        Section = "Other samples",
                        SelectedLeadingIcon = "star_selected.png",
                        UnselectedLeadingIcon = "star_unselected.png",
                        SelectedTrailingIcon = "card.png",
                        UnselectedTrailingIcon = "card.png",
                        Text = "Custom icon",
                    },
                    new MaterialNavigationDrawerItem
                    {
                        Section = "Other samples",
                        Text = "Without icon",
                    }
                });
            }

            Items = new ObservableCollection<MaterialNavigationDrawerItem>(list);
        }

        private void IncrementBadgetText()
        {
            _counter++;
            _variantItem.BadgeText = $" {100 + _counter}+";
        }

        #endregion Methods
    }
}
