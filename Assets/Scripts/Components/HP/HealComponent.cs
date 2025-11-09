using UnityEngine;

public class HealComponent : MonoBehaviour
{
    [SerializeField] private int healValue;
    public void Heal(GameObject target)
    {
        var HP = target.GetComponent<HPComponent>();
        if (HP != null && HP.Heal(healValue) > 0) Destroy(gameObject);     
    }
}
