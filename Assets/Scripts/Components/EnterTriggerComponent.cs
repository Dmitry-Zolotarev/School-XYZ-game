using UnityEngine;
using UnityEngine.Events;

public class EnterTriggerComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private bool isLooping;
    [SerializeField] private EventTrigger action = new EventTrigger();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(_tag)) action?.Invoke(other.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isLooping && other.gameObject.CompareTag(_tag)) action?.Invoke(other.gameObject);
    }
    [System.Serializable]
    public class EventTrigger : UnityEvent<GameObject> { }
}
