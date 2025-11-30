using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HPComponent : MonoBehaviour
{
    public int maxHP = 100;
    public int XP_for_murder = 50;
    [HideInInspector] public bool isDead;
    [HideInInspector] public int HP;
    [SerializeField] private Image HPBar;
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
        UpdateUI();
        if (HP > 0) onDamage?.Invoke();
        else Die();
    }
    public int Heal(int healing)
    {
        var wasHP = HP;
        HP += healing;
        if (HP > maxHP) HP = maxHP;
        UpdateUI();
        onHeal?.Invoke();
        return HP - wasHP;
    }
    public void UpdateMaxHP(int increase)
    {
        maxHP += increase;
        HP = maxHP;
        UpdateUI();
    }
    public void Die()
    {
        if (tag == "Enemy" && !isDead)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var leveling = player.GetComponent<Leveling>();
            leveling?.GetXP(XP_for_murder);
        }
        UpdateUI();
        isDead = true;
        onDie?.Invoke();
    }
    private void UpdateUI()
    {
        if (HPBar != null) HPBar.fillAmount = HP / (float)maxHP;
        if (HPLabel != null) HPLabel.SetText($"♥ {HP}");
    }
}