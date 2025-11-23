using JetBrains.Annotations;
using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int size = 15;
    [HideInInspector] public int selectedSlot = 0;

    [SerializeField] private Vector2 itemOffset = new Vector2(0, 0.5f);
    private Animator animator;

    private AttackComponent attack;
    public Item[] Items;
    public Action ItemsChanged;
    public Transform itemHand;
    private void Start()
    {
        Items = new Item[size];
        animator = GetComponent<Animator>();
        attack = GetComponent<AttackComponent>();
        SelectItem(0);
    }
    private void FixedUpdate()
    {
        if (Items[selectedSlot] != null && animator != null) Items[selectedSlot].Render(itemHand, itemOffset, transform.localScale);
    }
    public void SelectItem(int i)
    {
        while (i < 0) i += size;
        if (i >= size) i %= size;

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
            if (Items[i] != null && item.IsWeapon() && Items[i].name == item.name) return false;

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
        attack.attackMode = 0;
        attack.damageIncrease = 1;
        attack.armRadiusIncrease = 0f;
        attack.attackCooldownScale = 1f;

        if (item is Melee meleeWeapon)
        {
            attack.damageIncrease = meleeWeapon.damageIncrease;
            attack.armRadiusIncrease = meleeWeapon.armRadiusIncrease;
        }
        else if (item is Range rangeWeapon)
        {
            attack.attackMode = 1;
            rangeWeapon.chargeProjectile();
            attack.SetProjectile(rangeWeapon.projectile);
            attack.attackCooldownScale = 1f / rangeWeapon.fireRate;
        }
        else if (item is RayGun rayGun)
        {
            attack.attackMode = 2;
            attack.damageIncrease = rayGun.damageIncrease;
            attack.armRadiusIncrease = rayGun.rangeIncrease;
            attack.attackCooldownScale = 1f / rayGun.fireRate;
        }
    }
}