using System.Collections.Generic;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/Data")]
    public class EndPointsConfig : ScriptableObject
    {
        [SerializeField] private List<string> _endpointsUrls = new List<string>();
        
        private int _currentConfigID;

        public string GetCurrentEndPointUrl()
        {
            _currentConfigID++;
            
            if (_currentConfigID >= _endpointsUrls.Count)
                _currentConfigID = 0;
            
            return _endpointsUrls[_currentConfigID];
        }
    }
}