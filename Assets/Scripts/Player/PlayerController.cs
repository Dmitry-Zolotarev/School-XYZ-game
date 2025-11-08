using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpawnComponent))]
public class PlayerController : MonoBehaviour
{
    private float direction = 0;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private GameObject runParticles, jumpParticles, fallParticles;
    [SerializeField] private float velocity = 1f, jumpForce = 7f, sprintAcceleration = 2f;
    [SerializeField] LayerMask groundLayer;

    private bool isRunning, isGrounded, isJumping, facingRight = true;
    private int jumpCount;
    private SpawnComponent spawner;

    private static readonly int AnimatorIsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int AnimatorIsJumping = Animator.StringToHash("IsJumping");
    private static readonly int AnimatorIsRunning = Animator.StringToHash("IsRunning");
    private static readonly int AnimatorHit = Animator.StringToHash("Hit");
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawner = GetComponent<SpawnComponent>();
        Cursor.visible = false;
    }
    public void SetDirection(float _direction)
    {
        direction = _direction;
        if (direction > 0 && !facingRight || direction < 0 && facingRight) Flip();
    }
    private void Flip() 
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public void Jump()
    {
        if (jumpCount < 1) {
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            jumpCount++;
        } 
    }
    private bool CheckGround()
    {
        Vector2 origin = (Vector2)transform.position + Vector2.down * 0.5f;
        return Physics2D.Raycast(origin, Vector2.down, 0.1f, groundLayer);
    }
    private void FixedUpdate()
    {
        bool lastGroundedState = isGrounded;
        isGrounded = CheckGround();
        if (isGrounded) jumpCount = 0;
        Vector2 currentVelocity = rb.linearVelocity;
        currentVelocity.x = direction * velocity;
        rb.linearVelocity = currentVelocity;
        isJumping = !isGrounded && currentVelocity.y > 0;
        isRunning = isGrounded && Mathf.Abs(currentVelocity.x) > 0;

        animator.SetBool(AnimatorIsGrounded, isGrounded);
        animator.SetBool(AnimatorIsJumping, isJumping);
        animator.SetBool(AnimatorIsRunning, isRunning);

        if (isGrounded && !lastGroundedState)
        {
            spawner.prefab = fallParticles;
            spawner.Spawn();
        }
        else if (isRunning) spawner.prefab = runParticles;
        else if (isJumping) spawner.prefab = jumpParticles;
             
    }
    public void TakeDamage()
    {
        animator.SetTrigger(AnimatorHit);
    }
}