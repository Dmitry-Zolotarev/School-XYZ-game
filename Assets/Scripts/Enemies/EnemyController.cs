using UnityEngine;

public class EnemyController : EntityController
{
    [Header("AI Settings")]
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private float detectionRange = 5f;

    private Transform player;
    private bool hitWall;

    private void Update()
    {
        // Поиск игрока
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }
        Vector2 toPlayer = player.position - transform.position;
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, toPlayer.normalized, toPlayer.magnitude, groundLayer);
        bool isBlocked = rayHit.collider != null && rayHit.distance < toPlayer.magnitude && rayHit.collider.gameObject != gameObject;

        if (player != null && !isBlocked && toPlayer.magnitude < detectionRange) ChasePlayer();
        else Patrol();

        // Атака
        if (player && toPlayer.magnitude < armRadius * 1.5f) Attack();
    }

    private void Patrol()
    {

        // Проверяем столкновение со стеной
        if (hitWall)
        {
            hitWall = false;
            SetDirection(-direction);
            return;
        }

        // Правая граница
        if (transform.position.x >= rightPoint.position.x) SetDirection(-1);
        // Левая граница
        if (transform.position.x <= leftPoint.position.x) SetDirection(1);
    }

    private void ChasePlayer()
    {
        if (player == null) return;

        float distance = player.position.x - transform.position.x;
        float dir = distance > 0 ? 1 : -1;

        // Останавливаемся, если близко к игроку
        if (Mathf.Abs(distance) < armRadius || hitWall)
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
