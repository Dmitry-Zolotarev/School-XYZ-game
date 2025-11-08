using UnityEngine;


public class SwitchComponent : MonoBehaviour
{
    [SerializeField]private Animator animator;
    [SerializeField] private string animationKey;
    [SerializeField] private bool state;
    
    public void Switch()
    {
        state = !state;
        animator.SetBool (animationKey, state);
    }
}
