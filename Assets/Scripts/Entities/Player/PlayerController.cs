using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HPComponent))]
public class PlayerController : EntityController
{
    [SerializeField] private UnityEvent onJump, onDash;
    [SerializeField] private float jumpForce = 5.5f, dashForce = 3f;
    [HideInInspector] public Vector3 checkPoint;
    private void Start()
    {
        checkPoint = transform.position;
    }
    public new void OnDie()
    {
        base.OnDie();
        animator.SetTrigger(AnimatorDie);
        StartCoroutine(Revive());
    }
    private IEnumerator Revive()
    {
        yield return new WaitForSeconds(1);
        transform.position = checkPoint + Vector3.up;
        transform.rotation = Quaternion.identity;
        health.UpdateMaxHP(0);
    }
    public void Jump()
    {
        if (isGrounded || (perks.IsUnlocked("Double jump") && jumpCount < 1))
        {
            onJump?.Invoke();
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            jumpCount++;
        }
    }
    public void Dash()
    {
        if (perks.IsUnlocked("Dash") && dashCount == 0)
        {
            onDash?.Invoke();
            float dir = facingRight ? 1 : -1;

            rb.AddForce(dashForce * Vector2.right * dir, ForceMode2D.Impulse);
            dashCount++;
        }
    }
    public void SaveSceneToFile()
    {
    }
    public void LoadSceneFromFile()
    {
    }
}