using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HPComponent : MonoBehaviour
{
    public UnityEvent onDamage, onHeal, onDie;
    [HideInInspector] public int HP;
    private WalletComponent wallet;
    public int maxHP;
    
    private void Start() {
        HP = maxHP;
        wallet = GetComponent<WalletComponent>();
    }
    public void ApplyDamage(int damage)
    {
        HP -= damage;
        HP = Mathf.Max(HP, 0);
        onDamage?.Invoke();

        if (HP <= 0) onDie?.Invoke();

        if (wallet != null && HP <= maxHP / 2) wallet.DropAllCoins();
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