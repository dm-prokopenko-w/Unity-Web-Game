using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGridView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _preview;
    [SerializeField] private GameObject goCell;
    
    public void Setup(Action onClick)
    {
        goCell.SetActive(true);
        _button.onClick.AddListener(() =>
        {
            onClick?.Invoke();
            Active(true);
        });
    }
    
    public void Active(bool isOpen) 
    {
        _button.interactable = !isOpen;
        goCell.SetActive(!isOpen);
    }
}
