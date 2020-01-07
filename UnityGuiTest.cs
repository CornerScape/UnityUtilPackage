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

        public static Action UnityGUIAction;

        private void Awake()
        {
            width = Screen.width;
            height = Screen.height;
            logWinRect = new Rect(width * .375f, height * .4f, width * .25f, height * .2f);
            screenRect = new Rect(0, 0, width, height);
        }

        private void OnGUI()
        {
            logWinRect = GUI.Window(0, logWinRect, LogWindow, "H5 Simulate");
        }

        private void LogWindow(int InWindowId)
        {
            UnityGUIAction?.Invoke();
            GUI.DragWindow(screenRect);
        }
    }
}