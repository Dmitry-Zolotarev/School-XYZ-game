using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Range")]
public class Range : Item
{
    public float fireRate = 0.5f, shootForce = 10f, shootAngle = 15f;
    public GameObject projectile;

    public void chargeProjectile()
    {
        var charge = projectile.GetComponent<ProjectileComponent>();
        
        if (charge != null) {
            charge.shootForce = shootForce;
            charge.shootAngle = shootAngle;
        }
    }
}
