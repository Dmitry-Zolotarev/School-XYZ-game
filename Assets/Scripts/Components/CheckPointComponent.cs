using UnityEngine;

[RequireComponent (typeof(Animator))]
public class CheckPointComponent : MonoBehaviour
{
    private PlayerController playerController;
    private PlaySoundsComponent playSounds;
    private Animator animator;
    private bool isChecked = false;
    private void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playSounds = GetComponent<PlaySoundsComponent>();
        animator = GetComponent<Animator>();
    }
    public void Check()
    {
        if (!isChecked)
        {
            isChecked = true;
            animator.SetBool("Checked", true);
            playerController.checkPoint = transform.position;
            playSounds?.Play("CheckPoint");
        }
        
    }
    private void Update()
    {
        if(playerController.health.isDead) isChecked = false;
        if (playerController.checkPoint != transform.position) 
        {
            isChecked = false;
            animator.SetBool("Checked", false);
        }
        
    }
}
