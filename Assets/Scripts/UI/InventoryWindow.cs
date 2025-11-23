using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private int hotbarSize = 5;
    [SerializeField] private GameObject itemIconPrefab;

    private GridLayoutGroup grid;
    private List<GameObject> drawIcons = new List<GameObject>();
    private bool subscribed = false;

    private void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        TryBindInventory();
    }

    private void Update()
    {
        if (inventory == null) TryBindInventory();

    }

    private void TryBindInventory()
    {
        if (inventory != null) return;

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        inventory = player.GetComponent<Inventory>();
        if (inventory == null) return;

        hotbarSize = Mathf.Min(hotbarSize, inventory.size);

        if (!subscribed)
        {
            inventory.ItemsChanged += ReDraw;
            subscribed = true;
        }

        if (inventory.Items != null && inventory.Items.Length == inventory.size) ReDraw();

    }

    private void OnDestroy()
    {
        if (inventory != null && subscribed)
            inventory.ItemsChanged -= ReDraw;
    }

    private void ReDraw()
    {
        if (inventory == null || inventory.Items == null) return;

        foreach (var icon in drawIcons)
            Destroy(icon);
        drawIcons.Clear();

        // Вычисляем стартовый индекс для хотбара с учётом выбранного слота
        int startIndex = 0;
        if (inventory.selectedSlot >= hotbarSize)
            startIndex = inventory.selectedSlot - hotbarSize + 1;

        int endIndex = Mathf.Min(startIndex + hotbarSize, inventory.size);

        for (int i = startIndex; i < endIndex; i++)
        {
            Item item = inventory.Items[i];
            GameObject icon = GetIcon(item, i == inventory.selectedSlot);
            drawIcons.Add(icon);
        }
    }

    private GameObject GetIcon(Item item, bool isSelected)
    {
        GameObject icon = new GameObject("ItemIcon", typeof(RectTransform));
        icon.transform.SetParent(grid.transform, false);

        RectTransform iconRect = icon.GetComponent<RectTransform>();
        iconRect.anchorMin = new Vector2(0.5f, 0.5f);
        iconRect.anchorMax = new Vector2(0.5f, 0.5f);
        iconRect.sizeDelta = new Vector2(36, 36);
        iconRect.anchoredPosition = Vector2.zero;

        if (isSelected)
        {
            GameObject overlayGO = new GameObject("Overlay", typeof(RectTransform));
            overlayGO.transform.SetParent(icon.transform, false);

            Image overlay = overlayGO.AddComponent<Image>();
            overlay.color = new Color(0.3f, 0.3f, 0.1f, 0.3f);

            RectTransform overlayRect = overlayGO.GetComponent<RectTransform>();
            overlayRect.anchorMin = Vector2.zero;
            overlayRect.anchorMax = Vector2.one;
            overlayRect.offsetMin = Vector2.zero;
            overlayRect.offsetMax = Vector2.zero;

            overlayGO.transform.SetAsFirstSibling();
        }

        if (item != null)
        {
            Image image = icon.AddComponent<Image>();
            image.sprite = item.Icon;
            image.preserveAspect = true;

            if (item.count > 1 && !item.IsWeapon())
            {
                GameObject textGO = new GameObject("CountText", typeof(RectTransform));
                textGO.transform.SetParent(icon.transform, false);

                RectTransform textRect = textGO.GetComponent<RectTransform>();
                textRect.anchorMin = new Vector2(0.5f, 0.5f);
                textRect.anchorMax = new Vector2(0.5f, 0.5f);
                textRect.sizeDelta = Vector2.zero;
                textRect.anchoredPosition = new Vector2(14, -14);

                Text countText = textGO.AddComponent<Text>();
                countText.alignment = TextAnchor.LowerRight;
                countText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                countText.color = Color.black;
                countText.fontSize = 12;
                countText.horizontalOverflow = HorizontalWrapMode.Overflow;
                countText.verticalOverflow = VerticalWrapMode.Overflow;
                countText.text = item.count.ToString();
            }
        }

        return icon;
    }
}
