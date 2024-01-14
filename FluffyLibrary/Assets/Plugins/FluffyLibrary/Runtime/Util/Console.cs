using UnityEngine;

namespace FluffyLibrary.Util
{
    public class Console
    {
        private const bool Print = true;
        private const string LibraryName = "<color=#ffdd00>[FluffyConsole]</color>";
        private const string LabelColor = "<color=#00aaff>[{0}]</color>";

        private static string LabelName(string label)
        {
            return string.Format(LabelColor, label);
        }

        public static void Log(object message)
        {
            if (Print)
            {
                Debug.Log($"{LibraryName} {message}");
            }
        }

        public static void Log(string label, object message)
        {
            if (Print)
            {
                Debug.Log($"{LibraryName} {LabelName(label)} {message}");
            }
        }

        public static void LogWarning(object message)
        {
            if (Print)
            {
                Debug.LogWarning($"{LibraryName} {message}");
            }
        }

        public static void LogWarning(string label, object message)
        {
            if (Print)
            {
                Debug.LogWarning($"{LibraryName} {LabelName(label)} {message}");
            }
        }

        public static void LogError(object message)
        {
            if (Print)
            {
                Debug.LogError($"{LibraryName} {message}");
            }
        }

        public static void LogError(string label, object message)
        {
            if (Print)
            {
                Debug.LogError($"{LibraryName} {LabelName(label)} {message}");
            }
        }
    }
}