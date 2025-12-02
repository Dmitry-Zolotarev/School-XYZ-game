
using UnityEngine;

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

    protected Rigidbody2D rb;
    protected Animator animator;
    
    [SerializeField] private GameObject HPBar, runParticles, jumpParticles, fallParticles, hitParticles;  
    [HideInInspector] public SpawnComponent spawner;
    
    protected PerksComponent perks;

    [SerializeField] protected float velocity = 1f, HPBarOffset = 0.8f, HPBarScale = 0.1f;
    [SerializeField] protected LayerMask groundLayer;
    protected bool isRunning, isGrounded, isJumping, facingRight = true;

    protected static readonly int AnimatorIsGrounded = Animator.StringToHash("IsGrounded");
    protected static readonly int AnimatorIsJumping = Animator.StringToHash("IsJumping");
    protected static readonly int AnimatorIsRunning = Animator.StringToHash("IsRunning");
    protected static readonly int AnimatorHit = Animator.StringToHash("Hit");
    protected static readonly int AnimatorDie = Animator.StringToHash("Die");

    [HideInInspector] public HPComponent health;
    protected AttackComponent attackComponent;
    protected int jumpCount = 0, dashCount = 0, velocityModifier = 1;
    protected Collider2D collider;

    protected void Awake()
    {
        if (tag != "Player") SetDirection(1);     
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spawner = GetComponent<SpawnComponent>();
        health = GetComponent<HPComponent>();
        attackComponent = GetComponent<AttackComponent>();
        perks = GetComponent<PerksComponent>();
        collider = GetComponent<Collider2D>();
        if(HPBar != null) HPBar.transform.localScale = Vector3.one * HPBarScale;
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

    private bool CheckGround()
    {
        Collider2D collider = GetComponent<Collider2D>();       
        Vector2 origin = (Vector2)collider.bounds.center + Vector2.down * (collider.bounds.extents.y + 0.05f);
        var hit = Physics2D.Raycast(origin, Vector2.down, 0.1f, groundLayer);
        if (perks != null && perks.IsUnlocked("Jump attack") && !isGrounded && hit) attackComponent.Stomp();
        return hit;
    }
    protected bool CheckPit()
    {
        bool canGo;
        transform.position += Vector3.right * direction;
        canGo = CheckGround();
        transform.position += Vector3.left * direction;
        return canGo;
    }
    private void FixedUpdate()
    {    
        if (health.HP <= 0 && tag != "Player") return;
        bool lastGrounded = isGrounded;
        isGrounded = CheckGround();
        Vector2 vel = rb.linearVelocity;
        if (dashCount == 0)
        {          
            vel.x = direction * velocity * velocityModifier;
            rb.linearVelocity = vel;
        }      
        if (isGrounded) 
        {
            jumpCount = 0;
            dashCount = 0;
        }     
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
        {
            spawner.prefab = runParticles;
        }          
        else if (isJumping && jumpParticles != null) spawner.prefab = jumpParticles;

        if (HPBar != null) HPBar.transform.position = transform.position + Vector3.up * HPBarOffset;

        
        
    }
    public void OnDamage()
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
        if (HPBar != null) Destroy(HPBar);
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
        if (attackComponent != null && health.HP > 0)
        {
            attackComponent.CurrentDirection = facingRight ? Vector2.right : Vector2.left;
            attackComponent.Attack();
        }        
    }
}
