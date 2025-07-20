using System;
using Controllers.SceneTransition;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Button _btnYes, _btnNo;
    
    public void Setup(Action onClickYes, Action onClickNo)
    {
        _btnYes.onClick.AddListener(() =>
        {
            Debug.Log("Yes => sound on");
            onClickYes?.Invoke();
        });
        
        _btnNo.onClick.AddListener(() =>
        {
            Debug.Log("No => sound off");
            onClickNo?.Invoke();
        });
    }
}
