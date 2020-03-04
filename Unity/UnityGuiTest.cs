using System;
using UnityEngine;

namespace Szn.Framework.UtilPackage
{
    public class UnityGuiTest : MonoBehaviour
    {
#if UNIT_TEST
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Init()
        {
            GameObject go = new GameObject("Unit Test");
            DontDestroyOnLoad(go);
            go.AddComponent<UnityGuiTest>();
        }
#endif

        private float width;
        private float height;
        private Rect logWinRect;
        private Rect screenRect;

        public static event Action UnityGUIAction;

        private void Awake()
        {
            width = Screen.width;
            height = Screen.height;
            logWinRect = new Rect(width * .3f, height * .4f, width * .4f, height * .2f);
            screenRect = new Rect(0, 0, width, height);
        }

        private void OnGUI()
        {
            logWinRect = GUI.Window(0, logWinRect, LogWindow, "Unity GUI Window");
        }

        private void LogWindow(int InWindowId)
        {
            GUI.skin.button.fontSize = 32;
            if (GUILayout.Button("Close"))
            {
                DestroyImmediate(gameObject);
            }
            UnityGUIAction?.Invoke();
            GUI.DragWindow(screenRect);
        }
    }
}