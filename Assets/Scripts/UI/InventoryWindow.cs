using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] public int hotbarSize = 15;

    private GridLayoutGroup grid;
    private List<GameObject> drawIcons = new List<GameObject>();
    private bool subscribed = false;

    private int? draggingIndex = null;
    private int lastDragSlot = -1;
    private bool isMouseDown = false;

    private void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        if (inventory == null) TryBindInventory();
    }
    private void Update()
    {
        if (inventory == null) TryBindInventory();
        if (isMouseDown && !Mouse.current.leftButton.isPressed) EndDrag();
    }
    private void TryBindInventory()
    {
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
        ReDraw();
    }

    private void OnDestroy()
    {
        if (inventory != null && subscribed) inventory.ItemsChanged -= ReDraw;
    }

    public void ReDraw()
    {
        try
        {
            foreach (var icon in drawIcons) Destroy(icon);
            drawIcons.Clear();

            int count = Mathf.Min(hotbarSize, inventory.size);

            int selectedSlotInUI = Mathf.Clamp(inventory.selectedSlot, 0, count - 1);

            for (int i = 0; i < count; i++)
            {
                int slot = i;
                GameObject icon = CreateSlot(inventory.Items[i], slot == selectedSlotInUI);
                AddPointerEvents(icon, slot);
                drawIcons.Add(icon);
            }
        }
        catch { }      
    }

    private GameObject CreateSlot(Item item, bool selected)
    {
        GameObject obj = new GameObject("Slot", typeof(RectTransform), typeof(Image));
        obj.transform.SetParent(grid.transform, false);

        Image img = obj.GetComponent<Image>();
        img.raycastTarget = true;

        if (item != null)
        {
            img.sprite = item.Icon;
            img.color = Color.white;

            if (item.count > 1 && !item.IsWeapon())
            {
                GameObject textGO = new GameObject("CountText", typeof(RectTransform));
                textGO.transform.SetParent(obj.transform, false);

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
        else img.color = new Color(0, 0, 0, 0.1f);

        if (selected)
        {
            GameObject overlay = new GameObject("Selected", typeof(RectTransform), typeof(Image));
            overlay.transform.SetParent(obj.transform, false);

            var overlayImage = overlay.GetComponent<Image>();
            overlayImage.color = new Color(0.3f, 0.3f, 0.1f, 0.3f);

            RectTransform overlayRect = overlay.GetComponent<RectTransform>();
            overlayRect.anchorMin = Vector2.zero;
            overlayRect.anchorMax = Vector2.one;
            overlayRect.offsetMin = overlayRect.offsetMax = Vector2.zero;
        }
        return obj;
    }

    private void AddPointerEvents(GameObject icon, int slotIndex)
    {
        EventTrigger trigger = icon.AddComponent<EventTrigger>();

        var down = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        down.callback.AddListener((_) => OnPointerDown(slotIndex));
        trigger.triggers.Add(down);

        var enter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        enter.callback.AddListener((_) => OnEnter(slotIndex));
        trigger.triggers.Add(enter);

        var up = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        up.callback.AddListener((_) => EndDrag());
        trigger.triggers.Add(up);
    }

    private void OnPointerDown(int slot)
    {
        if (inventory == null) return;

        isMouseDown = true;
        inventory.SelectItem(slot);

        if (inventory.Items[slot] == null)
        {
            draggingIndex = null;
            lastDragSlot = -1;
            return;
        }
        draggingIndex = slot;
        lastDragSlot = slot;
    }
    private void OnEnter(int targetSlot)
    {
        if (isMouseDown && draggingIndex != null && inventory != null && targetSlot != lastDragSlot)
        {
            SwapSlots(targetSlot);
            lastDragSlot = targetSlot;
            inventory.SelectItemWithoutInvoke(targetSlot);
            inventory.ItemsChanged?.Invoke();
        }     
    }
    private void SwapSlots(int targetSlot)
    {
        Item t = inventory.Items[targetSlot];
        inventory.Items[targetSlot] = inventory.Items[lastDragSlot];
        inventory.Items[lastDragSlot] = t;
    }
    private void EndDrag()
    {
        isMouseDown = false;
        draggingIndex = null;
        lastDragSlot = -1;
        if (inventory != null) inventory.SelectItemWithoutInvoke(inventory.selectedSlot);
    }
}
