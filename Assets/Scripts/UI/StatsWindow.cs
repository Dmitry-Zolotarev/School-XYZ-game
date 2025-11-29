using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatsWindow : MonoBehaviour
{
    private Leveling leveling;
    private HPComponent health;
    private AttackComponent attack;
    private Inventory inventory;
    private GameObject player, hotBar;

    [SerializeField] private TextMeshProUGUI levelLabel, XPLabel, HPLabel, damageLabel;
    public GameObject statsWindow, statsMenu, inventoryMenu, perksMenu;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hotBar = GameObject.FindGameObjectWithTag("InventoryHotBar");
        if (player != null)
        {
            leveling = player.GetComponent<Leveling>();
            health = player.GetComponent<HPComponent>();
            attack = player.GetComponent<AttackComponent>();
            inventory = player.GetComponent<Inventory>();
        }
    }
    void Start() => statsWindow.SetActive(false); 
    public void SelectStatsMenu()
    {
        statsMenu?.SetActive(true);
        inventoryMenu?.SetActive(false);
        perksMenu?.SetActive(false);

        if (leveling != null)
        {
            levelLabel?.SetText("Level: " + leveling.level);
            XPLabel?.SetText($"XP: {leveling.XP} / {leveling.currentXPforLevelUP}");
        }
        if (health != null) HPLabel?.SetText($"HP: {health.HP} / {health.maxHP}");
        if (attack != null) damageLabel?.SetText("Damage: " + attack.damage);

        Cursor.visible = true;
    }
    public void SelectInventoryMenu()
    {
        statsMenu?.SetActive(false);
        inventoryMenu?.SetActive(true);
        perksMenu?.SetActive(false);

        Cursor.visible = true;
    }
    public void SelectPerksMenu()
    {
        statsMenu?.SetActive(false);
        inventoryMenu?.SetActive(false);
        perksMenu?.SetActive(true);
        Cursor.visible = true;
    }
    

    private void OpenWindow(GameObject menu)
    {
        if (statsWindow == null || menu == null) return;

        if (menu == statsMenu) SelectStatsMenu();
        if (menu == inventoryMenu) SelectInventoryMenu();
        if (menu == perksMenu) SelectPerksMenu();

        var hotBarScript = hotBar?.GetComponent<InventoryWindow>();
        if (hotBarScript != null) inventory.ItemsChanged -= hotBarScript.ReDraw;

        hotBar?.SetActive(false);
        statsWindow.SetActive(true);
        menu.SetActive(true);

        Cursor.visible = true;
    }   

    public void CloseWindow()
    {
        if (statsWindow == null || !statsWindow.activeSelf) return;

        var hotBarScript = hotBar?.GetComponent<InventoryWindow>();
        if (hotBarScript != null) inventory.ItemsChanged += hotBarScript.ReDraw;

        hotBar?.SetActive(true);

        if (inventory != null && hotBarScript != null)
        {
            inventory.SelectItem(inventory.selectedSlot % hotBarScript.hotbarSize);
        }    
        statsWindow.SetActive(false);
        Cursor.visible = false;
    }
    public void CloseWindow(InputAction.CallbackContext context)
    {
        if (context.performed) CloseWindow();
    }
    public void ToggleStatsWindow(InputAction.CallbackContext context)
    {
        if (context.performed) ToggleStatsWindow();
    }
    public void ToggleInventoryWindow(InputAction.CallbackContext context)
    {
        if (context.performed) ToggleInventoryWindow();
    }
    public void TogglePerksWindow(InputAction.CallbackContext context)
    {
        if (context.performed) TogglePerksWindow();
    }
    public void ToggleStatsWindow()
    {
        if (statsWindow != null && !statsWindow.activeSelf)
        {
            OpenWindow(statsMenu);
        }   
        else CloseWindow();
    }  
    public void ToggleInventoryWindow()
    {
        if (statsWindow != null && !statsWindow.activeSelf)
        {
            OpenWindow(inventoryMenu);
        }
        else CloseWindow();
    }
    
    public void TogglePerksWindow()
    {
        if (statsWindow != null && !statsWindow.activeSelf)
        {
            OpenWindow(perksMenu);
        }
        else CloseWindow();
    }
}
