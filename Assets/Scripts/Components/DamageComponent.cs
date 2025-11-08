using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] private int damage;
    public void SetDamage(GameObject target)
    {
        var HP = target.GetComponent<HPComponent>();
        if (HP != null) HP.ApplyDamage(damage);
    }
}
