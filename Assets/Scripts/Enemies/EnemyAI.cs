using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(EntityController))]
public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private float detectionRange = 5f;

    private Transform player;
    private bool chasing;
    private bool hitWall;

    private EntityController enemy;

    private void Start()
    {
        enemy = GetComponent<EntityController>();
        enemy.SetDirection(1);
    }

    private void Update()
    {
        // Поиск игрока
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
        }

        float distanceToPlayer = player ? Vector2.Distance(transform.position, player.position) : Mathf.Infinity;

        // Решаем, преследуем игрока или патрулируем
        chasing = player != null && distanceToPlayer < detectionRange;
        if (chasing) ChasePlayer();
        else Patrol();

        // Атака
        if (player && distanceToPlayer < enemy.armRadius) enemy.Attack();
    }

    private void Patrol()
    {

        // Проверяем столкновение со стеной
        if (hitWall)
        {
            hitWall = false;
            enemy.SetDirection(-enemy.GetDirection());
            return;
        }

        // Правая граница
        if (transform.position.x >= rightPoint.position.x) enemy.SetDirection(-1);
        // Левая граница
        if (transform.position.x <= leftPoint.position.x) enemy.SetDirection(1);
    }

    private void ChasePlayer()
    {
        if (!player) return;

        float distance = player.position.x - transform.position.x;
        float dir = distance > 0 ? 1 : -1;

        // Останавливаемся, если близко к игроку
        if (Mathf.Abs(distance) < enemy.armRadius || hitWall)
        {
            enemy.SetDirection(0);
            return;
        }

        enemy.SetDirection(dir);
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
