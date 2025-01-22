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

        public override string Title => Models.Pages.NavigationDrawer;

        [ObservableProperty]
        private ObservableCollection<MaterialNavigationDrawerItem> _items;
        
        [ObservableProperty]
        private ObservableCollection<MaterialNavigationDrawerItem> _disabledItems;

        #endregion

        public NavigationDrawerViewModel()
        {
            Subtitle = "Navigation drawers let people switch between UI views on larger devices";

            LoadItems(_includeAllItems);
            LoadDisabledItems();
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
            _variantItem = new MaterialNavigationDrawerItem
            {
                Headline = "Mail",
                SelectedLeadingIcon = "email.png",
                LeadingIcon = "email.png",
                Text = "Outbox",
                BadgeText = "100+",
            };

            var list = new List<MaterialNavigationDrawerItem>
            {
                _variantItem,

                new MaterialNavigationDrawerItem
                {
                    Headline = "Mail",
                    SelectedLeadingIcon = "email.png",
                    LeadingIcon = "email.png",
                    SelectedTrailingIcon = "arrow_drop_down.png",
                    TrailingIcon = "arrow_drop_down.png",
                    Text = "Inbox"
                },
                new MaterialNavigationDrawerItem
                {
                    Headline = "Mail",
                    Text = "Favorites (Different icons)",
                    SelectedLeadingIcon = "star_selected.png",
                    LeadingIcon = "star_unselected.png",
                },
                new MaterialNavigationDrawerItem
                {
                    Headline = "Mail",
                    SelectedLeadingIcon = "trash.png",
                    LeadingIcon = "trash.png",
                    Text = "Trash",
                },
                new MaterialNavigationDrawerItem
                {
                    Headline = "Other samples",
                    SelectedLeadingIcon = "card.png",
                    LeadingIcon = "card.png",
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
                        Headline = "Other samples",
                        SelectedLeadingIcon = "card.png",
                        LeadingIcon = "card.png",
                        Text = "Disabled",
                        IsEnabled = false
                    },
                    new MaterialNavigationDrawerItem
                    {
                        Headline = "Other samples",
                        SelectedLeadingIcon = "star_selected.png",
                        LeadingIcon = "star_unselected.png",
                        SelectedTrailingIcon = "card.png",
                        TrailingIcon = "card.png",
                        Text = "Custom icon",
                    },
                    new MaterialNavigationDrawerItem
                    {
                        Headline = "Other samples",
                        Text = "Without icon",
                    }
                });
            }

            Items = new ObservableCollection<MaterialNavigationDrawerItem>(list);
        }

        private void LoadDisabledItems()
        {
            var disabledItems = new List<MaterialNavigationDrawerItem>
            {
                new()
                {
                    Headline = "Disabled",
                    LeadingIcon = "email.png",
                    Text = "With leading icon",
                    IsEnabled = false
                },
                new()
                {
                    Headline = "Disabled",
                    SelectedLeadingIcon = "email.png",
                    LeadingIcon = "email.png",
                    Text = "With leading icon and badge",
                    BadgeText = "24",
                    IsEnabled = false
                },
                new()
                {
                    Headline = "Disabled",
                    SelectedLeadingIcon = "email.png",
                    LeadingIcon = "email.png",
                    SelectedTrailingIcon = "arrow_drop_down.png",
                    TrailingIcon = "arrow_drop_down.png",
                    Text = "With leading and trailing icons",
                    IsEnabled = false
                },
                new()
                {
                    Headline = "Disabled",
                    Text = "With badge",
                    BadgeText = "35",
                    IsEnabled = false
                },
                new()
                {
                    Headline = "Disabled",
                    Text = "With trailing icon",
                    TrailingIcon = "arrow_drop_down.png",
                    IsEnabled = false
                },
                new()
                {
                    Headline = "Disabled",
                    Text = "Without icons",
                    IsEnabled = false
                }
            };
            
            DisabledItems = new ObservableCollection<MaterialNavigationDrawerItem>(disabledItems);
        }
        
        private void IncrementBadgetText()
        {
            _counter++;
            _variantItem.BadgeText = $" {100 + _counter}+";
        }

        #endregion Methods
    }
}
