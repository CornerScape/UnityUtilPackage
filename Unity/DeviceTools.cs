using System;
using UnityEngine;

namespace Szn.Framework.UtilPackage
{
    public class DeviceTools : MonoBehaviour
    {
        private static float screenWidth;
        public static float ScreenWidth => screenWidth;
        
        private static float screenHeight;
        public static float ScreenHeight => screenHeight;

        private static int screenOrientationEventCount;
        private static event Action<ScreenOrientation> screenOrientationEvent;

        public static event Action<ScreenOrientation> ScreenOrientationEvent
        {
            add
            {
                ++screenOrientationEventCount;
                screenOrientationEvent += value;
            }
            remove
            {
                --screenOrientationEventCount;
                screenOrientationEvent -= value;
            }
        }

        private static ScreenOrientation currentScreenOrientation;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Init()
        {
            (new GameObject("Device Tools")).AddComponent<DeviceTools>();
        }

        private void Awake()
        {
            screenWidth = Screen.width;
            screenHeight = Screen.height;

            SetScreenOrientation(true, false, false, false);
           
            currentScreenOrientation = Screen.orientation;
        }

        private void Update()
        {
            if (screenOrientationEventCount > 0 && currentScreenOrientation != Screen.orientation)
            {
                currentScreenOrientation = Screen.orientation;
                screenOrientationEvent?.Invoke(currentScreenOrientation);
            }
        }

        public static void SetScreenOrientation(
            bool InIsAutorotateToPortrait = true,
            bool InIsAutorotateToLandscapeLeft = true,
            bool InIsAutorotateToLandscapeRight = true,
            bool InIsAutorotateToPortraitUpsideDown = true)
        {
            if (!(InIsAutorotateToPortrait || InIsAutorotateToLandscapeLeft || InIsAutorotateToLandscapeRight ||
                  InIsAutorotateToPortraitUpsideDown))
            {
                Screen.autorotateToPortrait = true;
            }
            else
            {
                Screen.autorotateToPortrait = InIsAutorotateToPortrait;
            }

            Screen.autorotateToLandscapeLeft = InIsAutorotateToLandscapeLeft;
            Screen.autorotateToLandscapeRight = InIsAutorotateToLandscapeRight;
            Screen.autorotateToPortraitUpsideDown = InIsAutorotateToPortraitUpsideDown;
        }

        public static void SetScreenOrientation(ScreenOrientation InScreenOrientation,
            bool InIsAutorotateToPortrait = true,
            bool InIsAutorotateToLandscapeLeft = true,
            bool InIsAutorotateToLandscapeRight = true,
            bool InIsAutorotateToPortraitUpsideDown = true)
        {
            Screen.orientation = InScreenOrientation;
            Screen.autorotateToPortrait =
                InIsAutorotateToPortrait || ScreenOrientation.Portrait == InScreenOrientation;
            Screen.autorotateToLandscapeLeft =
                InIsAutorotateToLandscapeLeft || ScreenOrientation.LandscapeLeft == InScreenOrientation;
            Screen.autorotateToLandscapeRight =
                InIsAutorotateToLandscapeRight || ScreenOrientation.LandscapeRight == InScreenOrientation;
            Screen.autorotateToPortraitUpsideDown =
                InIsAutorotateToPortraitUpsideDown || ScreenOrientation.PortraitUpsideDown == InScreenOrientation;
        }
    }
}