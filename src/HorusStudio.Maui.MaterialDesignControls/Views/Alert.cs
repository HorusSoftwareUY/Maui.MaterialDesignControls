using Foundation;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls.Views;

public class Alert
{
    NSTimer? timer;

    /// <summary>
    /// Initialize Alert
    /// </summary>
    public Alert()
    {
        FloatView = [];

        FloatView.ParentView.AddSubview(FloatView);
        FloatView.ParentView.BringSubviewToFront(FloatView);
    }

    /// <summary>
    /// Duration of time before <see cref="Alert"/> disappears
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// <see cref="UIView"/> on which Alert will appear. When null, <see cref="Alert"/> will appear at bottom of screen.
    /// </summary>
    public UIView? Anchor { get; set; }

    /// <summary>
    /// Action to execute on popup dismissed
    /// </summary>
    public Action? OnDismissed { get; set; }

    /// <summary>
    /// Action to execute on popup shown
    /// </summary>
    public Action? OnShown { get; set; }

    /// <summary>
    /// <see cref="UIView"/> for <see cref="Alert"/>
    /// </summary>
    protected FloatView FloatView { get; }

    /// <summary>
    /// Dismiss the <see cref="Alert"/> from the screen
    /// </summary>
    public void Dismiss()
    {
        if (timer != null)
        {
            timer.Invalidate();
            timer.Dispose();
            timer = null;
        }

        FloatView.Dismiss();
        OnDismissed?.Invoke();
    }

    /// <summary>
    /// Show the <see cref="Alert"/> on the screen
    /// </summary>
    public void Show()
    {
        FloatView.AnchorView = Anchor;

        FloatView.Setup();

        timer = NSTimer.CreateScheduledTimer(Duration, t =>
        {
            Dismiss();
        });

        OnShown?.Invoke();
    }
}