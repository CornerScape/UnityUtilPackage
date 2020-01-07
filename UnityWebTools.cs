using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Szn.Framework.Web
{
    public static class UnityWebTools
    {
        public static void DownloadHandle(this MonoBehaviour InMono, string InUrl,
            Action<bool, DownloadHandler, string> InResultCallback)
        {
            if (string.IsNullOrEmpty(InUrl))
            {
                Debug.LogError("Download url can not be null or empty.");
                return;
            }

            if (null == InResultCallback)
            {
                Debug.LogError("Do not download unused resources.");
                return;
            }

            if (null == InMono) DownloadHandle(InUrl, InResultCallback);
            else InMono.StartCoroutine(DownloadHandleAsync(InUrl, InResultCallback));
        }

        public static IEnumerator DownloadHandleAsync(string InUrl,
            Action<bool, DownloadHandler, string> InResultCallback)
        {
            if (null == InResultCallback)
            {
                Debug.LogError("Do not download unused resources.");
                yield break;
            }

            using (UnityWebRequest request = UnityWebRequest.Get(InUrl))
            {
                yield return request.SendWebRequest();

                if (request.isHttpError || request.isNetworkError) InResultCallback.Invoke(false, null, request.error);

                InResultCallback.Invoke(true, request.downloadHandler, null);
            }
        }

        public static void DownloadHandle(string InUrl, Action<bool, DownloadHandler, string> InResultCallback)
        {
            if (null == InResultCallback)
            {
                Debug.LogError("Do not download unused resources.");
                return;
            }

            using (UnityWebRequest request = UnityWebRequest.Get(InUrl))
            {
                request.SendWebRequest();

                while (!request.isDone)
                {
                }

                if (request.isHttpError || request.isNetworkError) InResultCallback.Invoke(false, null, request.error);

                InResultCallback.Invoke(true, request.downloadHandler, null);
            }
        }
    }
}