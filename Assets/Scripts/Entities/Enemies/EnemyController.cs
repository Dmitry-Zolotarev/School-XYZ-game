using UnityEngine;
using UnityEngine.Events;

public class EnemyController : EntityController
{
    [Header("AI Settings")]
    [SerializeField] private Transform leftPoint, rightPoint;
    [SerializeField] private float detectionRange = 5f;

    
    private bool chasing, hitWall;
    private Transform player;
    [SerializeField] private UnityEvent onBeginChasing;

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null || attackComponent == null) return;

        float distanceToPlayerX = Mathf.Abs(transform.position.x - player.position.x);
        float distanceToPlayerY = Mathf.Abs(transform.position.y - player.position.y);

        bool lastChaseState = chasing;
        
        chasing = distanceToPlayerX < detectionRange && distanceToPlayerY < 1f;
        
        if (chasing) {
            if (chasing != lastChaseState) {
                animator.SetTrigger(AnimatorHit);
                onBeginChasing?.Invoke();
            } 
            ChasePlayer();
        } 
        else Patrol();
        if (distanceToPlayerX < attackComponent.armRadius && distanceToPlayerY < 1f) Attack();
    }

    private void Patrol()
    {
        if (hitWall)
        {
            hitWall = false;
            SetDirection(-direction);
            return;
        }
        if (transform.position.x >= rightPoint.position.x) SetDirection(-1);
        if (transform.position.x <= leftPoint.position.x) SetDirection(1);
    }

    private void ChasePlayer()
    {
        float distance = player.position.x - transform.position.x, dir = distance > 0 ? 1 : -1;

        if (Mathf.Abs(distance) < attackComponent.armRadius || hitWall)
        {
            SetDirection(0);
            return;
        }
        SetDirection(dir);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;

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