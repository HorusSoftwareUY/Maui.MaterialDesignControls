using System.Windows.Input;
using HorusStudio.Maui.MaterialDesignControls.Behaviors;

namespace HorusStudio.Maui.MaterialDesignControls
{
    /// <summary>
    /// It is a touchable border view.
    /// </summary>
    internal class BorderButton : Border, ITouchableView
    {
        #region Attributes

        private static readonly BindableProperty.CreateDefaultValueDelegate DefaultTouchAnimationType = _ => MaterialAnimation.TouchAnimationType;

        #endregion Attributes

        #region Bindable properties

        /// <summary>
        /// The backing store for the <see cref="Command">Command</see> bindable property.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(BorderButton), propertyChanged:
            (bindable, _, _) =>
            {
                var self = (BorderButton)bindable;
                self.UpdateTouchBehavior();
            });

        /// <summary>
        /// The backing store for the <see cref="CommandParameter">CommandParameter</see> bindable property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(BorderButton));

        /// <summary>
        /// The backing store for the <see cref="TouchAnimationType">TouchAnimationType</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TouchAnimationTypeProperty = BindableProperty.Create(nameof(TouchAnimationType), typeof(TouchAnimationTypes), typeof(BorderButton), defaultValueCreator: DefaultTouchAnimationType);

        /// <summary>
        /// The backing store for the <see cref="TouchAnimation">TouchAnimation</see> bindable property.
        /// </summary>
        public static readonly BindableProperty TouchAnimationProperty = BindableProperty.Create(nameof(TouchAnimation), typeof(ITouchAnimation), typeof(BorderButton));

        #endregion Bindable properties

        #region Properties

        /// <summary>
        /// Gets or sets the command to invoke when the button is activated.
        /// This is a bindable property.
        /// </summary>
        /// <remarks>
        /// This property is used to associate a command with an instance of a button.
        /// <para>This property is most often set in the MVVM pattern to bind callbacks back into the ViewModel. <see cref="VisualElement.IsEnabled">VisualElement.IsEnabled</see> is controlled by the <see cref="Command.CanExecute(object)">Command.CanExecute(object)</see> if set.</para>
        /// </remarks>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the parameter to pass to the <see cref="Command">Command</see> property.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see langword="null">Null</see>.
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
        /// <see cref="TouchAnimationTypes.Fade">TouchAnimationTypes.Fade</see>
        /// </default>
        public TouchAnimationTypes TouchAnimationType
        {
            get => (TouchAnimationTypes)GetValue(TouchAnimationTypeProperty);
            set => SetValue(TouchAnimationTypeProperty, value);
        }

        /// <summary>
        /// Gets or sets a custom animation to be executed when a icon is clicked.
        /// This is a bindable property.
        /// </summary>
        /// <default>
        /// <see langword="null">Null</see>.
        /// </default>
        public ITouchAnimation TouchAnimation
        {
            get => (ITouchAnimation)GetValue(TouchAnimationProperty);
            set => SetValue(TouchAnimationProperty, value);
        }

        #endregion Properties

        #region Events

        private EventHandler? _clicked;
        private EventHandler? _pressed;
        private EventHandler? _released;
        private EventHandler<Behaviors.TouchEventArgs>? _touch;
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
        
        /// <summary>
        /// Occurs when the border button is touched.
        /// </summary>
        public event EventHandler<Behaviors.TouchEventArgs>? Touch
        {
            add
            {
                lock (_objectLock)
                {
                    _touch += value;
                    UpdateTouchBehavior();
                }
            }
            remove
            {
                lock (_objectLock)
                {
                    _touch -= value;
                    UpdateTouchBehavior();
                }
            }
        }

        #endregion Events

        #region ITouchable

        public async void OnTouch(TouchEventType gestureType)
        {
            Utils.Logger.Debug($"Gesture: {gestureType}");

            if (!IsEnabled) return;
            await TouchAnimationManager.AnimateAsync(this, gestureType);
            
            _touch?.Invoke(this, new Behaviors.TouchEventArgs(gestureType));

            switch (gestureType)
            {
                case TouchEventType.Pressed:
                    _pressed?.Invoke(this, EventArgs.Empty);
                    break;

                case TouchEventType.Released:
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

            if (Command != null || _clicked != null || _pressed != null || _released != null || _touch != null)
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
}