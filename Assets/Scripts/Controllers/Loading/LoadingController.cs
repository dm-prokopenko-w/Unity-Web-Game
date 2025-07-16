using Controllers.SceneTransition;
using Services.Data;
using UnityEngine.SceneManagement;
using View.Loading;
using Zenject;

namespace Controllers.Loading
{
    public class LoadingController : IInitializable
    {
        
        [Inject] private readonly IDataService _dataService;
        [Inject] private readonly ISceneTransitionController _sceneTransition;
        
        private readonly LoadingView _view;
        
        public LoadingController(LoadingView view)
        {
            _view = view;
        }
        
        public void Initialize()
        {
            _dataService.Initialize(()=>
            {
                _view.AddLoadingProgress(1f, () =>
                {
                    _sceneTransition.HideScreen(() =>
                    {
                        SceneManager.LoadScene("Game");
                    });
                });
            });
        }

    }
}