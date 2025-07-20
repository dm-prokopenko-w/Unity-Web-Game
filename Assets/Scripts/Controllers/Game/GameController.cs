using Controllers.SceneTransition;
using Services.Data;
using UnityEngine;
using Zenject;

public class GameController : IInitializable
{
    [Inject] private readonly ISceneTransitionController _sceneTransition;
    [Inject] private readonly IDataService _dataService;
    [Inject] private readonly IGameBuilder _gameBuilder;

    private readonly SettingsView _settingsView;
    private readonly GameView _gameView;

    public GameController(SettingsView settingsView, GameView gameView)
    {
        Debug.Log("LobbyController");
        _settingsView = settingsView;
        _gameView = gameView;
    }

    public void Initialize()
    {
        _settingsView.gameObject.SetActive(true);
        _gameView.gameObject.SetActive(false);
        _gameBuilder.Setup(_dataService.GetSessionData(), _gameView);
        _settingsView.Setup(SetGame, SetGame);
        _sceneTransition.ShowScreen();
    }
    
    private void SetGame()
    {
        _sceneTransition.HideScreen(() =>
        {
            _settingsView.gameObject.SetActive(false);
            _gameView.gameObject.SetActive(true);
            _sceneTransition.ShowScreen();
        });
    }
}
