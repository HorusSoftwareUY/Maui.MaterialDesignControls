using Firebase.Crashlytics;
using HorusStudio.Maui.MaterialDesignControls.Sample.Services;
using HorusStudio.Maui.MaterialDesignControls.Sample.Utils;

namespace HorusStudio.Maui.MaterialDesignControls.Sample.iOS.Services;

public class CrashlyticsService : ICrashlyticsService
{
    public void InitCrashDetection()
    {
        TaskScheduler.UnobservedTaskException += (sender, e) =>
        {
            LogException(e.Exception, new Dictionary<string, string> { {"CrashDetectionMethod", "TaskScheduler.UnobservedTaskException"} });
            e.SetObserved();
        };
    }
    
    public void LogException(Exception exception, Dictionary<string, string>? properties = null)
    {
       try
       {
           Crashlytics.SharedInstance.Log($"ExceptionSource: {exception.Source}");
                
           if (properties != null && properties.Any())
           {
               foreach (var property in properties)
               {
                   try
                   {
                       Crashlytics.SharedInstance.Log($"ExceptionProperty {property.Key}: {property.Value}");
                   }
                   catch (Exception ex)
                   {
                       Logger.LogInfo($"Error logging exception log on firebase: {ex.Message}");
                   }
               }
           }
           
           var exceptionModel = new ExceptionModel(exception.GetType().Name, exception.Message);
           exceptionModel.StackTrace = GetStackFrames(exception).ToArray();
           Crashlytics.SharedInstance.RecordExceptionModel(exceptionModel);
           Crashlytics.SharedInstance.SendUnsentReports();
       }
       catch (Exception ex)
       {
           Logger.LogInfo($"Error logging event on firebase: {ex.Message}");
       }
    }
    
    private List<StackFrame> GetStackFrames(Exception ex)
    {
        var result = new List<StackFrame>();

        try
        {
            if (ex != null && ex.StackTrace != null)
            {
                var stackTraceLines = ex.StackTrace.Split('\n').ToList();
                if (stackTraceLines != null && stackTraceLines.Any())
                {
                    foreach (var stackTraceLine in stackTraceLines)
                    {
                        try
                        {
                            var firstSplit = stackTraceLine.Split(new string[] { " in " }, StringSplitOptions.None);
                            var secondSplit = firstSplit[1].Split(new string[] { ".cs:" }, StringSplitOptions.None);
                            var lineNumberParse = int.TryParse(secondSplit[1], out int lineNumber);
                            result.Add(new Firebase.Crashlytics.StackFrame(firstSplit[0], $"{secondSplit[0]}.cs",
                                lineNumberParse ? lineNumber : -1));
                        }
                        catch (Exception stackFrameEx)
                        {
                            Logger.LogInfo($"Error getting stack frame: {stackFrameEx.Message}");
                        }
                    }
                }
            }
        }
        catch (Exception stackFramesEx)
        {
            Logger.LogInfo($"Error getting stack frames: {stackFramesEx.Message}");
        }

        return result;
    }
}