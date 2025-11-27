using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private GameObject attackParticles;
    [SerializeField] private float attackCooldown = 0.5f;

    [HideInInspector] public SpawnComponent spawner;
    private Animator animator;
    
    private LineRenderer laserRay;

    [HideInInspector] public float armRadiusIncrease = 0, attackCooldownScale = 1f;
    [HideInInspector] public Vector3 CurrentDirection;
    public int damage = 5, damageIncrease = 1;
    
    public float attackRadius = 0.5f;
    private float lastAttackTime;

    public int attackMode = 0;

    private static readonly int AnimatorRange = Animator.StringToHash("RangeShot");
    private static readonly int AnimatorMelee = Animator.StringToHash("Melee");
    public GameObject projectile;
    [SerializeField] private UnityEvent onMeleeAttack, onRangeAttack;
    void Awake()
    {
        spawner = GetComponent<SpawnComponent>();
        animator = GetComponent<Animator>();
        laserRay = GetComponent<LineRenderer>();
        laserRay.enabled = false;
    }

    public void Attack()
    {
        if (Time.time < lastAttackTime + attackCooldown * attackCooldownScale) return;
        lastAttackTime = Time.time;

        if (attackParticles != null)
        {
            spawner.prefab = attackParticles;
            spawner.Spawn();
        }

        switch (attackMode)
        {
            case 0: Melee(); break;
            case 1: StartCoroutine(Range()); break;
            case 2: StartCoroutine(Ray()); break;
        }
    }
    public void Stomp()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position + Vector3.down, attackRadius);
        foreach (var hit in hits)
        {
            var target = hit.GetComponent<HPComponent>();
            if (target != null && target.gameObject.tag != gameObject.tag) target.ApplyDamage(damage);
        }
    }
    private void Melee()
    {
        animator.SetTrigger(AnimatorMelee);
        onMeleeAttack?.Invoke();

        var hits = Physics2D.OverlapCircleAll(transform.position, attackRadius + armRadiusIncrease);
        foreach (var hit in hits)
        {
            var target = hit.GetComponent<HPComponent>();
            if (target != null && target.gameObject.tag != gameObject.tag) target.ApplyDamage(damage * damageIncrease);
        }
    }

    private IEnumerator Range()
    {
        animator.SetTrigger(AnimatorRange);
        yield return new WaitForSeconds(attackCooldown * attackCooldownScale / 3f);
        onRangeAttack?.Invoke();

        if (projectile != null)
        {
            spawner.prefab = projectile;
            spawner.Spawn();
        }
    }

    private IEnumerator Ray()
    {
        animator.SetTrigger(AnimatorRange);
        yield return new WaitForSeconds(attackCooldown * attackCooldownScale / 3f);
        onRangeAttack?.Invoke();

        Vector3 origin = transform.position + CurrentDirection * 0.25f + Vector3.up * 0.4f;
        float distance = armRadiusIncrease;

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, CurrentDirection, distance);
        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.tag != gameObject.tag && hit.collider.gameObject.tag != "Confiner" && !hit.collider.isTrigger)

            {
                distance = Vector2.Distance(origin, hit.point);
                var target = hit.collider.GetComponent<HPComponent>();
                if (target != null) target.ApplyDamage(damage * damageIncrease);
                break;
            }
        }

        laserRay.positionCount = 2;
        laserRay.SetPosition(0, origin);
        laserRay.SetPosition(1, origin + CurrentDirection * distance);
        laserRay.enabled = true;

        float t = 0.1f;
        while (t > 0)
        {
            Vector3 newOrigin = transform.position + CurrentDirection * 0.25f + Vector3.up * 0.4f;
            laserRay.SetPosition(0, newOrigin);
            laserRay.SetPosition(1, newOrigin + CurrentDirection * distance);
            t -= Time.deltaTime;
            yield return null;
        }
        laserRay.enabled = false;
    }
}
