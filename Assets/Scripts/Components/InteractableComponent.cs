using UnityEngine;
using UnityEngine.Events;

public class InteractableComponent : MonoBehaviour
{
    [SerializeField] private UnityEvent action;

    public void Interact()
    {
        action?.Invoke();
    }
}
