using System;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int Size = 10;
    [HideInInspector]public int selectedSlot = 0;
    [SerializeField] private Transform itemHand;
    [SerializeField] private Vector2 itemOffset = new Vector2(0, 0.5f);
    private Animator animator;
    private PlayerController playerController;
    public Item[] Items;
    public Action ItemsChanged;
    private void Start()
    {
        Items = new Item[Size];
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        if (Items[selectedSlot] != null && animator != null) Items[selectedSlot].Render(itemHand, itemOffset);     
    }
    public void ScrollItem(float scroll)
    {
        if (scroll != 0f)
        {
            if (scroll > 0 && selectedSlot < Size) selectedSlot++;
            if (scroll < 0 && selectedSlot > 0) selectedSlot--;
            CheckMeleeWeapon(Items[selectedSlot]);
            Items[selectedSlot].Select();
            ItemsChanged?.Invoke();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        var pickup = other.GetComponent<ItemPickup>();
        if (pickup == null || pickup.item == null) return;
        pickup.isPicked = true;
        if (PickItem(pickup.item)) Destroy(other.gameObject);
    }
    public Item getItem()
    {
        return Items[selectedSlot];
    }

    public bool PickItem(Item item)
    {
        if (item == null) return false;

        Item newItem = Instantiate(item);

        // Привязываем модель к игроку
        newItem.Attach(itemHand);

        // Проверка на стак
        for (int i = 0; i < Size; i++)
        {
            if (Items[i] != null && Items[i].Name == newItem.Name)
            {
                if (Items[i].isStackable)
                {
                    Items[i].count += newItem.count;
                    ItemsChanged?.Invoke();
                    return true;
                }
                else return false;
            }
        }

        // Ищем пустой слот
        for (int i = 0; i < Size; i++)
        {
            if (Items[i] == null)
            {
                Items[i] = newItem;

                CheckMeleeWeapon(newItem);
                Items[i].Select();
                selectedSlot = i;
                ItemsChanged?.Invoke();
                return true;
            }
        }

        Debug.Log("📦 Инвентарь полон!");
        return false;
    }
    private void CheckMeleeWeapon(Item item)
    {
        if (item is Melee)
        {
            Melee weapon = item as Melee;
            if (Items[selectedSlot] != null) Items[selectedSlot].Deselect();
            playerController.damageIncrease = weapon.damageIncrease;
            playerController.armRadiusIncrease = weapon.armRadiusIncrease;
        }
        else {
            playerController.damageIncrease = 1;
            playerController.armRadiusIncrease = 0f;
        }
        
    }
}
