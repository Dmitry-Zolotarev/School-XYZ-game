using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HPComponent))]
public class PlayerController : EntityController
{

    [SerializeField] private UnityEvent onJump, onDash;
    [SerializeField] private float jumpForce = 5.5f, dashForce = 3f;
    private static PlayerController instance;
    private PerksComponent perks;
    private new void Awake()
    {
        base.Awake();
        perks = GetComponent<PerksComponent>();
        if (instance != null && instance != this)
        {
            instance.SetPosition(transform.position);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public new void OnDie()
    {
        base.OnDie();
        var gameOverWindow = GameObject.FindGameObjectWithTag("GameOver");

        gameOverWindow?.GetComponent<GameOverComponent>()?.GameOver();
    }
    public void Jump()
    {
        if (isGrounded || (perks.IsUnlocked("Double jump") && jumpCount < 1))
        {
            onJump?.Invoke();
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            jumpCount++;
        }
    }
    public void Dash()
    {
        if (perks.IsUnlocked("Dash") && dashCount == 0)
        {
            onDash?.Invoke();
            float dir = facingRight ? 1 : -1;

            rb.AddForce(dashForce * Vector2.right * dir, ForceMode2D.Impulse);
            dashCount++;
        }
    }
    public void SaveSceneToFile()
    {
    }
    public void LoadSceneFromFile()
    {
    }
}