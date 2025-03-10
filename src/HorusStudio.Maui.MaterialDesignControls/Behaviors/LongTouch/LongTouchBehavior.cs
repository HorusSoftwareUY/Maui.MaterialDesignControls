using System.Windows.Input;

namespace HorusStudio.Maui.MaterialDesignControls.Behaviors;

/// <summary>
/// <see cref="Behavior"/> that handles long press (or long click) event on underlaying <see cref="View" />
/// </summary>
public partial class LongTouchBehavior : PlatformBehavior<View>
{
    #region Bindable Properties

    /// <summary>
    /// The backing store for the <see cref="Command" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LongTouchBehavior));

    /// <summary>
    /// The backing store for the <see cref="CommandParameter" /> bindable property.
    /// </summary>
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(LongTouchBehavior));

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the <see cref="ICommand" /> to be executed once long touch is detected over the element.
    /// This is a bindable property.
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Gets or sets a parameter to be passed to <see cref="Command" />.
    /// This is a bindable property.
    /// </summary>
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    #endregion

    #region Methods

    private void OnViewLongClick()
    {
        if (Command != null && Command.CanExecute(CommandParameter))
        {
            Command.Execute(CommandParameter);
        }
    }

    #endregion

}