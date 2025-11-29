using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HPComponent : MonoBehaviour
{
    
    [HideInInspector] public int HP;
    public int XP_for_murder = 50;
    public int maxHP = 100;
    public bool isDead;
    [SerializeField] private TextMeshProUGUI HPLabel;
    public UnityEvent onDamage, onHeal, onDie;
    
    private void Start()
    {
        HP = maxHP;
        HPLabel?.SetText($"♥ {HP}");
    }
    public void ApplyDamage(int damage)
    {
        HP -= damage;
        HP = Mathf.Max(HP, 0);
        HPLabel?.SetText($"♥ {HP}");
        if (HP > 0) onDamage?.Invoke();
        else Die();
    }
    public int Heal(int healing)
    {
        var wasHP = HP;
        HP += healing;
        if (HP > maxHP) HP = maxHP;

        HPLabel?.SetText($"♥ {HP}");
        onHeal?.Invoke();
        return HP - wasHP;
    }
    public void UpdateMaxHP(int increase)
    {
        HP = maxHP;
        maxHP += increase;  
        HPLabel?.SetText($"♥ {HP}");
        isDead = false;
    }
    public void Die()
    {
        if (tag == "Enemy" && !isDead)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var leveling = player.GetComponent<Leveling>();
            leveling?.GetXP(XP_for_murder);
        }
        HPLabel?.SetText($"♥ {HP = 0}");
        isDead = true;
        onDie?.Invoke();
    }
    
}