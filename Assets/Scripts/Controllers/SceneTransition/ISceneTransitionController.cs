using System;

namespace Controllers.SceneTransition
{
    public interface ISceneTransitionController
    {

        void HideScreen(Action onComplete = null);
        void ShowScreen(Action onComplete = null);

    }
}