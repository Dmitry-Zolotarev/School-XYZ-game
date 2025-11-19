using UnityEngine;
using UnityEngine.Events;



public class EnterCollisionComponent : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private bool isLooping;
    [SerializeField] private EventAction action = new EventAction();  
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(targetTag)) action?.Invoke(other.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(targetTag)) action?.Invoke(other.gameObject);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (isLooping && other.gameObject.CompareTag(targetTag)) action?.Invoke(other.gameObject);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (isLooping && other.gameObject.CompareTag(targetTag)) action?.Invoke(other.gameObject);
    }
    [System.Serializable]
    public class EventAction : UnityEvent<GameObject> { }
}
