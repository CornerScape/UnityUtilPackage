using System.IO;
using UnityEngine;

namespace Szn.Framework.UtilPackage
{
    public static class UnityPathTools
    {
        static UnityPathTools()
        {
            string platform;
            switch (Application.platform)
            {
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.WindowsEditor:
#if UNITY_EDITOR
                    switch (UnityEditor.EditorUserBuildSettings.activeBuildTarget)
                    {
                        case UnityEditor.BuildTarget.StandaloneWindows:
                            platform = "Win";
                            break;

                        case UnityEditor.BuildTarget.iOS:
                            platform = "iOS";
                            break;

                        case UnityEditor.BuildTarget.Android:
                            platform = "Android";
                            break;
                        
                        default:
                            platform = "Unknown";
                            break;
                    }
#endif
                    break;

                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.IPhonePlayer:
                    platform = "iOS";
                    break;

                case RuntimePlatform.WindowsPlayer:
                    platform = "Win";
                    break;

                case RuntimePlatform.Android:
                    platform = "Android";
                    break;
                
                default:
                    platform = "Unknown";
                    break;
            }


            _persistentDataPath = Path.Combine(Application.persistentDataPath, platform);
            _streamingAssetsPath = Path.Combine(Application.streamingAssetsPath, platform);
        }

        private static readonly string _streamingAssetsPath;
        public static string GetStreamingAssetsPath(string InSubPath = null)
        {
            if (string.IsNullOrEmpty(InSubPath)) return _streamingAssetsPath;

            return Path.Combine(_streamingAssetsPath, InSubPath);
        }

        private static readonly string _persistentDataPath;
        public static string GetPersistentDataPath (string InSubPath = null)
        {
            if (string.IsNullOrEmpty(InSubPath)) return _persistentDataPath;

            return Path.Combine(_persistentDataPath, InSubPath);
        }
    }

}

