using System.IO;
using UnityEngine;

namespace Szn.Framework.UtilPackage
{
    public static class UnityPathTools
    {
        static UnityPathTools()
        {
            switch (Application.platform)
            {
#if UNITY_EDITOR
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.WindowsEditor:
                    switch (UnityEditor.EditorUserBuildSettings.activeBuildTarget)
                    {
                        case UnityEditor.BuildTarget.iOS:
                            _platform = "iOS";
                            break;

                        case UnityEditor.BuildTarget.Android:
                            _platform = "Android";
                            break;
                    }
                    break;
#endif

                case RuntimePlatform.IPhonePlayer:
                    _platform = "iOS";
                    break;

                case RuntimePlatform.Android:
                    _platform = "Android";
                    break;
            }

            _persistentDataPath = Application.persistentDataPath;
            _streamingAssetsPath = Path.Combine(Application.streamingAssetsPath, _platform);
            
#if UNITY_EDITOR
            _streamingAssetsUrl = Path.Combine($"file:///{Application.streamingAssetsPath}", _platform);
#elif UNITY_ANDROID
            _streamingAssetsUrl = Path.Combine(Application.streamingAssetsPath, _platform);
#elif UNITY_IOS
            _streamingAssetsUrl = Path.Combine($"file://{Application.streamingAssetsPath}", _platform);
#endif
        }
        private static readonly string _platform = "Unknown";
        public static string GetPlatform()
        {
            return _platform;
        }

        private static readonly string _streamingAssetsPath;

        public static string GetStreamingAssetsPath(string InSubPath = null)
        {
            if (string.IsNullOrEmpty(InSubPath)) return _streamingAssetsPath;
            return Path.Combine(_streamingAssetsPath, InSubPath);
        }

        private static readonly string _persistentDataPath;

        public static string GetPersistentDataPath(string InSubPath = null)
        {
            if (string.IsNullOrEmpty(InSubPath)) return _persistentDataPath;

            return Path.Combine(_persistentDataPath, InSubPath);
        }

        private static readonly string _streamingAssetsUrl;
        public static string GetStreamingAssetsUrl(string InSubPath = null)
        {
            if (string.IsNullOrEmpty(InSubPath)) return _streamingAssetsUrl;

            return Path.Combine(_streamingAssetsUrl, InSubPath);
        }
    }
}