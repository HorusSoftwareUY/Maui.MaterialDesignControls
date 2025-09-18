using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// Define <see cref="MaterialSegmentedButton">material segmented button</see> types.
/// </summary>
public enum MaterialSegmentedButtonType
{
    /// <summary>Outlined segmented button</summary>
    Outlined,
    /// <summary>Filled segmented button</summary>
    Filled
}

/// <summary>
/// Segmented buttons help people select options, switch views, or sort elements, and follow Material Design Guidelines. <see href="https://m3.material.io/components/segmented-buttons/overview">See more</see>.
/// </summary>
/// <example>
///
/// <img>https://raw.githubusercontent.com/HorusSoftwareUY/MaterialDesignControlsPlugin/develop/screenshots/MaterialSegmentedButton.gif</img>
///
/// <h3>XAML sample</h3>
/// <code>
/// <xaml>
/// xmlns:material="clr-namespace:HorusStudio.Maui.MaterialDesignControls;assembly=HorusStudio.Maui.MaterialDesignControls"
/// 
/// &lt;material:MaterialSegmentedButton
///     ItemsSource="{Binding Items}"
///     SelectionCommand="{Binding OnItemSelectedCommand}"
///     Type="Outlined"/&gt;
/// </xaml>
/// </code>
/// 
/// <h3>C# sample</h3>
/// <code>
/// var segmentedButtons = new MaterialSegmentedButton
/// {
///     SelectionCommand = OnItemSelectedCommand,
///     ItemsSource = Items,
///     Type = MaterialSegmentedButtonType.Outlined
/// };
/// </code>
/// 
/// [See more example](../../samples/HorusStudio.Maui.MaterialDesignControls.Sample/Pages/SegmentedButtonsPage.xaml)
/// 
/// </example>
/// <todoList>
/// * [iOS] FontAttributes doesn't work (MAUI issue)
/// * [iOS] TextDecorations doesn't work correctly when different values are set for different VisualStates
/// </todoList>
public class MaterialSegmentedButton : ContentView, ITouchableView
{   
    #region Attributes

    private readonly static MaterialSegmentedButtonType DefautlSegmentedType = MaterialSegmentedButtonType.Outlined;
    private readonly static IEnumerable<MaterialSegmentedButtonItem>? DefaultItemsSource = null;
    private readonly static MaterialSegmentedButtonItem? DefaultSelectedItem = null;
    private readonly static bool DefaultAllowMultiSelect = false;
    private readonly static bool DefaultIsEnabled = true;
    private readonly static Thickness DefaultPadding = new(12, 0);
    private readonly static CornerRadius DefaultCornerRadius = new(16);
    private readonly static Color DefaultBorderColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Outline, Dark = MaterialDarkTheme.Outline }.GetValueForCurrentTheme<Color>();
    private readonly static double DefaultBorderWidth = 1;
    private readonly static Color DefaultBackgroundColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Surface, Dark = MaterialDarkTheme.Surface }.GetValueForCurrentTheme<Color>();
    private readonly static BindableProperty.CreateDefaultValueDelegate DefaultTouchAnimationType = _ => MaterialAnimation.TouchAnimationType;
    private readonly static double DefaultHeightRequest = 40;

    #endregion

    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="MaterialSegmentedButtonType">MaterialSegmentedButtonType</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TypeProperty = BindableProperty.Create(nameof(Type), typeof(MaterialSegmentedButtonType), typeof(MaterialSegmentedButton), defaultValue: DefautlSegmentedType, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmentedButton self)
        {
            self.SetBackgroundColor();
            self.UpdateItemsSource();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="ItemsSource">ItemsSource</see> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<MaterialSegmentedButtonItem>), typeof(MaterialSegmentedButton), defaultValue: DefaultItemsSource, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmentedButton self)
        {
            self.UpdateItemsSource();
        }
    });
    
    /// <summary>
    /// The backing store for the <see cref="AllowMultiSelect">AllowMultiSelect</see> bindable property.
    /// </summary>
    public static readonly BindableProperty AllowMultiSelectProperty = BindableProperty.Create(nameof(AllowMultiSelect), typeof(bool), typeof(MaterialSegmentedButton), defaultValue: DefaultAllowMultiSelect, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmentedButton self)
        {
            self.UpdateItemsSource();
        }
    });

    /// <summary>
    /// Gets or sets the state when the Segmented is enabled.
    /// bindable property.
    /// </summary>
    public new static readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialSegmentedButton), defaultValue: DefaultIsEnabled, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmentedButton self)
        {
            self.SetIsEnabled();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="MaterialSegmentedButton.Padding">Padding</see> bindable property.
    /// </summary>
    public static new readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(MaterialSegmentedButton), defaultValue: DefaultPadding);
    
    /// <summary>
    /// The backing store for the <see cref="CornerRadius">CornerRadius</see> bindable property.
    /// </summary>
    public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(MaterialSegmentedButton), defaultValue: DefaultCornerRadius);
    
    /// <summary>
    /// The backing store for the <see cref="BorderColor">BorderColor</see> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(MaterialSegmentedButton), defaultValue: DefaultBorderColor);

    /// <summary>
    /// The backing store for the <see cref="BorderWidth">BorderWidth</see> bindable property.
    /// </summary>
    public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(MaterialSegmentedButton), defaultValue: DefaultBorderWidth);

    /// <summary>
    /// The backing store for the <see cref="BackgroundColor">BackgroundColor</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(MaterialSegmentedButton), defaultValue: DefaultBackgroundColor, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmentedButton self)
        {
            self.SetBackgroundColor();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Command">Command</see> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectionCommandProperty = BindableProperty.Create(nameof(SelectionCommand), typeof(ICommand), typeof(MaterialSegmentedButton));
    
    /// <summary>
    /// The backing store for the <see cref="TouchAnimationType">TouchAnimationType</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TouchAnimationTypeProperty = BindableProperty.Create(nameof(TouchAnimationType), typeof(TouchAnimationTypes), typeof(MaterialSegmentedButton), defaultValueCreator: DefaultTouchAnimationType);

    /// <summary>
    /// The backing store for the <see cref="TouchAnimation">TouchAnimation</see> bindable property.
    /// </summary>
    public static readonly BindableProperty TouchAnimationProperty = BindableProperty.Create(nameof(TouchAnimation), typeof(ITouchAnimation), typeof(MaterialSegmentedButton));

    /// <summary>
    /// The backing store for the <see cref="ItemTemplate">ItemTemplate</see> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(MaterialSegmentedButton), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="HeightRequest">HeightRequest</see> bindable property.
    /// </summary>
    public new static readonly BindableProperty HeightRequestProperty = BindableProperty.Create(nameof(HeightRequest), typeof(double), typeof(MaterialSegmentedButton), defaultValue: DefaultHeightRequest);

    /// <summary>
    /// The backing store for the <see cref="SelectedItems">SelectedItems</see> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemsProperty = BindableProperty.Create(nameof(SelectedItems), typeof(IEnumerable<MaterialSegmentedButtonItem>), typeof(MaterialSegmentedButton), defaultValue: Array.Empty<MaterialSegmentedButtonItem>(), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmentedButton self)
        {
            self.SetSelectedItems();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="SelectedItem">SelectedItem</see> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(MaterialSegmentedButtonItem), typeof(MaterialSegmentedButton), defaultValue: null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialSegmentedButton self)
        {
            self.SetSelectedItem();
        }
    });

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the segmented button <see cref="MaterialSegmentedButtonType">type</see>.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="MaterialButtonType.Outlined">MaterialButtonType.Outlined</see>
    /// </default>
    public MaterialSegmentedButtonType Type
    {
        get => (MaterialSegmentedButtonType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    /// <summary>
    /// Gets or sets items' source to define segments.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    public IEnumerable<MaterialSegmentedButtonItem> ItemsSource
    {
        get => (IEnumerable<MaterialSegmentedButtonItem>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets if the segmented button allows multiple selection.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="false">False</see>
    /// </default>
    public bool AllowMultiSelect
    {
        get => (bool)GetValue(AllowMultiSelectProperty);
        set => SetValue(AllowMultiSelectProperty, value);
    }

    /// <summary>
    /// Gets or sets if the segmented button is enabled.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="true">True</see>
    /// </default>
    public new bool IsEnabled
    {
        get => (bool)GetValue(IsEnabledProperty);
        set => SetValue(IsEnabledProperty, value);
    }

    /// <summary>
    /// Gets or sets the corner radius for the segmented button.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// CornerRadius(16)
    /// </default>
    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the border <see cref="Color">color</see> for the segmented button.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Outline">MaterialLightTheme.Outline</see> - Dark = <see cref="MaterialDarkTheme.Outline">MaterialDarkTheme.Outline</see>
    /// </default>
    /// <remarks>
    /// <para>This property has no effect if <see cref="IBorderElement.BorderWidth">IBorderElement.BorderWidth</see> is set to 0.</para>
    /// <para>On Android this property will not have an effect unless <see cref="VisualElement.BackgroundColor">background color</see> is set to a non-default color.</para>
    /// </remarks>
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the width of the border, in device-independent units.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 1
    /// </default>
    /// <remarks>This property has no effect if <see cref="MaterialSegmentedButton.Type">type</see> is set to <see cref="MaterialSegmentedButtonType.Filled">Filled</see>.</remarks>
    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }

    /// <summary>
    /// Gets or sets the Background <see cref="Color">color</see> for the segmented button.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// Theme: Light = <see cref="MaterialLightTheme.Surface">MaterialLightTheme.Surface</see> - Dark = <see cref="MaterialDarkTheme.Surface">MaterialDarkTheme.Surface</see>
    /// </default>
    /// <remarks>This property has no effect if <see cref="MaterialSegmentedButton.Type">type</see> is set to <see cref="MaterialSegmentedButtonType.Filled">Outlined</see>.</remarks>
    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    /// <summary>
    /// Gets or sets the command invoked when the selection of segmented button changes.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    /// <remarks>
    /// If <see cref="AllowMultiSelect">AllowMultiSelect</see>=<see langword="true">True</see>, then <see cref="IEnumerable{MaterialSegmentedButton}">IEnumerable{MaterialSegmentedButton}</see>; <see cref="MaterialSegmentedButtonItem">MaterialSegmentedButtonItem</see> otherwise.
    /// </remarks>
    public ICommand SelectionCommand
    {
        get => (ICommand)GetValue(SelectionCommandProperty);
        set => SetValue(SelectionCommandProperty, value);
    }
    
    /// <summary>
    /// Gets or sets an animation to be executed when is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="TouchAnimationTypes.Fade">TouchAnimationTypes.Fade</see>
    /// </default>
    public TouchAnimationTypes TouchAnimationType
    {
        get => (TouchAnimationTypes)GetValue(TouchAnimationTypeProperty);
        set => SetValue(TouchAnimationTypeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets a custom animation to be executed when is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    public ITouchAnimation TouchAnimation
    {
        get => (ITouchAnimation)GetValue(TouchAnimationProperty);
        set => SetValue(TouchAnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the item template for each item from ItemsSource.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    /// <summary>
    /// Gets or sets the desired height override of this element.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// 40
    /// </default>
    public new double HeightRequest
    {
        get => (double)GetValue(HeightRequestProperty);
        set => SetValue(HeightRequestProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected buttons.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="Array.Empty">Array.Empty</see>
    /// </default>
    /// <remarks>Used only when <see cref="AllowMultiSelect">AllowMultiSelect</see>=<see langword="true">True</see>.</remarks>
    public IEnumerable<MaterialSegmentedButtonItem> SelectedItems
    {
        get => (IEnumerable<MaterialSegmentedButtonItem>)GetValue(SelectedItemsProperty);
        set => SetValue(SelectedItemsProperty, value);
    }

    /// <summary>
    /// Gets or sets the selected button.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null">Null</see>
    /// </default>
    /// <remarks>Used only when <see cref="AllowMultiSelect">AllowMultiSelect</see>=<see langword="false">False</see>.</remarks>
    public MaterialSegmentedButtonItem? SelectedItem
    {
        get => (MaterialSegmentedButtonItem?)GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }

    #endregion

    #region Events

    private EventHandler<TouchEventArgs>? _touch;
    private readonly object _objectLock = new();
    
    /// <summary>
    /// Occurs when the selection of one of the segmented buttons changes.
    /// </summary>
    public event EventHandler<SegmentedButtonSelectedEventArgs>? SelectionChanged;
    
    /// <summary>
    /// Occurs when the segmented button is touched.
    /// </summary>
    public event EventHandler<TouchEventArgs>? Touch
    {
        add
        {
            lock (_objectLock)
            {
                _touch += value;
                AddTouchEvents();
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _touch -= value;
                RemoveTouchEvents();
            }
        }
    }

    private void OnSegmentedButtonTap(MaterialSegmentedButtonItem segmentedButton)
    {
        if (!IsEnabled)
        {
            return;
        }

        if (AllowMultiSelect)
        {
            segmentedButton.IsSelected = !segmentedButton.IsSelected;

            SelectedItems = ItemsSource.Where(x => x.IsSelected);
        }
        else
        {
            foreach (var itemCollection in ItemsSource)
            {
                if (itemCollection.Text == segmentedButton.Text)
                {
                    itemCollection.IsSelected = true;
                }
                else
                {
                    itemCollection.IsSelected = false;
                }
            }

            SelectedItem = ItemsSource.FirstOrDefault(x => x.IsSelected);
        }

        SetVisualStateToItems();
    }

    private void InvokeSelectionChanged()
    {
        object? commandParameter = AllowMultiSelect ? SelectedItems : SelectedItem;
        if (SelectionCommand != null && SelectionCommand.CanExecute(commandParameter))
        {
            SelectionCommand.Execute(commandParameter);
        }

        if (AllowMultiSelect)
        {
            SelectionChanged?.Invoke(this, new SegmentedButtonSelectedEventArgs(SelectedItems));
        }
        else
        {
            SelectionChanged?.Invoke(this, new SegmentedButtonSelectedEventArgs(SelectedItem));
        }
    }
    
    protected virtual async void InternalTouchHandler(object? sender, TouchEventArgs e)
    {
        if (!IsEnabled) return;
        
        _touch?.Invoke(sender, new TouchEventArgs(e.TouchEventType));
    }
    
    private void AddTouchEvents()
    {
        if (_itemsContainer.Children != null && _itemsContainer.Children.Any())
        {
            foreach (var itemView in _itemsContainer.Children)
            {
                if (itemView is ITouchableView touchableView)
                {
                    touchableView.Touch += InternalTouchHandler;
                }
            }
        }
    }
    
    private void RemoveTouchEvents()
    {
        if (_itemsContainer.Children != null && _itemsContainer.Children.Any())
        {
            foreach (var itemView in _itemsContainer.Children)
            {
                if (itemView is ITouchableView touchableView)
                {
                    touchableView.Touch -= InternalTouchHandler;
                }
            }
        }
    }

    #endregion

    #region Layout

    private MaterialCard? _container;
    private Grid? _itemsContainer;

    #endregion

    #region Constructor

    public MaterialSegmentedButton()
    {
        _itemsContainer = new Grid
        {
            ColumnSpacing = 0,
            Padding = 0,
        };
        
        _container = new MaterialCard
        {
            Type = MaterialCardType.Filled,
            BorderWidth = 0,
            BorderColor = Colors.Transparent,
            BackgroundColor = Colors.Transparent,
            Padding = 0,
            Content = _itemsContainer
        };

        _container.SetBinding(MaterialCard.HeightRequestProperty, new Binding(nameof(HeightRequest), source: this));
        _container.SetBinding(MaterialCard.MinimumHeightRequestProperty, new Binding(nameof(MinimumHeightRequest), source: this));
        _container.SetBinding(MaterialCard.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));

        Content = _container;

        SetBackgroundColor();
    }

    #endregion

    #region Setters

    private void SetSelectedItems()
    {
        if (ItemsSource != null)
        {
            foreach (var item in ItemsSource)
            {
                item.IsSelected = SelectedItems != null && SelectedItems.Contains(item);
            }
        }

        SetVisualStateToItems();

        InvokeSelectionChanged();
    }

    private void SetSelectedItem()
    {
        if (ItemsSource != null)
        {
            foreach (var item in ItemsSource)
            {
                item.IsSelected = SelectedItem != null && SelectedItem == item;
            }
        }

        SetVisualStateToItems();

        InvokeSelectionChanged();
    }

    private void UpdateItemsSource()
    {
        if (_itemsContainer == null)
        {
            return;
        }

        RemoveTouchEvents();
        
        _itemsContainer.Children.Clear();
        _itemsContainer.ColumnDefinitions = new ColumnDefinitionCollection();
        _itemsContainer.ColumnSpacing = Type == MaterialSegmentedButtonType.Outlined ? -2 : 0;

        if (ItemsSource == null)
        {
            return;
        }

        var indexColum = 0;
        foreach (var item in ItemsSource)
        {
            var result = AddItem(item, indexColum);
            if (result)
            {
                indexColum++;
            }
        }

        SetVisualStateToItems();
    }

    private bool AddItem(MaterialSegmentedButtonItem item, int indexColum)
    {
        try
        {
            Utils.Logger.Debug($"Creating item '{item.Text}'");
            return AddItem(item, ItemTemplate ?? GetDefaultItemDataTemplate(item), indexColum);
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException($"ERROR creating item '{item.Text}'", ex, this);
        }

        return false;
    }

    private bool AddItem(MaterialSegmentedButtonItem item, DataTemplate itemTemplate, int indexColum)
    {
        if (itemTemplate?.CreateContent() is not MaterialSegmentedButtonItemView itemView)
        {
            Utils.Logger.LogException(new Exception("ERROR: The root element of the DataTemplate must be a MaterialSegmentedButtonViewItem."), this);
            return false;
        }

        var itemLayout = CreateItemLayout(item, indexColum);
        if (itemLayout == null)
        {
            return false;
        }

        itemLayout.Content = itemView;
        itemLayout.BindingContext = item;

        var result = itemView.CreateItemContent(item);
        if (!result)
        {
            return false;
        }

        if (_itemsContainer == null)
        {
            return false;
        }
        
        if (itemLayout is ITouchableView touchableView && _touch != null)
        {
            touchableView.Touch += InternalTouchHandler;
        }
        
        _itemsContainer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(ItemsSource.Count() / 100.0, GridUnitType.Star) });
        _itemsContainer.Add(itemLayout, indexColum);

        return true;
    }

    private DataTemplate GetDefaultItemDataTemplate(MaterialSegmentedButtonItem item)
    {
        return new DataTemplate(() =>
        {
            var itemContent = new MaterialSegmentedButtonItemView();
            itemContent.Style = GetItemViewStyle();
            return itemContent;
        });
    }

    private MaterialCard? CreateItemLayout(MaterialSegmentedButtonItem item, int indexColum)
    {
        try
        {
            Utils.Logger.Debug("Creating item layout");
            var materialCard = new MaterialCard
            {
                Type = MaterialCardType.Filled,
                ShadowColor = Colors.Transparent,
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.Fill,
                Margin = new Thickness(0),
                Padding = new Thickness(0),
                BorderWidth = 0,
                BorderColor = Colors.Transparent,
                Command = new Command(() => OnSegmentedButtonTap(item), () => IsEnabled)
            };
            materialCard.Style = GetItemLayoutStyle();

            materialCard.SetBinding(MaterialCard.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
            materialCard.SetBinding(MaterialCard.TouchAnimationTypeProperty, new Binding(nameof(TouchAnimationType), source: this));
            materialCard.SetBinding(MaterialCard.TouchAnimationProperty, new Binding(nameof(TouchAnimation), source: this));

            if (Type == MaterialSegmentedButtonType.Outlined)
            {
                materialCard.Type = MaterialCardType.Custom;
                materialCard.SetBinding(MaterialCard.BorderColorProperty, new Binding(nameof(BorderColor), source: this));
                materialCard.SetBinding(MaterialCard.BorderWidthProperty, new Binding(nameof(BorderWidth), source: this));
                if (ItemsSource.Count() > 1)
                {
                    if (indexColum == 0)
                    {
                        materialCard.CornerRadius = new CornerRadius(CornerRadius.TopLeft, 0, CornerRadius.BottomLeft, 0);
                    }
                    else if (indexColum == ItemsSource.Count() - 1)
                    {
                        materialCard.CornerRadius = new CornerRadius(0, CornerRadius.TopRight, 0, CornerRadius.BottomRight);
                    }
                    else
                    {
                        materialCard.CornerRadius = 0;
                        materialCard.Margin = new Thickness(-1, 0);
                    }
                }
                else
                {
                    materialCard.CornerRadius = CornerRadius;
                }
            }
            else
            {
                materialCard.SetBinding(MaterialCard.CornerRadiusProperty, new Binding(nameof(CornerRadius), source: this));
            }

            return materialCard;
        }
        catch (Exception ex)
        {
            Utils.Logger.LogException("ERROR creating item layout", ex, this);
            return null;
        }
    }

    private void SetIsEnabled()
    {
        SetBackgroundColor();

        if (IsEnabled)
        {
            VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Normal);
        }
        else
        {
            VisualStateManager.GoToState(this, VisualStateManager.CommonStates.Disabled);
        }

        SetVisualStateToItems();
    }

    private void SetVisualStateToItems()
    {
        if (_itemsContainer != null)
        {
            foreach (var itemLayout in _itemsContainer.Children)
            {
                if (itemLayout is MaterialCard materialCard
                    && materialCard.BindingContext is MaterialSegmentedButtonItem item)
                {
                    if (IsEnabled)
                    {
                        if (item.IsSelected)
                        {
                            VisualStateManager.GoToState(materialCard, VisualStateManager.CommonStates.Selected);
                            VisualStateManager.GoToState(materialCard.Content, VisualStateManager.CommonStates.Selected);
                        }
                        else
                        {
                            VisualStateManager.GoToState(materialCard, VisualStateManager.CommonStates.Normal);
                            VisualStateManager.GoToState(materialCard.Content, VisualStateManager.CommonStates.Normal);
                        }
                    }
                    else
                    {
                        VisualStateManager.GoToState(materialCard, VisualStateManager.CommonStates.Disabled);
                        VisualStateManager.GoToState(materialCard.Content, VisualStateManager.CommonStates.Disabled);
                    }
                }
            }
        }
    }

    private void SetBackgroundColor()
    {
        if (_itemsContainer == null || _container == null)
        {
            return;
        }

        if (Type == MaterialSegmentedButtonType.Outlined)
        {
            _container.BackgroundColor = Colors.Transparent;
            _itemsContainer.BackgroundColor = Colors.Transparent;
        }
        else
        {
            _container.BackgroundColor = BackgroundColor;
            _itemsContainer.BackgroundColor = BackgroundColor;
        }
    }

    public async void OnTouch(TouchEventType gestureType)
    {
        // Nothing is done since the touch events are handled by each item, and the animation properties were bound to each of them individually.
    }
    
    #endregion
    
    #region Styles

    private Style GetItemViewStyle()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = VisualStateManager.CommonStates.Disabled };

        disabledState.Setters.Add(
            MaterialSegmentedButtonItemView.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Surface,
                Dark = MaterialDarkTheme.Surface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        disabledState.Setters.Add(
            MaterialSegmentedButtonItemView.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.38f));

        var normalState = new VisualState { Name = VisualStateManager.CommonStates.Normal };

        normalState.Setters.Add(
            MaterialSegmentedButtonItemView.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Surface,
                Dark = MaterialDarkTheme.Surface
            }
            .GetValueForCurrentTheme<Color>());

        normalState.Setters.Add(
            MaterialSegmentedButtonItemView.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>());

        var selectedState = new VisualState { Name = VisualStateManager.CommonStates.Selected };

        selectedState.Setters.Add(
            MaterialSegmentedButtonItemView.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.SecondaryContainer,
                Dark = MaterialDarkTheme.SecondaryContainer
            }
            .GetValueForCurrentTheme<Color>());

        selectedState.Setters.Add(
            MaterialSegmentedButtonItemView.TextColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.OnSurface,
                Dark = MaterialDarkTheme.OnSurface
            }
            .GetValueForCurrentTheme<Color>());

        commonStatesGroup.States.Add(disabledState);
        commonStatesGroup.States.Add(normalState);
        commonStatesGroup.States.Add(selectedState);

        var style = new Style(typeof(MaterialSegmentedButtonItemView));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return style;
    }

    private Style GetItemLayoutStyle()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = ButtonCommonStates.Disabled };
        disabledState.Setters.Add(
            MaterialCard.BorderColorProperty,
            new AppThemeBindingExtension
            {
                Light = DefaultBorderColor,
                Dark = DefaultBorderColor
            }
            .GetValueForCurrentTheme<Color>());

        var pressedState = new VisualState { Name = ButtonCommonStates.Pressed };

        var normalState = new VisualState { Name = ButtonCommonStates.Normal };

        commonStatesGroup.States.Add(normalState);
        commonStatesGroup.States.Add(disabledState);
        commonStatesGroup.States.Add(pressedState);

        var style = new Style(typeof(MaterialCard));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });
        return style;
    }

    internal static IEnumerable<Style> GetStyles()
    {
        var commonStatesGroup = new VisualStateGroup { Name = nameof(VisualStateManager.CommonStates) };

        var disabledState = new VisualState { Name = VisualStateManager.CommonStates.Disabled };
        disabledState.Setters.Add(
            MaterialSegmentedButton.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Surface,
                Dark = MaterialDarkTheme.Surface
            }
            .GetValueForCurrentTheme<Color>()
            .WithAlpha(0.12f));

        var normalState = new VisualState { Name = VisualStateManager.CommonStates.Normal };
        normalState.Setters.Add(
            MaterialSegmentedButton.BackgroundColorProperty,
            new AppThemeBindingExtension
            {
                Light = MaterialLightTheme.Surface,
                Dark = MaterialDarkTheme.Surface
            }
            .GetValueForCurrentTheme<Color>());

        commonStatesGroup.States.Add(disabledState);
        commonStatesGroup.States.Add(normalState);

        var style = new Style(typeof(MaterialSegmentedButton));
        style.Setters.Add(VisualStateManager.VisualStateGroupsProperty, new VisualStateGroupList() { commonStatesGroup });

        return new List<Style> { style };
    }

    #endregion Styles
}