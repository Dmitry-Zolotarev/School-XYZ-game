using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private GameObject attackParticles;
    [SerializeField] private float attackCooldown = 0.5f;

    [HideInInspector] public SpawnComponent spawner;
    private Animator animator;
    private GameObject projectile;
    private LineRenderer laserRay;
    private Leveling leveling;

    
    [HideInInspector] public float armRadiusIncrease = 0, attackCooldownScale = 1f;
    [HideInInspector] public Vector2 CurrentDirection;
    public int damage = 5, damageIncrease = 1;
    public float armRadius = 0.5f;
    private float lastAttackTime;

    private enum AttackModes { Melee, Range, Ray }
    [HideInInspector] public int attackMode = (int)AttackModes.Melee;

    private static readonly int AnimatorRange = Animator.StringToHash("RangeShot");
    private static readonly int AnimatorMelee = Animator.StringToHash("Melee");
    [SerializeField] private UnityEvent onMeleeAttack, onRangeAttack;

    void Awake()
    {
        spawner = GetComponent<SpawnComponent>();
        animator = GetComponent<Animator>();
        laserRay = GetComponent<LineRenderer>();
        leveling = GetComponent<Leveling>();
        laserRay.enabled = false;
    }

    public void SetProjectile(GameObject value) => projectile = value;

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
            case (int)AttackModes.Melee: Melee(); break;
            case (int)AttackModes.Range: StartCoroutine(Range()); break;
            case (int)AttackModes.Ray: StartCoroutine(Ray()); break;
        }
    }
    public void Stomp()
    {

        var hits = Physics2D.OverlapCircleAll(transform.position + Vector3.down, armRadius);
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

        var hits = Physics2D.OverlapCircleAll(transform.position, armRadius + armRadiusIncrease);
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

        Vector3 dir = CurrentDirection;
        Vector3 origin = transform.position + (Vector3)dir * 0.25f + Vector3.up * 0.4f;

        float distance = armRadiusIncrease;

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, dir, distance);
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
        laserRay.SetPosition(1, origin + dir * distance);
        laserRay.enabled = true;

        float t = 0.1f;
        while (t > 0)
        {
            Vector3 newOrigin = transform.position + (Vector3)dir * 0.25f + Vector3.up * 0.4f;
            laserRay.SetPosition(0, newOrigin);
            laserRay.SetPosition(1, newOrigin + dir * distance);

            t -= Time.deltaTime;
            yield return null;
        }

        laserRay.enabled = false;
    }
}
