using CompassMobileUpdate.Helpers;
using CompassMobileUpdate.Services;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CompassMobileUpdate
{
    public static class AppLogger
    {
        public static void LogNewLine()
        {
            Log.AppendLine(" ");
        }
        public static void LogErrorLocallyOnly(Exception ex)
        {
            LogErrorLocally(ex);
        }
        public static void Debug(string message)
        {
            if (AppVariables.IsLogging)
            {
                Log.AppendLine(DateTime.Now.ToLongTimeString() + " - " + message);
            }
        }
        public static void LogError(Exception ex, [CallerMemberName] string callingMethod = "")
        {
            if (AppVariables.IsLogging)
            {
                LogErrorLocally(ex);
            }

            try
            {
                AppVariables.AppService.LogApplicationError(callingMethod, ex);
            }
            catch (Exception e)
            {
                LogErrorLocally(e);
            }
        }

        private static void LogErrorLocally(Exception ex)
        {
            string message = "Message: " + AppHelper.GetAmalgamatedExceptionMessage(ex) + ". StackTrace: " + ex.StackTrace;
            Log.AppendLine(DateTime.Now.ToLongTimeString() + " - " + message);
        }

        public static StringBuilder Log { get; set; } = new StringBuilder();
    }
}
