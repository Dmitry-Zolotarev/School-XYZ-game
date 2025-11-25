using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class StatsWindow : MonoBehaviour
{
    private Leveling leveling;
    private HPComponent health;
    private AttackComponent attack;
    private PerksComponent perks;
    private Inventory inventory;

    [SerializeField] private TextMeshProUGUI levelLabel, XPLabel, HPLabel, damageLabel, perkScoreLabel;
    [SerializeField] private GameObject statsWindow, statsMenu, inventoryMenu, perksMenu;
    [SerializeField] private List<Button> perkButtons;

    private GameObject player, hotBar;
    private string selectedPerkName = "";

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hotBar = GameObject.FindGameObjectWithTag("InventoryHotBar");
        if (player != null)
        {
            leveling = player.GetComponent<Leveling>();
            health = player.GetComponent<HPComponent>();
            attack = player.GetComponent<AttackComponent>();
            perks = player.GetComponent<PerksComponent>();
            inventory = player.GetComponent<Inventory>();
        }
    }

    void Start() => statsWindow.SetActive(false);

    public void SelectPerkButton(string name)
    {
        if (perks == null) return;

        selectedPerkName = name;

        for (int i = 0; i < perkButtons.Count; i++)
        {
            var image = perkButtons[i].targetGraphic as Image;
            if (image == null) continue;

            // Получаем имя перка, привязанное к этой кнопке
            string perkName = perks.GetName(i);

            var state = perkButtons[i].spriteState;

            image.sprite = (perkName == name) ? state.selectedSprite : perkButtons[i].image.sprite;
        }
    }
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
    private void UpdatePerkScoreLabel()
    {
        int perkScore = perks != null ? perks.perkScore : 0;
        perkScoreLabel.SetText("Perk score: " + perkScore);
    }
    public void SelectPerksMenu()
    {
        statsMenu?.SetActive(false);
        inventoryMenu?.SetActive(false);
        perksMenu?.SetActive(true);
        UpdatePerkScoreLabel();

        Cursor.visible = true;
    }
    public void CancelSelection()
    {
        selectedPerkName = "";

        foreach (var button in perkButtons)
        {
            var image = button.targetGraphic as Image;
            if (image == null) continue;

            image.sprite = button.image.sprite;
        }
    }
    public void BuySelectedPerk()
    {
        if (perks != null && !string.IsNullOrEmpty(selectedPerkName))
        {
            perks.BuyPerk(selectedPerkName);
            UpdatePerkScoreLabel();
        }
  
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
        Time.timeScale = 0f;
    }

    public void CloseWindow(InputAction.CallbackContext context)
    {
        if (statsWindow == null || !statsWindow.activeSelf) return;

        var hotBarScript = hotBar?.GetComponent<InventoryWindow>();
        if (hotBarScript != null) inventory.ItemsChanged += hotBarScript.ReDraw;

        hotBar?.SetActive(true);

        if (inventory != null && hotBarScript != null)
            inventory.SelectItem(inventory.selectedSlot % hotBarScript.hotbarSize);

        statsWindow.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void ToggleStatsWindow(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (statsWindow != null && !statsWindow.activeSelf)
            OpenWindow(statsMenu);
        else
            CloseWindow(context);
    }

    public void ToggleInventoryWindow(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (statsWindow != null && !statsWindow.activeSelf)
            OpenWindow(inventoryMenu);
        else
            CloseWindow(context);
    }
    public void TogglePerksWindow(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (statsWindow != null && !statsWindow.activeSelf)
            OpenWindow(perksMenu);
        else
            CloseWindow(context);
    }
}
