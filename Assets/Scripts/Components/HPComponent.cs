using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HPComponent : MonoBehaviour
{
    
    [SerializeField] private int maxHP;
    [SerializeField] private TextMeshProUGUI HPLabel;
    [SerializeField] private UnityEvent onDamage;
    [SerializeField] private UnityEvent onDie;
    
    private int HP;
    private void Start() {
        HP = maxHP;
        UpdateLabel();
    } 
    public void ApplyDamage(int damage)
    {
        HP -= damage;
        onDamage?.Invoke();
        if (HP <= 0) onDie?.Invoke();
        else if(HP > maxHP) HP = maxHP;
        UpdateLabel();
    }
    private void UpdateLabel() => HPLabel?.SetText("♥ " + HP);
}