using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Melee")]
public class Melee : Item
{
    public int damageIncrease = 2;
    public float armRadiusIncrease = 0.5f;
}
