using UnityEngine;
using UnityEngine.Events;



public class ExitCollisionComponent : MonoBehaviour
{
    public string targetTag;
    [SerializeField] private EventAction action = new EventAction();
    private void OnCollisionExit2D(Collision2D other) => Exit(other.gameObject);
    private void OnTriggerExit2D(Collider2D other) => Exit(other.gameObject);
    private void Exit(GameObject obj) 
    {
        if (string.IsNullOrEmpty(targetTag) || obj.CompareTag(targetTag)) action?.Invoke(obj);
    }
    [System.Serializable]
    public class EventAction : UnityEvent<GameObject> { }
}
