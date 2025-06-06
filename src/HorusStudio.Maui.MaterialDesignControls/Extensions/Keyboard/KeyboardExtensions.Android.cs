namespace Android.App;

static class KeyboardExtensions
{
    public static void HideKeyboard(this Activity activity, Views.View? view) => activity.RunOnUiThread(() =>
    {
        activity.SafeRunOnUiThread(() =>
        {
            var context = activity.ApplicationContext;
            if (context != null)
            {
                var systemService = context.GetSystemService(Content.Context.InputMethodService);
                if (view != null
                    && systemService != null
                    && systemService is Views.InputMethods.InputMethodManager inputMethodManager)
                {
                    inputMethodManager?.HideSoftInputFromWindow(view.WindowToken, Views.InputMethods.HideSoftInputFlags.None);
                }
            }
        });
    });
}