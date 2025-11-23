using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpawnComponent))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(HPComponent))]
[RequireComponent(typeof(AttackComponent))]
public class EntityController : MonoBehaviour
{
    protected float direction = 0;

    private Rigidbody2D rb;
    protected Animator animator;
    private LineRenderer laserRay;
    [HideInInspector] public SpawnComponent spawner;

    [SerializeField] private GameObject runParticles, jumpParticles, fallParticles, hitParticles;

    [SerializeField] protected float velocity = 1f, jumpForce = 7f;
    [SerializeField] protected LayerMask groundLayer;

    public bool isRunning, isGrounded, isJumping, facingRight = true;
    private int jumpCount;

    protected static readonly int AnimatorIsGrounded = Animator.StringToHash("IsGrounded");
    protected static readonly int AnimatorIsJumping = Animator.StringToHash("IsJumping");
    protected static readonly int AnimatorIsRunning = Animator.StringToHash("IsRunning");
    protected static readonly int AnimatorHit = Animator.StringToHash("Hit");
    protected static readonly int AnimatorDie = Animator.StringToHash("Die");

    protected HPComponent health;
    protected Inventory inventory;
    protected AttackComponent attackComponent;
    [SerializeField] private UnityEvent onJump;

    protected void Awake()
    {
        if (tag != "Player") SetDirection(1);

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawner = GetComponent<SpawnComponent>();
        inventory = GetComponent<Inventory>();
        health = GetComponent<HPComponent>();
        attackComponent = GetComponent<AttackComponent>();
        laserRay = GetComponent<LineRenderer>();
        laserRay.enabled = false;
    }

    public void SetPosition(Vector3 pos) => transform.position = pos;

    public void SetDirection(float _direction)
    {
        direction = _direction;

        if (_direction > 0 && !facingRight) Flip();
        else if (_direction < 0 && facingRight) Flip();
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Jump()
    {
        if (jumpCount < 1)
        {
            onJump?.Invoke();
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private bool CheckGround()
    {
        Collider2D collider = GetComponent<Collider2D>();
        Vector2 origin = (Vector2)collider.bounds.center +
                         Vector2.down * (collider.bounds.extents.y + 0.05f);

        return Physics2D.Raycast(origin, Vector2.down, 0.1f, groundLayer);
    }

    private void FixedUpdate()
    {
        if (health.HP <= 0) return;

        bool lastGrounded = isGrounded;
        isGrounded = CheckGround();
        if (isGrounded) jumpCount = 0;

        Vector2 vel = rb.linearVelocity;
        vel.x = direction * velocity;
        rb.linearVelocity = vel;

        isJumping = !isGrounded && vel.y > 0;
        isRunning = isGrounded && Mathf.Abs(vel.x) > 0;

        animator.SetBool(AnimatorIsGrounded, isGrounded);
        animator.SetBool(AnimatorIsJumping, isJumping);
        animator.SetBool(AnimatorIsRunning, isRunning);

        if (isGrounded && !lastGrounded && fallParticles != null)
        {
            spawner.prefab = fallParticles;
            spawner.Spawn();
        }
        else if (isRunning && runParticles != null)
            spawner.prefab = runParticles;
        else if (isJumping && jumpParticles != null)
            spawner.prefab = jumpParticles;
    }

    public void TakeDamage()
    {
        if (hitParticles)
        {
            spawner.prefab = hitParticles;
            spawner.Spawn();
        }
        animator.SetTrigger(AnimatorHit);
    }

    public void OnDie()
    {
        animator.SetTrigger(AnimatorDie);
        if (hitParticles)
        {
            spawner.prefab = hitParticles;
            spawner.Spawn();
        }
    }

    public void Interact()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var hit in hits)
        {
            var interaction = hit.GetComponent<InteractableComponent>();
            if (interaction) interaction.Interact();
        }
    }
    public void Attack()
    {
        if (health.HP <= 0) attackComponent = null;
        if (!attackComponent) return;
        attackComponent.CurrentDirection = facingRight ? Vector2.right : Vector2.left;
        attackComponent.Attack();
    }
}
