using Microsoft.Maui.Platform;
using Microsoft.Maui.Controls.Platform;

#if IOS
using UIKit;
#endif

#if ANDROID
using AndroidViews = Android.Views;
using AndroidWidget = Android.Widget;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using AndroidX.AppCompat.Widget;
using Android.Graphics.Drawables;
using Android.Views.InputMethods;
#endif

namespace HorusStudio.Maui.MaterialDesignControls.Sample.Handlers
{
	public class CustomMaterialTextFieldHandler : MaterialTextFieldHandler
    {
#if IOS
        protected override void ConnectHandler(MauiTextField platformView)
        {
            base.ConnectHandler(platformView);

            if (VirtualView.Keyboard != Keyboard.Telephone)
                return;

            var toolbar = new UIToolbar
            {
                BarStyle = UIBarStyle.Default,
                Translucent = true,
            };
            toolbar.SizeToFit();

            var doneButton = new UIBarButtonItem("Close keyboard", UIBarButtonItemStyle.Done, (sender, e) =>
            {
                platformView.ResignFirstResponder();
            });

            var flexibleSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            toolbar.SetItems(new[] { flexibleSpace, doneButton }, false);

            platformView.InputAccessoryView = toolbar;
        }
#endif

#if ANDROID
        AndroidViews.View? floatingButton;
        AndroidViews.WindowManagerLayoutParams? layoutParams;
        AndroidViews.IWindowManager? windowManager;
        
        protected override void ConnectHandler(MauiAppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);

            if (VirtualView.Keyboard != Keyboard.Telephone)
                return;

            platformView.FocusChange += OnFocusChanged;

            windowManager = platformView?.Context?.GetSystemService(Context.WindowService).JavaCast<AndroidViews.IWindowManager>();

            layoutParams = new AndroidViews.WindowManagerLayoutParams(
                AndroidViews.ViewGroup.LayoutParams.WrapContent,
                AndroidViews.ViewGroup.LayoutParams.WrapContent,
                AndroidViews.WindowManagerTypes.ApplicationPanel,
                AndroidViews.WindowManagerFlags.NotFocusable | AndroidViews.WindowManagerFlags.NotTouchModal,
                Format.Translucent);

            layoutParams.Gravity = AndroidViews.GravityFlags.Top | AndroidViews.GravityFlags.End;

            if (platformView != null && platformView.Context != null)
            {
                layoutParams.X = ToPixels(platformView.Context, 8);
                layoutParams.Y = ToPixels(platformView.Context, 8);

                CreateFloatingButton(platformView.Context);
            }
        }

        void OnFocusChanged(object sender, AndroidViews.View.FocusChangeEventArgs e)
        {
            if (e.HasFocus)
            {
                ShowFloatingButton();
            }
            else
            {
                RemoveFloatingButton();
            }
        }

        void CreateFloatingButton(Context context)
        {
            var button = new AndroidWidget.Button(context);
            button.Text = "Close keyboard";
            button.SetTextColor(MaterialLightTheme.OnPrimary.ToPlatform());
            button.SetPadding(16, 4, 16, 4);
            var drawable = new GradientDrawable();
            drawable.SetColor(MaterialLightTheme.Primary.ToPlatform());
            drawable.SetCornerRadius(ToPixels(context, 16));
            button.SetBackground(drawable);
            button.Click += (s, e) =>
            {
                var view = VirtualView?.Handler?.PlatformView as AndroidViews.View;
                if (view != null)
                {
                    view.ClearFocus();
                    var imm = (InputMethodManager)context.GetSystemService(Context.InputMethodService);
                    imm?.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
                }

                RemoveFloatingButton();
            };

            floatingButton = button;
        }

        int ToPixels(Context context, int dp)
        {
            return context != null && context.Resources != null && context.Resources.DisplayMetrics != null ?
                (int)(dp * context.Resources.DisplayMetrics.Density): 0;
        }

        void ShowFloatingButton()
        {
            if (floatingButton?.IsAttachedToWindow == false)
            {
                windowManager?.AddView(floatingButton, layoutParams);
            }
        }

        void RemoveFloatingButton()
        {
            if (floatingButton?.IsAttachedToWindow == true)
            {
                windowManager?.RemoveView(floatingButton);
            }
        }
        
        protected override void DisconnectHandler(MauiAppCompatEditText platformView)
        {
            RemoveFloatingButton();
            platformView.FocusChange -= OnFocusChanged;
            base.DisconnectHandler(platformView);
        }
#endif
    }
}