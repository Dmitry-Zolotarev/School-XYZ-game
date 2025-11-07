using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private UnityEvent _action;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _action?.Invoke();
    }
}
