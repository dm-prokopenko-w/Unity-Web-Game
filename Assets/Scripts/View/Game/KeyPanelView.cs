using UnityEngine;
using UnityEngine.UI;

public class KeyPanelView : MonoBehaviour
{
    [SerializeField] private Image viewKey;
    
    public void Setup(bool value)
    {
        viewKey.color = value ? Color.white : Color.black;
    }

}
