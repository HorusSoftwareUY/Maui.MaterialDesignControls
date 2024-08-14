using CoreGraphics;
using Microsoft.Maui.Platform;
using UIKit;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class Snackbar
{
    static SnackbarView? SnackbarView { get; set; }
    
    /// <summary>
    /// Dispose Snackbar
    /// </summary>
    protected virtual void Dispose(bool isDisposing)
    {
        if (isDisposed)
        {
            return;
        }

        if (isDisposing)
        {
            SnackbarView?.Dispose();
        }

        isDisposed = true;
    }

    /// <summary>
    /// Dismiss Snackbar
    /// </summary>
    static Task DismissPlatform(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        if (SnackbarView is not null)
        {
            SnackbarView.Dismiss();
            SnackbarView = null;
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Show Snackbar
    /// </summary>
    async Task ShowPlatform(CancellationToken token)
    {
        await DismissPlatform(token);
        token.ThrowIfCancellationRequested();

        var cornerRadius = GetCornerRadius(SnackbarOptions.CornerRadius);

        var padding = GetMaximum(cornerRadius.X, cornerRadius.Y, cornerRadius.Width, cornerRadius.Height);
        SnackbarView = new SnackbarView(
            Leading.Replace("File: ", string.Empty), 
            Text,
            Trailing.Replace("File: ", string.Empty),
            SnackbarOptions.BackgroundColor.ToPlatform(),
            cornerRadius,
            SnackbarOptions.TextColor.ToPlatform(),
            UIFont.SystemFontOfSize((nfloat)SnackbarOptions.Font.Size),
            SnackbarOptions.CharacterSpacing,
            TextAction,
            SnackbarOptions.ActionButtonTextColor.ToPlatform(),
            UIFont.SystemFontOfSize((nfloat)SnackbarOptions.ActionButtonFont.Size),
            padding)
        {
            ActionLabel = ActionText,
            ActionLeading = ActionLeading,
            ActionTrailing = ActionTrailing,
            Duration = Duration,
            OnDismissed = OnDismissed,
            OnShown = OnShown
        };

        SnackbarView.Show();

        static T? GetMaximum<T>(params T[] items) => items.Max();
    }

    static CGRect GetCornerRadius(CornerRadius cornerRadius)
    {
        return new CGRect(cornerRadius.BottomLeft, cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomRight);
    }
}