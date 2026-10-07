using System;
using UnityEngine;

namespace Core
{
    [Flags]
    public enum ELogMessageType
    {
        None = 0,
        Info = 1 << 0,
        Warning = 1 << 1,
        Error = 1 << 2,
    }
    [Flags]
    public enum ELogType
    {
        None = 0,
        UI = 1 << 0,
        Addressables = 1 << 1,
        Test = 1 << 2,
    }

    public static class LogManager
    {
        private static ELogMessageType showingMessageType = ELogMessageType.None;
        private static ELogCategory showingCategory = ELogCategory.None;

        // 로그를 볼 항목으로 선택한 것들만 로그 확인
        // 에디터에서만 로그 발생하도록
        public static void Log(string message, ELogType logType = ELogType.None, ELogCategory category = ELogCategory.None, ELogMessageType messageType = ELogMessageType.Info)
        {
#if UNITY_EDITOR
            if (IsLogEnabled(messageType, category) == false)
                return;

            string logMessage = $"[{logType}][{category}] {message}";
            switch (messageType)
            {
                case ELogMessageType.Info:
                    Debug.Log(logMessage);
                    break;
                case ELogMessageType.Warning:
                    Debug.LogWarning(logMessage);
                    break;
                case ELogMessageType.Error:
                    Debug.LogError(logMessage);
                    break;
                default:
                    Debug.Log(logMessage);
                    break;
            }
#endif
        }

        public static void SetLogFilter(ELogMessageType messageType, ELogCategory category)
        {
            showingMessageType = messageType;
            showingCategory = category;
        }

        public static bool IsLogEnabled(ELogMessageType messageType, ELogCategory category)
        {
            return (showingMessageType & messageType) != 0 && (showingCategory & category) != 0;
        }
    }
}
