using UnityEngine;
using UnityEngine.Events;

public class HPComponent : MonoBehaviour
{
    public UnityEvent onDamage, onHeal, onDie;
    [HideInInspector] public int HP;
    public int XP_for_murder = 50;
    public int maxHP = 100;
    
    private void Awake() {
        HP = maxHP;
 
    }
    public void ApplyDamage(int damage)
    {
        HP -= damage;
        HP = Mathf.Max(HP, 0);


        if (HP > 0) onDamage?.Invoke();
        else Die();
    }
    public void Die()
    {
        if(tag == "Enemy")
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var leveling = player.GetComponent<Leveling>();
            leveling.GetXP(XP_for_murder);
        }   
        onDie?.Invoke();
    }
    public int Heal(int healing)
    {
        var wasHP = HP;
        HP += healing;
        if (HP > maxHP) HP = maxHP;
        onHeal?.Invoke();
        return HP - wasHP;
    }
    public void UpdateMaxHP(int increase)
    {
        maxHP += increase;
        HP = maxHP;
    }
}