using UnityEngine;

namespace Helpers
{
    public class MonoBehaviourHelper : MonoBehaviour
    {
        static MonoBehaviourHelper _instance;
        public static MonoBehaviourHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("MonoBehaviourHelper");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<MonoBehaviourHelper>();
                }
                return _instance;
            }
        }
    }
}