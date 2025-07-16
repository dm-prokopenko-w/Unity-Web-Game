using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace View.Transition
{
    public class SceneTransitionView : MonoBehaviour
    {

        [SerializeField] private Image transitionBg;
        [SerializeField] private int endScale;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;

        [Header("startup settings")] 
        [SerializeField] private bool startSceneFromHide;

        private void Start()
        {
            if (startSceneFromHide)
            {
                transitionBg.transform.localScale = Vector3.one * endScale;
                ShowScreen();
            }
        }

        public void HideScreen(Action onComplete = null)
        {
            transitionBg.transform.DOScale(Vector3.one * endScale, duration).SetEase(ease).OnComplete(
                ()=>onComplete?.Invoke());
        }

        public void ShowScreen(Action onComplete = null)
        {
            transitionBg.transform.DOScale(Vector3.zero, duration).SetEase(ease).OnComplete(
                ()=>onComplete?.Invoke());;
        }

    }
}