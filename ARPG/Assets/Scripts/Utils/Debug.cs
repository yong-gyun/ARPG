#if UNITY_EDITOR
#define LOG_ENABLE
#endif

using UnityEngine;
using System.Diagnostics;


public static class Debug
{
    [Conditional("LOG_ENABLE")]
    public static void Log(object message)
    {
        UnityEngine.Debug.Log(message);
    }

    [Conditional("LOG_ENABLE")]
    public static void LogWarning(object message)
    {
        UnityEngine.Debug.LogWarning(message);
    }

    [Conditional("LOG_ENABLE")]
    public static void LogError(object message)
    {
        UnityEngine.Debug.LogError(message);
    }

    [Conditional("LOG_ENABLE")]
    public static void LogFormat(string format, params object[] message)
    {
        UnityEngine.Debug.LogFormat(format, message);
    }

    [Conditional("LOG_ENABLE")]
    public static void LogWarningFormat(string format, params object[] message)
    {
        UnityEngine.Debug.LogWarningFormat(format, message);
    }

    [Conditional("LOG_ENABLE")]
    public static void LogErrorFormat(string format, params object[] message)
    {
        UnityEngine.Debug.LogErrorFormat(format, message);
    }
}
