using UnityEngine;
using UnityEngine.Events;

public class HPComponent : MonoBehaviour
{
    public UnityEvent onDamage, onHeal, onDie;
    [HideInInspector] public int HP;
    public int maxHP = 100;
    
    private void Awake() {
        HP = maxHP;
 
    }
    public void ApplyDamage(int damage)
    {
        HP -= damage;
        HP = Mathf.Max(HP, 0);


        if (HP > 0) onDamage?.Invoke();
        else onDie?.Invoke();
    }
    public int Heal(int healing)
    {
        var wasHP = HP;
        HP += healing;
        if (HP > maxHP) HP = maxHP;
        onHeal?.Invoke();
        return HP - wasHP;
    }
}