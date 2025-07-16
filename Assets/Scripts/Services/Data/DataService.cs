using System;
using Services.Api;
using Transfer.ApiData;
using UnityEngine;
using Zenject;

namespace Services.Data
{
    public class DataService : IDataService
    {
        
        [Inject] private readonly IApiService _apiService;

        private SessionData _sessionData;
        private bool _isInitialized;
        
        public void Initialize(Action onComplete)
        {
            ParseDataFromJson(onComplete);
        }

        public SessionData GetSessionData()
        {
            if (_isInitialized == false)
            {
                Debug.LogError("You need init DataService before geting SessionData");
                return null;
            }
            
            return _sessionData;
        }

        private void ParseDataFromJson(Action onComplete)
        {
            _apiService.GetCurrentLevelDataFromServer(json =>
                {
                    
                    _sessionData = JsonUtility.FromJson<SessionData>(json);
                    Debug.Log($"Seed: {_sessionData.seed}, Tier: {_sessionData.tier}, Progress: {_sessionData.progress}");
                    foreach (var cell in _sessionData.openedCells)
                    {
                        Debug.Log($"Cell: x={cell.x}, y={cell.y}");
                    }
                    onComplete.Invoke();
                },
                err =>
                {
                    Debug.LogError("Error while try get json from api: " + err);
                });
        }
    }
}