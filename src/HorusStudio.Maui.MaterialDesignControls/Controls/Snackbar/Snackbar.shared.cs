using HorusStudio.Maui.MaterialDesignControls.Interface;

namespace HorusStudio.Maui.MaterialDesignControls;

public partial class Snackbar : ISnackbar
{
    static readonly WeakEventManager weakEventManager = new();

	bool isDisposed;
	private ISnackbar _snackbarImplementation;
	readonly string _textLabel = string.Empty;
	readonly string _textAction = string.Empty;
	readonly string _leading = string.Empty;
	readonly string _trailing = string.Empty;

	/// <summary>
	/// Initializes a new instance of <see cref="Snackbar"/>
	/// </summary>
	public Snackbar(SnackbarOptions options)
	{
#if WINDOWS
		if (!Options.ShouldEnableSnackbarOnWindows)
		{
			throw new InvalidOperationException($"Additional setup is required in the Package.appxmanifest file to enable {nameof(Snackbar)} on Windows. Additonally, `{nameof(AppBuilderExtensions.UseMauiCommunityToolkit)}(options => options.{nameof(Options.SetShouldEnableSnackbarOnWindows)}({bool.TrueString.ToLower()});` must be called to enable Snackbar on Windows. See the Platform Specific Initialization section of the {nameof(Snackbar)} documentaion for more information: https://learn.microsoft.com/dotnet/communitytoolkit/maui/alerts/snackbar")
			{
				HelpLink = "https://learn.microsoft.com/dotnet/communitytoolkit/maui/alerts/snackbar"
			};
		}
#endif

		Duration = GetDefaultTimeSpan();
		SnackbarOptions = options;
	}
	
	public static bool IsShown { get; private set; }
	
	public string Text
	{
		get => _textLabel;
		init => _textLabel = value ?? string.Empty;
	}
	
	public string TextAction
	{
		get => _textAction;
		init => _textAction = value ?? string.Empty;
	}
	
	public string Leading
	{
		get => _leading;
		init => _leading = value ?? string.Empty;
	}
	
	public string Trailing
	{
		get => _trailing;
		init => _trailing = value ?? string.Empty;
	}
	
	public TimeSpan Duration { get; init; }

	public SnackbarOptions SnackbarOptions { get;}

	public Action? ActionText { get; init; }
	
	public Action? ActionLeading { get; init; }
	
	public Action? ActionTrailing { get; init; }
	
	public IView? Anchor { get; init; }
	
	public static event EventHandler Shown
	{
		add => weakEventManager.AddEventHandler(value);
		remove => weakEventManager.RemoveEventHandler(value);
	}
	
	public static event EventHandler Dismissed
	{
		add => weakEventManager.AddEventHandler(value);
		remove => weakEventManager.RemoveEventHandler(value);
	}

	/// <summary>
	/// Create new Snackbar
	/// </summary>
	/// <param name="message">Snackbar message</param>
	/// <param name="actionButtonText">Snackbar action button text</param>
	/// <param name="duration">Snackbar duration</param>
	/// <param name="action">Snackbar action</param>
	/// <param name="visualOptions">Snackbar visual options</param>
	/// <param name="anchor">Snackbar anchor</param>
	/// <returns>New instance of Snackbar</returns>
	public static ISnackbar Make(
		MaterialSnackbar snackbar, 
		Action? actionText = null, 
		Action? actionLeading = null, 
		Action? actionTrailing = null,
		TimeSpan? duration = null)
	{
		var Options = GetOption(snackbar);
		return new Snackbar(Options)
		{
			Leading = Options.LeadingImage?.ToString(),
			Trailing = Options.TrailingImage?.ToString(),
			Text = Options.Text,
			ActionText = actionText,
			ActionLeading = actionLeading,
			ActionTrailing = actionTrailing,
			Duration = duration ?? GetDefaultTimeSpan(),
			TextAction = Options.ActionButtonText
		};
	}

	/// <summary>
	/// Dispose Snackbar
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Show Snackbar
	/// </summary>
	public virtual Task Show(CancellationToken token = default) => ShowPlatform(token);

	/// <summary>
	/// Dismiss Snackbar
	/// </summary>
	public virtual Task Dismiss(CancellationToken token = default) => DismissPlatform(token);

	internal static TimeSpan GetDefaultTimeSpan() => TimeSpan.FromSeconds(3);

	void OnShown()
	{
		IsShown = true;
		weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(Shown));
	}

	void OnDismissed()
	{
		IsShown = false;
		weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(Dismissed));
	}
	
	public static SnackbarOptions GetOption(MaterialSnackbar snackbar)
	{
		return new SnackbarOptions()
		{
			Text = snackbar.Text,
			Font = Microsoft.Maui.Font.SystemFontOfSize(snackbar.FontSize),
			TextColor = snackbar.TextColor,
			ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(snackbar.ActionFontSize),
			ActionButtonTextColor = snackbar.ActionTextColor,
			ActionButtonText = snackbar.ActionText,
			LeadingImage = snackbar.LeadingIcon,
			TrailingImage = snackbar.TrailingIcon,
			BackgroundColor = snackbar.BackgroundColor,
			CornerRadius = snackbar.CornerRadius
		};
	}
}