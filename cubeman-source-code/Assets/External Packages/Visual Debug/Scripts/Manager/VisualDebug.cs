namespace Core.Manager
{
    public static class VisualDebug
    {
        public delegate void LogMessage(string message);
        public delegate void LogMessageWithSeconds(string message, float messageDuration);

        public static event LogMessage OnLogMessage;
        public static event LogMessage OnLogWarningMessage;
        public static event LogMessage OnLogErrorMessage;

        public static event LogMessageWithSeconds OnLogMessageWithSeconds;
        public static event LogMessageWithSeconds OnLogWarningMessageWithSeconds;
        public static event LogMessageWithSeconds OnLogErrorMessageWithSeconds;

        public static void Log(string message)
        {
            if(message != string.Empty)
            {
                OnLogMessage?.Invoke(message);
            }
        }

        public static void Log(string message, float messageDuration)
        {
            if (message != string.Empty && messageDuration > 0)
            {
                OnLogMessageWithSeconds?.Invoke(message, messageDuration);
            }
        }

        public static void LogWarning(string message)
        {
            if (message != string.Empty)
            {
                OnLogWarningMessage?.Invoke(message);
            }
        }

        public static void LogWarning(string message, float messageDuration)
        {
            if (message != string.Empty && messageDuration > 0)
            {
                OnLogWarningMessageWithSeconds?.Invoke(message, messageDuration);
            }
        }

        public static void LogError(string message)
        {
            if (message != string.Empty)
            {
                OnLogErrorMessage?.Invoke(message);
            }
        }

        public static void LogError(string message, float messageDuration)
        {
            if (message != string.Empty && messageDuration > 0)
            {
                OnLogErrorMessageWithSeconds?.Invoke(message, messageDuration);
            }
        }
    }
}
