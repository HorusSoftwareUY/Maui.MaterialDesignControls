using HorusStudio.Maui.MaterialDesignControls.Behaviors;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls;

/// <summary>
/// It is a touchable view.
/// </summary>
public class MaterialViewButton : ContentView, ITouchable
{
    #region Attributes

    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultAnimationType = _ => MaterialAnimation.Type;
    private static readonly BindableProperty.CreateDefaultValueDelegate DefaultAnimationParameter = _ => MaterialAnimation.Parameter;

    #endregion Attributes

    #region Bindable properties

    /// <summary>
    /// The backing store for the <see cref="Command" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MaterialViewButton), propertyChanged:
        (bindable, _, _) =>
        {
            var self = (MaterialViewButton)bindable;
            self.UpdateTouchBehavior();
        });

    /// <summary>
    /// The backing store for the <see cref="CommandParameter" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MaterialViewButton));


    /// <summary>
    /// The backing store for the <see cref="Animation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationProperty = BindableProperty.Create(nameof(Animation), typeof(AnimationTypes), typeof(MaterialViewButton), defaultValueCreator: DefaultAnimationType);

    /// <summary>
    /// The backing store for the <see cref="AnimationParameter"/> bindable property.
    /// </summary>
    public static readonly BindableProperty AnimationParameterProperty = BindableProperty.Create(nameof(AnimationParameter), typeof(double?), typeof(MaterialViewButton), defaultValueCreator: DefaultAnimationParameter);

    /// <summary>
    /// The backing store for the <see cref="CustomAnimation"/> bindable property.
    /// </summary>
    public static readonly BindableProperty CustomAnimationProperty = BindableProperty.Create(nameof(CustomAnimation), typeof(ICustomAnimation), typeof(MaterialViewButton));

    #endregion Bindable properties

    #region Properties

    /// <summary>
    /// Gets or sets the command to invoke when the button is activated.
    /// This is a bindable property.
    /// </summary>
    /// <remarks>
    /// This property is used to associate a command with an instance of a button.
    /// <para>This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel. <see cref="VisualElement.IsEnabled" /> is controlled by the <see cref="Command.CanExecute(object)"/> if set.</para>
    /// </remarks>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Command"/> property.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>.
    /// </default>
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    /// <summary>
    /// Gets or sets an animation to be executed when an icon is clicked
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see cref="AnimationTypes.Fade">AnimationTypes.Fade</see>
    /// </default>
    public AnimationTypes Animation
    {
        get => (AnimationTypes)GetValue(AnimationProperty);
        set => SetValue(AnimationProperty, value);
    }

    /// <summary>
    /// Gets or sets the parameter to pass to the <see cref="Animation"/> property.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>.
    /// </default>
    public double? AnimationParameter
    {
        get => (double?)GetValue(AnimationParameterProperty);
        set => SetValue(AnimationParameterProperty, value);
    }

    /// <summary>
    /// Gets or sets a custom animation to be executed when a icon is clicked.
    /// This is a bindable property.
    /// </summary>
    /// <default>
    /// <see langword="null"/>.
    /// </default>
    public ICustomAnimation CustomAnimation
    {
        get => (ICustomAnimation)GetValue(CustomAnimationProperty);
        set => SetValue(CustomAnimationProperty, value);
    }

    #endregion Properties

    #region Events

    private EventHandler? _clicked;
    private EventHandler? _pressed;
    private EventHandler? _released;
    private readonly object _objectLock = new();

    /// <summary>
    /// Occurs when the card is clicked/tapped.
    /// </summary>
    public event EventHandler Clicked
    {
        add
        {
            lock (_objectLock)
            {
                _clicked += value;
                UpdateTouchBehavior();
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _clicked -= value;
                UpdateTouchBehavior();
            }
        }
    }

    /// <summary>
    /// Occurs when the card is pressed.
    /// </summary>
    public event EventHandler Pressed
    {
        add
        {
            lock (_objectLock)
            {
                _pressed += value;
                UpdateTouchBehavior();
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _pressed -= value;
                UpdateTouchBehavior();
            }
        }
    }

    /// <summary>
    /// Occurs when the card is released.
    /// </summary>
    public event EventHandler Released
    {
        add
        {
            lock (_objectLock)
            {
                _released += value;
                UpdateTouchBehavior();
            }
        }
        remove
        {
            lock (_objectLock)
            {
                _released -= value;
                UpdateTouchBehavior();
            }
        }
    }

    #endregion Events
    
    #region ITouchable

    public async void OnTouch(TouchType gestureType)
    {
        Utils.Logger.Debug($"Gesture: {gestureType}");

        if (!IsEnabled) return;
        await TouchAnimation.AnimateAsync(this, gestureType);
        
        switch (gestureType)
        {
            case TouchType.Pressed:
                _pressed?.Invoke(this, EventArgs.Empty);
                break;

            case TouchType.Released:
                if (Command != null && Command.CanExecute(CommandParameter))
                {
                    Command.Execute(CommandParameter);
                }
                if (_released != null)
                {
                    _released.Invoke(this, EventArgs.Empty);
                }
                else if (_clicked != null)
                {
                    _clicked.Invoke(this, EventArgs.Empty);
                }
                
                break;
        }
    }

    #endregion ITouchable

    #region Methods
    
    private void UpdateTouchBehavior()
    {
        var touchBehavior = Behaviors.FirstOrDefault(b => b is TouchBehavior) as TouchBehavior;
            
        if (Command != null || _clicked != null || _pressed != null || _released != null)
        {
            if (touchBehavior == null)
            {
                Behaviors.Add(new TouchBehavior());
            }
        }
        else if (touchBehavior != null)
        {
            Behaviors.Remove(touchBehavior);
        }
    }

    #endregion Methods
}