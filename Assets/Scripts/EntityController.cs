using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpawnComponent))]
public class EntityController : MonoBehaviour
{
    protected float direction = 0;

    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private GameObject runParticles, jumpParticles, fallParticles, attackParticles, hitParticles;
    [SerializeField] protected float velocity = 1f, jumpForce = 7f, armRadius = 0.5f;

    [SerializeField] private LayerMask groundLayer;
    [HideInInspector]public bool isRunning, isGrounded, isJumping, facingRight = true, didAttack = false;
    private int jumpCount;
    private SpawnComponent spawner;

    public int damage = 5, damageIncrease = 1;
    public float attackCooldown = 0.5f;
    [HideInInspector]public float lastAttackTime = 0, armRadiusIncrease = 0;

    private static readonly int AnimatorIsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int AnimatorIsJumping = Animator.StringToHash("IsJumping");
    private static readonly int AnimatorIsRunning = Animator.StringToHash("IsRunning");
    private static readonly int AnimatorHit = Animator.StringToHash("Hit");
    private static readonly int AnimatorMelee = Animator.StringToHash("Melee");

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawner = GetComponent<SpawnComponent>();
        if (tag != "Player") SetDirection(1);
    }
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public void SetDirection(float _direction)
    {
        float oldDir = direction;
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
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private bool CheckGround()
    {
        Collider2D collider = GetComponent<Collider2D>();
        Vector2 origin = (Vector2)collider.bounds.center + Vector2.down * (collider.bounds.extents.y + 0.05f);
        float rayLength = 0.1f;

        return Physics2D.Raycast(origin, Vector2.down, rayLength, groundLayer);
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

        if (isGrounded && !lastGroundedState && fallParticles != null)
        {
            spawner.prefab = fallParticles;
            spawner.Spawn();
        }
        else if (isRunning && runParticles != null) spawner.prefab = runParticles;
        else if (isJumping && jumpParticles != null) spawner.prefab = jumpParticles;

        didAttack = Time.time < lastAttackTime + attackCooldown;
    }

    public void TakeDamage()
    {
        if (attackParticles != null)
        {
            spawner.prefab = hitParticles;
            spawner.Spawn();
        }

        if (!didAttack || tag != "Player") animator.SetTrigger(AnimatorHit);
    }
    public void OnDie()
    {
        if (attackParticles != null)
        {
            spawner.prefab = hitParticles;
            spawner.Spawn();
        }
    }
    public void Interact()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, armRadius);
        foreach (var hit in hits)
        {
            var interaction = hit.GetComponent<InteractableComponent>();
            if (interaction != null) interaction.Interact();
        }
    }

    public void Attack()
    {
        if (didAttack) return;

        didAttack = true;
        lastAttackTime = Time.time;

        if (attackParticles != null)
        {
            spawner.prefab = attackParticles;
            spawner.Spawn();
        }

        var hits = Physics2D.OverlapCircleAll(transform.position, armRadius + armRadiusIncrease);
        foreach (var hit in hits)
        {
            var target = hit.GetComponent<HPComponent>();
            if (target != null && target.gameObject.tag != tag) target.ApplyDamage(damage * damageIncrease);
        }

        animator.SetTrigger(AnimatorMelee);
    }
}
