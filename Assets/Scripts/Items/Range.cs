using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Melee")]
public class Range : Item
{
    public int damage = 10;
    public float projectileForce = 10f;
    public GameObject projectile;
}
