using UnityEngine;
using UnityEngine.Events;

public class EnemyController : EntityController
{
    [Header("AI Settings")]
    [SerializeField] private Transform leftPoint, rightPoint;
    [SerializeField] private Vector2 detectionRange = new Vector2(5f, 1f);

    private bool chasing, hitWall;
    private Transform player;

    private static readonly int AnimatorChase = Animator.StringToHash("Chase");
    [SerializeField] private UnityEvent onBeginChasing;
    private void Update()
    {
        if (player == null)
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
            else return;
        }

        velocityModifier = 1;
        if (attackComponent == null) return;
        float distanceToPlayerX = Mathf.Abs(transform.position.x - player.position.x);
        float distanceToPlayerY = Mathf.Abs(transform.position.y - player.position.y);

        bool lastChaseState = chasing;

        chasing = distanceToPlayerX < detectionRange.x && distanceToPlayerY < detectionRange.y;

        if (chasing)
        {
            if (chasing != lastChaseState)
            {
                animator.SetTrigger(AnimatorChase);
                onBeginChasing?.Invoke();
            }
            ChasePlayer();
            if (Mathf.Abs(distanceToPlayerX) <= attackComponent.attackRadius) Attack();
        }
        else Patrol();

    }

    private void Patrol()
    {
        if (hitWall)
        {
            hitWall = false;
            SetDirection(-direction);
        }
        if (leftPoint != null && rightPoint != null)
        {
            if (transform.position.x >= rightPoint.position.x) SetDirection(-1);
            if (transform.position.x <= leftPoint.position.x) SetDirection(1);
        }       
    }

    private void ChasePlayer()
    {
        float distance = player.position.x - transform.position.x;
        SetDirection(distance > 0 ? 1 : -1);

        float attackDistance = Mathf.Max(attackComponent.attackRadius / 2f, 1);
        
        if (!CheckPit() || Mathf.Abs(distance) <= attackDistance || hitWall) velocityModifier = 0;
           
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }
        foreach (var contact in collision.contacts)
        {
            Vector2 normal = contact.normal;
            if (Mathf.Abs(normal.x) > 0.5f && Mathf.Abs(normal.y) < 0.5f)
            {
                hitWall = true;
                break;
            }
        }
    }
}
