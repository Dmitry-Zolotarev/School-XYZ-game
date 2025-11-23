using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatsWindow : MonoBehaviour
{
    private Leveling leveling;
    private HPComponent health;
    private AttackComponent attack;

    [SerializeField] private TextMeshProUGUI levelLabel, XPLabel, HPLabel, damageLabel;
    [SerializeField] private GameObject statsWindow, statsMenu, inventoryMenu;
    private GameObject player, hotBar;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hotBar = GameObject.FindGameObjectWithTag("InventoryHotBar");
        if (player != null )
        {
            leveling = player.GetComponent<Leveling>();
            health = player.GetComponent<HPComponent>();
            attack = player.GetComponent<AttackComponent>();
        }      
    }
    public void SelectInventory()
    {
        statsMenu?.SetActive(false);      
        inventoryMenu?.SetActive(true);
        Cursor.visible = true;
    }
    public void SelectStats()
    {
        statsMenu?.SetActive(true);
        inventoryMenu?.SetActive(false);      
        levelLabel?.SetText("Level: " + leveling.level);
        XPLabel.SetText($"XP: {leveling.XP} / {leveling.currentXPforLevelUP}");
        HPLabel.SetText($"HP: {health.HP} / {health.maxHP}");
        damageLabel?.SetText("Damage: " + attack.damage);
        Cursor.visible = true;
    }
    public void ToggleStatsWindow(InputAction.CallbackContext context)
    {
        if (context.performed && statsWindow != null && !statsWindow.activeSelf && Time.timeScale == 1f)
        {
            hotBar?.SetActive(false);
            statsWindow?.SetActive(true);
            
            SelectStats();
            
            Time.timeScale = 0f;
        }
        else CloseWindow(context);
    }
    public void ToggleInventoryWindow(InputAction.CallbackContext context)
    {
        if (context.performed && statsWindow != null && !statsWindow.activeSelf && Time.timeScale == 1f)
        {
            hotBar?.SetActive(false);
            statsWindow?.SetActive(true);
            SelectInventory();
            Time.timeScale = 0f;
        }
        else CloseWindow(context);
    }
    public void CloseWindow(InputAction.CallbackContext context)
    {
        if (context.performed && statsWindow != null && statsWindow.activeSelf && Time.timeScale == 0f)
        {
            hotBar?.SetActive(true);
            statsWindow?.SetActive(false);
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }
}