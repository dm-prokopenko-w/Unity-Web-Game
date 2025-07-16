using System.Collections;
using Configs;
using Helpers;
using UnityEngine.Networking;
using Zenject;

namespace Services.Api
{
    public class ApiService : IApiService
    {

        [Inject] readonly private EndPointsConfig _endPointsConfig;
        
        public void GetCurrentLevelDataFromServer(System.Action<string> onSuccess, System.Action<string> onError)
        {
            MonoBehaviourHelper.Instance.StartCoroutine(GetRequestCoroutine(onSuccess, onError));
        }

        private IEnumerator GetRequestCoroutine(System.Action<string> onSuccess, System.Action<string> onError)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(_endPointsConfig.GetCurrentEndPointUrl()))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    onSuccess?.Invoke(webRequest.downloadHandler.text);
                }
                else
                {
                    onError?.Invoke(webRequest.error);
                }
            }
        }
    }
}