
using System;
using Transfer.ApiData;

namespace Services.Data
{
    public interface IDataService
    {
        public void Initialize(Action onComplete);
        public SessionData GetSessionData();
    }
}