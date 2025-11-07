using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private float direction = 0;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    [SerializeField] private float velocity = 1f, jumpForce = 7f, sprintAcceleration = 2f;
    [SerializeField] LayerMask groundLayer;

    private bool isRunning, isGrounded, isJumping;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    public void SetDirection(float _direction) {
        
        direction = _direction;
        if (direction != 0) sprite.flipX = direction < 0;
    } 
    public void Jump()
    {
        if (isGrounded) {
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            isJumping = true;
        }
        
    }
    public void FixedUpdate()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1, groundLayer);
        Vector2 currentVelocity = rb.linearVelocity;
        currentVelocity.x = direction * velocity;
        rb.linearVelocity = currentVelocity;
        if (isGrounded || currentVelocity.y < 0) isJumping = false;

        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsRunning", Mathf.Abs(currentVelocity.x) > 0);
    }
}
