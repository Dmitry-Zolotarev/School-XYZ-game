using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/RayGun")]
public class RayGun : Item
{
    public int damageIncrease = 2;
    public float fireRate = 0.5f;
    public LineRenderer rayModel;
}
