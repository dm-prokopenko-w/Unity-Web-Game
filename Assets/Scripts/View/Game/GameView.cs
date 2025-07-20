using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Transform keyParent, transitionBg;
    [SerializeField] private HorizontalLayoutGroup chestParent;
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private GameObject goBlock;
    [SerializeField] private Button btnClaim;

    public GridLayoutGroup Grid => grid; 
    public Transform ChestParent => chestParent.transform; 
    public Transform KeyParent => keyParent; 
    
    public void Setup(float progress, int sizeGridX, Action onClaim)
    {
        chestParent.enabled = true;
        btnClaim.gameObject.SetActive(false);
        btnClaim.onClick.AddListener(() => onClaim?.Invoke());
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = sizeGridX;
        UpdateView(progress);
    }

    public void UpdateView(float progress) => progressSlider.value = progress;
    
    public void SetBlock(bool active) => goBlock.SetActive(active);
    
    public void Win()
    {
        chestParent.enabled = false;
        transitionBg.DOScale(Vector3.one * Constants.endScaleWin, Constants.durationWin)
            .SetEase(Ease.Linear).OnComplete(() =>
            {
                btnClaim.gameObject.SetActive(true);
            });
    }
}
