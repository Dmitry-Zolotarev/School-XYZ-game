using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Range")]
public class Range : Item
{
    public float attackCooldownScale = 1f;
    public float shootForce = 10f;
    public GameObject projectile;

}
