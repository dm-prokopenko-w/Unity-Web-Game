using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View.Loading
{
    public class LoadingView : MonoBehaviour
    {
        
        [SerializeField] private Slider loadingProgressSlider;
        [SerializeField] private TMP_Text loadingProgressText;
        [SerializeField] private float animationDuration = 0.5f;

        private Tween _sliderTween;
        private Tween _textTween;
        
        public void AddLoadingProgress(float progress, Action onComplete = null)
        {
            progress = Mathf.Clamp01(progress);

            _sliderTween?.Kill();
            _textTween?.Kill();

            _sliderTween = DOTween.To(
                () => loadingProgressSlider.value,
                x => loadingProgressSlider.value = x,
                progress,
                animationDuration
            ).SetEase(Ease.OutQuad);

            int currentPercent = 0;
            if (int.TryParse(loadingProgressText.text.Replace("%", ""), out int parsed))
            {
                currentPercent = parsed;
            }

            int targetPercent = Mathf.RoundToInt(progress * 100);

            if (currentPercent > targetPercent)
            {
                currentPercent = targetPercent;
                loadingProgressText.text = $"{currentPercent}%";
            }

            _textTween = DOTween.To(
                () => currentPercent,
                x =>
                {
                    currentPercent = x;
                    loadingProgressText.text = $"{currentPercent}%";
                },
                targetPercent,
                animationDuration
            ).SetEase(Ease.OutQuad).OnComplete(() => onComplete?.Invoke());
        }
        
    }
}