using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(GridLayoutGroup))]
public class InventoryWindow : MonoBehaviour
{
    
    [SerializeField] private Inventory inventory;
    [SerializeField] private GameObject itemIconPrefab; 
    private List<GameObject> drawIcons = new List<GameObject>();
    private GridLayoutGroup grid;
    

    private void Start()
    {
        if (inventory == null) inventory = FindAnyObjectByType<Inventory>();
        grid = GetComponent<GridLayoutGroup>();
        inventory.ItemsChanged += ReDraw;
    }
    private void OnDestroy()
    {
        if (inventory != null) inventory.ItemsChanged -= ReDraw;
    }

    private void ReDraw()
    {
        if (grid == null) return;

        foreach (var icon in drawIcons) Destroy(icon);

        drawIcons.Clear();

        int i = 0;
        foreach (var item in inventory.Items)
        {
            if (i >= inventory.hotbarSize || item == null) break;

            GameObject icon = CreateSimpleIcon(item, i == inventory.selectedSlot);

            Image img = icon.GetComponent<Image>();
            if (img != null) img.sprite = item.Icon;          

            Text txt = icon.GetComponentInChildren<Text>();

            if (txt != null) txt.text = (item.isStackable && item.count > 1) ? item.count.ToString() : "";

            drawIcons.Add(icon);
            i++;
        }
    }

    private GameObject CreateSimpleIcon(Item item, bool isSelected)
    {
        GameObject icon = new GameObject("ItemIcon", typeof(RectTransform));
        icon.transform.SetParent(grid.transform, false);

        RectTransform iconRect = icon.GetComponent<RectTransform>();
        iconRect.anchorMin = Vector2.zero;
        iconRect.anchorMax = Vector2.one;
        iconRect.offsetMin = Vector2.zero;
        iconRect.offsetMax = Vector2.zero;

        // --- Квадрат выделения (если выбран) ---
        if (isSelected)
        {
            GameObject overlayGO = new GameObject("Overlay", typeof(RectTransform));
            overlayGO.transform.SetParent(icon.transform, false);

            Image overlay = overlayGO.AddComponent<Image>();
            overlay.color = new Color(0.3f, 0.3f, 0.1f, 0.2f);  // прозрачный белый
            RectTransform overlayRect = overlayGO.GetComponent<RectTransform>();
            overlayRect.anchorMin = Vector2.zero;
            overlayRect.anchorMax = Vector2.one;
            overlayRect.offsetMin = Vector2.zero;
            overlayRect.offsetMax = Vector2.zero;

            // Отправляем overlay на задний план
            overlayGO.transform.SetAsFirstSibling();
        }

        // --- Иконка предмета ---Сделай Scale до 48x48 пикселей
        Image image = icon.AddComponent<Image>();     
        image.sprite = item.Icon;        
        image.preserveAspect = true;

        // Текст для количества
        GameObject textGO = new GameObject("CountText", typeof(RectTransform));
        textGO.transform.SetParent(icon.transform, false);

        RectTransform textRect = textGO.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        Text countText = textGO.AddComponent<Text>();
        countText.alignment = TextAnchor.LowerRight;
        countText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        countText.color = Color.black;
        countText.raycastTarget = false;
        countText.fontSize = 18;
        countText.horizontalOverflow = HorizontalWrapMode.Overflow;
        countText.verticalOverflow = VerticalWrapMode.Overflow;
        if (item.isStackable && item.count > 1) countText.text = item.count.ToString();

        return icon;
    }
}
