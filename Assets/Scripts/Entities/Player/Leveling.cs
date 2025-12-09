using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Leveling : MonoBehaviour
{
    [HideInInspector]public int XP = 0, level = 1, currentXPforLevelUP;
    [SerializeField] private int xp_forLevelUP = 1000, HPIncrease = 10, damageIncrease = 1;
    [SerializeField] private GameObject levelUPLabel;
    private AttackComponent attack;   
    public UnityEvent OnLevelUP;
    private HPComponent health;
    private PerksComponent perks;
    void Start()
    {       
        currentXPforLevelUP = xp_forLevelUP;
        health = GetComponent<HPComponent>();
        attack = GetComponent<AttackComponent>();
        perks = GetComponent<PerksComponent>();
    }
    // Update is called once per frame
    public void GetXP(int amount)
    {
        XP += amount;
        while (XP >= currentXPforLevelUP) StartCoroutine(LevelUP());
    }
    private IEnumerator LevelUP()
    {
        level++;
        perks.perkScore++;
        XP -= currentXPforLevelUP;     
        if (attack != null) attack.damage += damageIncrease;
        currentXPforLevelUP = xp_forLevelUP * level;
        Instantiate(levelUPLabel, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.2f);
        if (health != null) health.UpdateMaxHP(HPIncrease);
        OnLevelUP?.Invoke();
    }
}
