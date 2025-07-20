using System;
using UnityEngine;
using View.Transition;

namespace Controllers.SceneTransition
{
    public class SceneTransitionController : ISceneTransitionController
    {
        
        private readonly SceneTransitionView _transitionView;

        public SceneTransitionController(SceneTransitionView transitionView)
        {
            _transitionView = transitionView;
        }

        public void HideScreen(Action onComplete = null)
        {
            _transitionView.HideScreen(onComplete);
        }

        public void ShowScreen(Action onComplete = null)
        {
            _transitionView.ShowScreen(onComplete);
        }
    }
}