using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatsWindow : MonoBehaviour
{
    private Leveling leveling;
    private HPComponent health;
    private AttackComponent attack;

    [SerializeField] private TextMeshProUGUI levelLabel, XPLabel, HPLabel, damageLabel;
    [SerializeField] private GameObject statsWindow, player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player != null )
        {
            leveling = player.GetComponent<Leveling>();
            health = player.GetComponent<HPComponent>();
            attack = player.GetComponent<AttackComponent>();
        }      
    } 
    public void ToggleStatsWindow(InputAction.CallbackContext context)
    {
        if (context.performed && statsWindow != null && !statsWindow.activeSelf && Time.timeScale == 1f)
        {
            levelLabel?.SetText("Level: " + leveling.level);
            XPLabel.SetText($"XP: {leveling.XP} / {leveling.currentXPforLevelUP}");
            HPLabel.SetText($"HP: {health.HP} / {health.maxHP}");         
            damageLabel?.SetText("Damage: " + attack.damage);
            statsWindow?.SetActive(true);
            Time.timeScale = 0f;
        }
        else CloseStatsWindow(context);
    }
    public void CloseStatsWindow(InputAction.CallbackContext context)
    {
        if (context.performed && statsWindow != null && statsWindow.activeSelf && Time.timeScale == 0f)
        {
            statsWindow?.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}