using UnityEngine;
using UnityEngine.Events;

public class Leveling : MonoBehaviour
{
    [HideInInspector]public int XP = 0, level = 1, currentXPforLevelUP;
    [SerializeField] private int xp_forLevelUP = 1000, HPIncrease = 10, damageIncrease = 1;
    private AttackComponent attack;
    private HPComponent health;
    public UnityEvent OnLevelUP;
    void Start()
    {       
        currentXPforLevelUP = xp_forLevelUP;
        health = GetComponent<HPComponent>();
        attack = GetComponent<AttackComponent>();
    }
    // Update is called once per frame
    public void GetXP(int amount)
    {
        XP += amount;
        while (XP >= currentXPforLevelUP) LevelUP();
    }
    private void LevelUP()
    {
        level++;
        XP %= currentXPforLevelUP;
        if (health != null) health.UpdateMaxHP(HPIncrease);
        if (attack != null) attack.damage += damageIncrease;
        currentXPforLevelUP = xp_forLevelUP * level;
        OnLevelUP?.Invoke();
    }
}
