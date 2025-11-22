using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int size = 3, hotbarSize = 3;
    [HideInInspector]public int selectedSlot = 0;
    
    [SerializeField] private Vector2 itemOffset = new Vector2(0, 0.5f);
    private Animator animator;
    
    private EntityController entityController;
    public Item[] Items;
    public Action ItemsChanged;
    public Transform itemHand;
    private void Start()
    {
        if (hotbarSize > size) hotbarSize = size;
        Items = new Item[size];
        animator = GetComponent<Animator>();
        entityController = GetComponent<EntityController>();
        SelectItem(0);
    }
    private void FixedUpdate()
    {
        if (Items[selectedSlot] != null && animator != null) Items[selectedSlot].Render(itemHand, itemOffset, transform.localScale);     
    }
    public void SelectItem(int i)
    {
        while (i < 0) i += hotbarSize;
        if (i >= hotbarSize) i %= hotbarSize;

        var old = Items[selectedSlot];
        if (old != null) old.Deselect();

        selectedSlot = i;
        var item = Items[selectedSlot];
        CheckWeapon(item);
        ItemsChanged?.Invoke();
        if (item != null) item.Select();
    }

    public void ScrollItem(float delta)
    {
        if (delta > 0) SelectItem(selectedSlot + 1);
        if (delta < 0) SelectItem(selectedSlot - 1);
    }
    public bool PickItem(Item item)
    {
        if (item == null) return false;

        Item newItem = Instantiate(item);
        newItem.Attach(itemHand);

        for (int i = 0; i < size; i++)
        {
            if (Items[i] != null && Items[i].Name == newItem.Name)
            {
                Items[i].count += newItem.count;
                ItemsChanged?.Invoke();
                return true;
            }
            else if (Items[i] == null)
            {
                Items[i] = newItem;
                SelectItem(i);
                return true;
            }
        }
        return false;
    }
    private void CheckWeapon(Item item)
    {
        entityController.attackMode = 0;
        entityController.damageIncrease = 1;
        entityController.armRadiusIncrease = 0f;
        entityController.attackCooldownScale = 1f;

        if (item is Melee meleeWeapon)
        {
            entityController.damageIncrease = meleeWeapon.damageIncrease;
            entityController.armRadiusIncrease = meleeWeapon.armRadiusIncrease;
        }
        else if (item is Range rangeWeapon)
        {
            entityController.attackMode = 1;
            rangeWeapon.chargeProjectile();
            entityController.SetProjectile(rangeWeapon.projectile);
            entityController.attackCooldownScale = 1f / rangeWeapon.fireRate;
        }
        else if (item is RayGun rayGun)
        {
            entityController.attackMode = 2;

            entityController.damageIncrease = rayGun.damageIncrease;
            entityController.armRadiusIncrease = rayGun.rangeIncrease;
            entityController.attackCooldownScale = 1f / rayGun.fireRate;
        }
    }
}
