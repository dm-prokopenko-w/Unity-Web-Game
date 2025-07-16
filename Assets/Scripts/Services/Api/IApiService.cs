namespace Services.Api
{
    public interface IApiService
    {
        void GetCurrentLevelDataFromServer(System.Action<string> onSuccess, System.Action<string> onError);
    }
}