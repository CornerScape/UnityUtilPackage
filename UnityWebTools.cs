using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Szn.Framework.Web
{
    public static class UnityWebTools
    {
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

                if (request.isHttpError || request.isNetworkError) InResultCallback.Invoke(false, null, $"is http error = {request.isHttpError}\nis network error = {request.isNetworkError}\nmsg = {request.error}");

                InResultCallback.Invoke(true, request.downloadHandler, null);
            }
        }
    }
}