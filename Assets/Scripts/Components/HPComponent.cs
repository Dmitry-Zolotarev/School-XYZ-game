using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HPComponent : MonoBehaviour
{
    
    
    [SerializeField] private TextMeshProUGUI HPLabel;
    [SerializeField] private UnityEvent onDamage;
    [SerializeField] private UnityEvent onHeal;
    [SerializeField] private UnityEvent onDie;
    private WalletComponent wallet;
    public int maxHP;
    private int HP;
    private void Start() {
        HP = maxHP;
        wallet = GetComponent<WalletComponent>();
        UpdateLabel();
    }
    public void ApplyDamage(int damage)
    {
        HP -= damage;
        HP = Mathf.Max(HP, 0);
        onDamage?.Invoke();
        if (HP <= 0) onDie?.Invoke();
        if (wallet != null && HP <= maxHP / 2) wallet.DropAllCoins();
        UpdateLabel();
    }
    public void Heal(int healing)
    {
        HP += healing;
        if (HP > maxHP) HP = maxHP;
        onHeal?.Invoke();
        UpdateLabel();
    }
    private void UpdateLabel() => HPLabel?.SetText("♥ " + HP);
}