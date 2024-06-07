
using Microsoft.Maui.Controls.Shapes;
using System.Runtime.CompilerServices;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// A rating <see cref="View" /> allows users to view and set ratings that reflect degrees of satisfaction./>.
/// </summary>
public class MaterialRating : ContentView
{
    #region Attributes
    private readonly static Color DefaultTextColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Text, Dark = MaterialDarkTheme.Text }.GetValueForCurrentTheme<Color>();
    private readonly static Color DefaultStrokeColor = new AppThemeBindingExtension { Light = MaterialLightTheme.Primary, Dark = MaterialDarkTheme.Primary }.GetValueForCurrentTheme<Color>();
    private readonly static double DefaultStrokeThickness = 2.0;
    private readonly static int DefaultItemsSize = 5;
    private readonly static int DefaultItemsPerRow = 5;
    private readonly static string DefaultFontFamily = MaterialFontFamily.Default;
    private readonly static double DefaultCharacterSpacing = MaterialFontTracking.BodyMedium;
    private readonly static double DefaultFontSize = MaterialFontSize.BodyLarge;
    private readonly static AnimationTypes DefaultAnimationType = MaterialAnimation.Type;
#nullable enable
    private readonly static double? DefaultAnimationParameter = MaterialAnimation.Parameter;
#nullable disable

    #endregion Attributes

    #region Layout

    private MaterialLabel _label;
    private Grid _mainLayout;
    private Grid _containerLayout;

    #endregion Layout

    #region Bindable Properties


    /// <summary>
    /// The backing store for the <see cref="Label" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelProperty = BindableProperty.Create(nameof(Label), typeof(string), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="LabelColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelColorProperty = BindableProperty.Create(nameof(LabelColor), typeof(Color), typeof(MaterialRating), defaultValue: DefaultTextColor);

    /// <summary>
    /// The backing store for the <see cref="FontFamily" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(MaterialRating), defaultValue: DefaultFontFamily);
    
    /// <summary>
    /// The backing store for the <see cref="CharacterSpacing" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(MaterialRating), defaultValue: DefaultCharacterSpacing);

    /// <summary>
    /// The backing store for the <see cref="FontAttributes" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="FontAutoScalingEnabled" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontAutoScalingEnabledProperty = BindableProperty.Create(nameof(FontAutoScalingEnabled), typeof(bool), typeof(MaterialRating), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="FontSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(MaterialRating), defaultValue: DefaultFontSize);

    /// <summary>
    /// The backing store for the <see cref="LabelTransform" /> bindable property.
    /// </summary>
    public static readonly BindableProperty LabelTransformProperty = BindableProperty.Create(nameof(LabelTransform), typeof(TextTransform), typeof(MaterialRating), defaultValue: TextTransform.Default);

    /// <summary>
    /// The backing store for the <see cref="IsEnabled" /> bindable property.
    /// </summary>
    public static new readonly BindableProperty IsEnabledProperty = BindableProperty.Create(nameof(IsEnabled), typeof(bool), typeof(MaterialRating), defaultValue: true, defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is MaterialRating self && newValue is bool)
        {
            self.ChangeVisualState();
        }
    });

    /// <summary>
    /// The backing store for the <see cref="Animation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialRating), defaultValue: DefaultAnimationType);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter"/> bindable property.
    /// </summary>
#nullable enable
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialRating), defaultValue: DefaultAnimationParameter);
#nullable disable

    /// <summary>
    /// The backing store for the <see cref="CustomAnimation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialRating));

    /// <summary>
    /// The backing store for the <see cref="StrokeColor" /> bindable property.
    /// </summary>
    public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(MaterialRating), defaultValue: DefaultStrokeColor);

    /// <summary>
    /// The backing store for the <see cref="StrokeThickness" /> bindable property.
    /// </summary>
    public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.Create(nameof(StrokeThickness), typeof(double), typeof(MaterialRating), defaultValue: DefaultStrokeThickness);

    /// <summary>
    /// The backing store for the <see cref="SelectedIconSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedIconSourceProperty = BindableProperty.Create(nameof(SelectedIconSource), typeof(ImageSource), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="UnselectedIconSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty UnselectedIconSourceProperty = BindableProperty.Create(nameof(UnselectedIconSource), typeof(ImageSource), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="SelectedIconsSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty SelectedIconsSourceProperty = BindableProperty.Create(nameof(SelectedIconsSource), typeof(IEnumerable<ImageSource>), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="UnselectedIconsSource" /> bindable property.
    /// </summary>
    public static readonly BindableProperty UnselectedIconsSourceProperty = BindableProperty.Create(nameof(UnselectedIconsSource), typeof(IEnumerable<ImageSource>), typeof(MaterialRating), defaultValue: null);

    /// <summary>
    /// The backing store for the <see cref="UseSameIcon" /> bindable property.
    /// </summary>
    public static readonly BindableProperty UseSameIconProperty = BindableProperty.Create(nameof(UseSameIcon), typeof(bool), typeof(MaterialRating), defaultValue: true);

    /// <summary>
    /// The backing store for the <see cref="ItemsSize" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemsSizeProperty = BindableProperty.Create(nameof(ItemsSize), typeof(int), typeof(MaterialRating), defaultValue: DefaultItemsSize);

    /// <summary>
    /// The backing store for the <see cref="ItemsPerRow" /> bindable property.
    /// </summary>
    public static readonly BindableProperty ItemsPerRowProperty = BindableProperty.Create(nameof(ItemsPerRow), typeof(int), typeof(MaterialRating), defaultValue: DefaultItemsPerRow);

    /// <summary>
    /// The backing store for the <see cref="Value"/> bindable property.
    /// </summary>
    public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(int), typeof(MaterialRating), defaultBindingMode: BindingMode.TwoWay, defaultValue: -1, propertyChanged: (bindableObject, _, newValue) =>
    {
        if (bindableObject is MaterialRating self)
        {
            self.OnValuePropertyChanged(self, newValue);
        }
    });

    #endregion Bindable Properties

    #region Properties

    /// <summary>
    /// Gets or sets the <see cref="Label" /> for the label. This is a bindable property.
    /// </summary>
    public string Label
    {
        get { return (string)GetValue(LabelProperty); }
        set { SetValue(LabelProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="LabelColor" /> for the text of the label. This is a bindable property.
    /// </summary>
    public Color LabelColor
    {
        get { return (Color)GetValue(LabelColorProperty); }
        set { SetValue(LabelColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the font family for the label. This is a bindable property.
    /// </summary>
    public string FontFamily
    {
        get { return (string)GetValue(FontFamilyProperty); }
        set { SetValue(FontFamilyProperty, value); }
    }

    /// <summary>
    /// Gets or sets the spacing between characters of the label. This is a bindable property.
    /// </summary>
    public double CharacterSpacing
    {
        get { return (double)GetValue(CharacterSpacingProperty); }
        set { SetValue(CharacterSpacingProperty, value); }
    }

    /// <summary>
    /// Gets or sets the text style of the label. This is a bindable property.
    /// </summary>
    public FontAttributes FontAttributes
    {
        get { return (FontAttributes)GetValue(FontAttributesProperty); }
        set { SetValue(FontAttributesProperty, value); }
    }

    /// <summary>
    /// Defines whether an app's UI reflects text scaling preferences set in the operating system. The default value of this property is true
    /// </summary>
    public bool FontAutoScalingEnabled
    {
        get { return (bool)GetValue(FontAutoScalingEnabledProperty); }
        set { SetValue(FontAutoScalingEnabledProperty, value); }
    }

    /// <summary>
    /// Defines the font size of the label. This is a bindable property.
    /// </summary>
    public double FontSize
    {
        get { return (double)GetValue(FontSizeProperty); }
        set { SetValue(FontSizeProperty, value); }
    }

    /// <summary>
    /// Defines the casing of the label. This is a bindable property.
    /// </summary>
    public TextTransform LabelTransform
    {
        get { return (TextTransform)GetValue(LabelTransformProperty); }
        set { SetValue(LabelTransformProperty, value); }
    }

    /// <summary>
    /// Gets or sets <see cref="IsEnabled" />  for the rating control. This is a bindable property.
    /// </summary>
    public new bool IsEnabled
    {
        get { return (bool)GetValue(IsEnabledProperty); }
        set { SetValue(IsEnabledProperty, value); }
    }

    /// <summary>
    /// Gets or sets an animation to be executed when an icon is clicked
    /// The default value is <see cref="AnimationTypes.Fade"/>.
    /// This is a bindable property.
    /// </summary>
    public AnimationTypes Animation
    {
        get => (AnimationTypes)GetValue(AnimationProperty);
        set => SetValue(AnimationProperty, value);
    }

#nullable enable
    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Animation"/> property.
    /// The default value is <see langword="null"/>.
    /// This is a bindable property.
    /// </summary>
    public double? AnimationParameter
    {
        get => (double?)GetValue(AnimationParameterProperty);
        set => SetValue(AnimationParameterProperty, value);
    }
#nullable disable

    /// <summary>
    /// Gets or sets a custom animation to be executed when a icon is clicked.
    /// The default value is <see langword="null"/>.
    /// This is a bindable property.
    /// </summary>
    public ICustomAnimation CustomAnimation
    {
        get => (ICustomAnimation)GetValue(CustomAnimationProperty);
        set => SetValue(CustomAnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the <see cref="Color" /> for the stroke of default start. This is a bindable property.
    /// </summary>
    public Color StrokeColor
    {
        get { return (Color)GetValue(StrokeColorProperty); }
        set { SetValue(StrokeColorProperty, value); }
    }

    /// <summary>
    /// Gets or sets the <see cref="StrokeThickness" /> for the default start. This is a bindable property.
    /// </summary>
    public double StrokeThickness
    {
        get { return (double)GetValue(StrokeThicknessProperty); }
        set { SetValue(StrokeThicknessProperty, value); }
    }

    /// <summary>
    /// Allows you to display a bitmap image on the rating when is selected. This is a bindable property.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="ImageButton"/>.</remarks>
    public ImageSource SelectedIconSource
    {
        get => (ImageSource)GetValue(SelectedIconSourceProperty);
        set => SetValue(SelectedIconSourceProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image on the rating when is unselected. This is a bindable property.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="ImageButton"/>.</remarks>
    public ImageSource UnselectedIconSource
    {
        get => (ImageSource)GetValue(UnselectedIconSourceProperty);
        set => SetValue(UnselectedIconSourceProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image diferent on each rating when is selected. This is a bindable property.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="ImageButton"/>.</remarks>
    public IEnumerable<ImageSource> SelectedIconsSource
    {
        get => (IEnumerable<ImageSource>)GetValue(SelectedIconsSourceProperty);
        set => SetValue(SelectedIconsSourceProperty, value);
    }

    /// <summary>
    /// Allows you to display a bitmap image diferent on each rating when is unselected. This is a bindable property.
    /// </summary>
    /// <remarks>For more options have a look at <see cref="ImageButton"/>.</remarks>
    public IEnumerable<ImageSource> UnselectedIconsSource
    {
        get => (IEnumerable<ImageSource>)GetValue(UnselectedIconsSourceProperty);
        set => SetValue(UnselectedIconsSourceProperty, value);
    }

    /// <summary>
    /// Gets or sets <see cref="UseSameIcon" />  for the rating control. This is a bindable property.
    /// </summary>
    public bool UseSameIcon
    {
        get { return (bool)GetValue(UseSameIconProperty); }
        set { SetValue(UseSameIconProperty, value); }
    }

    /// <summary>
    /// Defines the quantity of items on the rating
    /// The default value is <value>5</value>.
    /// This is a bindable property.
    /// </summary>
    public int ItemsSize
    {
        get { return (int)GetValue(ItemsSizeProperty); }
        set { SetValue(ItemsSizeProperty, value); }
    }

    /// <summary>
    /// Defines the quantity of items per row on the rating
    /// The default value is <value>5</value>.
    /// This is a bindable property.
    /// </summary>
    public int ItemsPerRow
    {
        get { return (int)GetValue(ItemsPerRowProperty); }
        set { SetValue(ItemsPerRowProperty, value); }
    }

    /// <summary>
    /// Defines the value of the Rating
    /// The default value is <value>-1</value>.
    /// This is a bindable property.
    /// </summary>
    public int Value
    {
        get { return (int)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }

    #endregion Properties

    #region Constructors

    public MaterialRating()
    {
        _mainLayout = new()
        {
            Margin = new Thickness(0),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Fill,
            RowDefinitions = new()
            {
                new()
                {
                    Height =  GridLength.Auto
                },
                new()
                {
                    Height = GridLength.Auto
                }
            },
            ColumnDefinitions = new()
            {
                new()
                {
                    Width = GridLength.Star
                }
            }
        };

        _label = new()
        {
            TextColor = LabelColor,
            Text = Label,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
        };
        _label.SetValue(Grid.RowProperty, 0);

        _label.SetBinding(MaterialLabel.TextProperty, new Binding(nameof(Label), source: this));
        _label.SetBinding(MaterialLabel.TextColorProperty, new Binding(nameof(LabelColor), source: this));
        _label.SetBinding(MaterialLabel.FontFamilyProperty, new Binding(nameof(FontFamily), source: this));
        _label.SetBinding(MaterialLabel.CharacterSpacingProperty, new Binding(nameof(CharacterSpacing), source: this));
        _label.SetBinding(MaterialLabel.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        _label.SetBinding(MaterialLabel.FontAutoScalingEnabledProperty, new Binding(nameof(FontAutoScalingEnabled), source: this));
        _label.SetBinding(MaterialLabel.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        _label.SetBinding(MaterialLabel.TextTransformProperty, new Binding(nameof(LabelTransform), source: this));
        _label.SetBinding(MaterialLabel.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));

        _mainLayout.Children.Add(_label);

        _containerLayout = new()
        {
            Margin = new Thickness(0, 5, 0, 0),
            Padding = new Thickness(0),
            ColumnSpacing = 0,
            RowSpacing = 0,
            HorizontalOptions = LayoutOptions.Fill
        };

        _containerLayout.SetBinding(Grid.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));
        _containerLayout.SetValue(Grid.RowProperty, 1);

        _mainLayout.Children.Add(_containerLayout);

        SetGridContent();

        base.Content = _mainLayout;
    }

    #endregion Constructors

    #region Methods

    public void OnValuePropertyChanged(MaterialRating control, object newValue)
    {
        if (newValue is not null && int.TryParse(newValue.ToString(), out int value))
        {
            for(int position = 0; position < control._containerLayout.Children.Count; position++)
            {
                var item = control._containerLayout.Children[position];
                SetIconsRatingControl(item, value, position);
            }
        }
    }

    /// <summary>
    /// Set the content of the container of the ratings
    /// </summary>
    private void SetGridContent()
    {
        var useDefaultIcon = GetIfUseDefaultIcon();

        var itemsSize = ItemsSize;
        var useCustomIconsForEachRating = SelectedIconsSource is not null && UnselectedIconsSource is not null;
        if (useCustomIconsForEachRating) 
        {
            itemsSize = Math.Min(SelectedIconsSource.Count(), UnselectedIconsSource.Count());
        }

        int rows = (int)Math.Ceiling(itemsSize * 1.0 / ItemsPerRow * 1.0);
        var itemsPerRow = ItemsPerRow;
        if (rows == 1 && useCustomIconsForEachRating)
        {
            itemsPerRow = Math.Min(SelectedIconsSource.Count(), UnselectedIconsSource.Count());
        }

        _containerLayout.RowDefinitions = new RowDefinitionCollection();
        _containerLayout.ColumnDefinitions = new ColumnDefinitionCollection();
        _containerLayout.Children.Clear();

        AddRowsDefinitions(rows);
        AddColumnsDefinitions(itemsPerRow);
        PupulateGrid(rows, itemsPerRow, itemsSize, useDefaultIcon);
    }

    /// <summary>
    /// Add all rating icons
    /// </summary>
    /// <param name="rows"></param>
    /// <param name="columns"></param>
    /// <param name="itemsSize"></param>
    /// <param name="useDefaultIcon"></param>

    private void PupulateGrid(int rows, int columns, int itemsSize, bool useDefaultIcon)
    {
        int populatedObjects = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                if (populatedObjects == itemsSize)
                    return;

                int value = populatedObjects;

                ++populatedObjects;

                if (!useDefaultIcon)
                {
                    AddMaterialIconAsRating(i, j, value, populatedObjects);
                }
                else
                {
                    AddCustomGridAsRating(i, j, value);
                }
            }
        }
    }

    /// <summary>
    /// Add <see cref="CustomGrid" /> as rating icon
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <param name="populatedObjects"></param>
    private void AddCustomGridAsRating(int row, int column, int value)
    {
        // Add element at row,column position on grid
        var customGrid = new CustomGrid()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Command = new Command((e) => OnTapped((int)(e))),
            CommandParameter = value + 1,
            Animation = Animation,
            Padding = 4,
            WidthRequest = 50,
            HeightRequest = 50,
            Margin = new Thickness(3),
        };

        if (AnimationParameter.HasValue)
            customGrid.AnimationParameter = AnimationParameter;

        customGrid.SetValue(Grid.RowProperty, row);
        customGrid.SetValue(Grid.ColumnProperty, column);

        customGrid.SetBinding(Grid.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));

        SetIconsRatingControl(customGrid, this.Value);

        _containerLayout.Children.Add(customGrid);
    }

    /// <summary>
    /// Add <see cref="MaterialIconButton" /> as rating icon
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <param name="value"></param>
    /// <param name="populatedObjects">this is used to know the position of the icon</param>
    private void AddMaterialIconAsRating(int row, int column, int value, int populatedObjects)
    {
        // Add element at row,column position on grid
        var customImageButton = new MaterialIconButton()
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Type = MaterialIconButtonType.Standard,
            IsVisible = true,
            Command = new Command((e) => OnTapped((int)(e))),
            CommandParameter = value + 1,
            Animation = Animation,
            Padding = 4,
            Margin = new Thickness(3)
        };

        if (AnimationParameter.HasValue)
            customImageButton.AnimationParameter = AnimationParameter;

        customImageButton.SetValue(Grid.RowProperty, row);
        customImageButton.SetValue(Grid.ColumnProperty, column);

        customImageButton.SetBinding(MaterialIconButton.IsEnabledProperty, new Binding(nameof(IsEnabled), source: this));

        SetIconsRatingControl(customImageButton, this.Value, populatedObjects - 1);

        _containerLayout.Children.Add(customImageButton);
    }

    /// <summary>
    /// Add row definitions for rating container
    /// </summary>
    private void AddRowsDefinitions(int rows)
    {
        // Set row definitions of grid
        for (int i = 0; i < rows; i++)
            _containerLayout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
    }

    /// <summary>
    /// Add columns definitions for rating container
    /// </summary>
    private void AddColumnsDefinitions(int columns)
    {
        // Set columns definitions of grid
        for (int i = 0; i < columns; i++)
            _containerLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(ItemsPerRow / 100.0, GridUnitType.Star) });
    }

    /// <summary>
    /// Get if use the default icon, a star. 
    /// </summary>
    /// <returns>true if it should draw star; otherwise, false</returns>
    private bool GetIfUseDefaultIcon()
    {
        return (UseSameIcon && SelectedIconSource is null) ||
                (UseSameIcon && UnselectedIconSource is null) ||
                (!UseSameIcon && SelectedIconsSource is null) ||
                (!UseSameIcon && UnselectedIconsSource is null);
    }

    /// <summary>
    /// Event to set the Value
    /// </summary>
    /// <param name="value">new value</param>
    private void OnTapped(int value)
    {
        if (IsEnabled)
        {
            if (Value == 1 && value == 1)
                Value = -1;
            else
                Value = value;
        }
    }

    /// <summary>
    /// Set Image of the rating depeding of the value and item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="value">rating value</param>
    /// <param name="position">position of rating</param>
    public void SetIconsRatingControl(object item, int value, int? position = null)
    {
        if (item is MaterialIconButton iconButton)
        {
            UpdateIconButtonAppearance(iconButton, value, position!.Value);
        }
        else if (item is CustomGrid gridContainer)
        {
            UpdateGridContainerAppearance(gridContainer, value);
        }
    }

    /// <summary>
    /// Set image of the icon container
    /// </summary>
    /// <param name="iconButton">icon container</param>
    /// <param name="value">icon value</param>
    /// <param name="position">is used to determinate the position of the icon</param>
    private void UpdateIconButtonAppearance(MaterialIconButton iconButton, int value, int position)
    {
        bool isSelected = iconButton.CommandParameter != null && (int)iconButton.CommandParameter <= value;

        if (isSelected)
        {
            if (UseSameIcon && SelectedIconSource != null)
            {
                iconButton.ImageSource = SelectedIconSource;
            }
            else
            {
                var selectedIcon = SelectedIconsSource?.ElementAtOrDefault(position);
                if (selectedIcon != null)
                {
                    iconButton.ImageSource = selectedIcon;
                }
            }
        }
        else
        {
            if (UseSameIcon && UnselectedIconSource != null)
            {
                iconButton.ImageSource = UnselectedIconSource;
            }
            else
            {
                var unselectedIcon = UnselectedIconsSource?.ElementAtOrDefault(position);
                if (unselectedIcon != null)
                {
                    iconButton.ImageSource = unselectedIcon;
                }
            }
        }
    }

    /// <summary>
    /// Add a star to container with a value. If is selected, draws a filled start; otherwise, outline star
    /// </summary>
    /// <param name="gridContainer">star container</param>
    /// <param name="value">star value</param>
    private void UpdateGridContainerAppearance(CustomGrid gridContainer, int value)
    {
        gridContainer.Children.Clear();
        double size = Math.Min(gridContainer.Width, gridContainer.Height) - 10;
        if (size < 1)
            size = 40;

        bool isSelected = gridContainer.CommandParameter != null && (int)gridContainer.CommandParameter <= value;
        var starPath = new Path
        {
            Data = CreateStarPathGeometry(size, size),
            Fill = isSelected ? new SolidColorBrush(StrokeColor) : null,
            Stroke = new SolidColorBrush(StrokeColor),
            StrokeThickness = StrokeThickness,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };

        gridContainer.Children.Add(starPath);
    }

    /// <summary>
    /// Used to draw a star
    /// </summary>
    /// <returns>Path geometry of the star</returns>
    private PathGeometry CreateStarPathGeometry(double width, double height)
    {
        // Define the points of the star based on the width and height
        double centerX = width / 2;
        double centerY = height / 2;
        double radius = Math.Min(centerX, centerY);

        var pathFigure = new PathFigure { StartPoint = new Point(centerX, centerY - radius) };

        for (int i = 1; i < 10; i++)
        {
            double angle = Math.PI / 5 * i;
            double x = centerX + radius * Math.Sin(angle) * (i % 2 == 0 ? 1 : 0.5);
            double y = centerY - radius * Math.Cos(angle) * (i % 2 == 0 ? 1 : 0.5);
            pathFigure.Segments.Add(new LineSegment { Point = new Point(x, y) });
        }

        // Close the figure
        pathFigure.IsClosed = true;

        var pathGeometry = new PathGeometry();
        pathGeometry.Figures.Add(pathFigure);
        return pathGeometry;
    }

    //We override this method to execute SetGridContent when some property of rating changed that is used to populate the rating icons
    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        switch (propertyName)
        {
            case nameof(ItemsSize):
            case nameof(ItemsPerRow):
            case nameof(UnselectedIconSource):
            case nameof(SelectedIconSource):
            case nameof(AnimationParameter):
            case nameof(Animation):
            case nameof(UseSameIcon):
            case nameof(SelectedIconsSource):
            case nameof(UnselectedIconsSource):
                SetGridContent();
                break;
            default:
                base.OnPropertyChanged(propertyName);
                break;
        }
    }

    #endregion Methods
}