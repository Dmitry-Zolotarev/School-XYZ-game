using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/RayGun")]
public class RayGun : Item
{
    public int damageIncrease = 2, rangeIncrease = 10;
    public float fireRate = 0.5f;
}
