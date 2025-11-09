using UnityEngine;

[RequireComponent (typeof(Animator))]
public class SwitchThisComponent : MonoBehaviour
{
    [SerializeField] private string animationKey;
    [SerializeField] private bool state;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Switch()
    {
        state = !state;
        animator.SetBool(animationKey, state);
    }
}
