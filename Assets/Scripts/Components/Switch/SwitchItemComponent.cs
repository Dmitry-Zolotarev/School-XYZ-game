using UnityEngine;


public class SwitchItemComponent : MonoBehaviour
{
    [SerializeField] private string animationKey;
    [SerializeField] private bool state;
    [SerializeField] private Animator animator;
    public void Switch()
    {
        state = !state;
        animator.SetBool (animationKey, state);
    }
}
