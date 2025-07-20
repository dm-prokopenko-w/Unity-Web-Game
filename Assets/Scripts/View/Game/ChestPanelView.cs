using UnityEngine;

public class ChestPanelView : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    private bool isOpen;
    
    public void SetAnimation(string animName) => anim.SetTrigger(animName);

    public void Setup(bool value) => isOpen = value;

    private void OnEnable()
    {
        SetAnimation(isOpen ? 
            Constants.ChestAnimIsOpen : 
            Constants.ChestAnimIsClose);
    }
}
