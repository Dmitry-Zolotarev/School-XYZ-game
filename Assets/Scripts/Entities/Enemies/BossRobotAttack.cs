using System.Collections.Generic;
using UnityEngine;

public class BossRobotAttack : AttackComponent
{
    [SerializeField] private List<SpawnComponent> guns;
    public void GunsAttack()
    {
        foreach (var gun in guns) 
        {
            gun.prefab = projectile;
            gun.Spawn();
        } 
    }
}
