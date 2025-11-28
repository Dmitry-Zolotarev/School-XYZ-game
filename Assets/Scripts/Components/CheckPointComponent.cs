using UnityEngine;

[RequireComponent (typeof(Animator))]
public class CheckPointComponent : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;
    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }
    public void Check()
    {
        animator.SetBool("Checked", true);
        playerController.checkPoint = transform.position;
    }
    private void Update()
    {
        if (playerController.checkPoint != transform.position) animator.SetBool("Checked", false);
    }
}
