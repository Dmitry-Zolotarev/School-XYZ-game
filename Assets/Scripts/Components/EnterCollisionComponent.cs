using UnityEngine;
using UnityEngine.Events;



public class EnterCollisionComponent : MonoBehaviour
{
    public string targetTag;
    [SerializeField] private bool isLooping;
    [SerializeField] private float loopDelay;
    [SerializeField] private EventAction action = new EventAction();
    private float lastActionTime;
    private void OnCollisionEnter2D(Collision2D other) => Enter(other.gameObject);
    private void OnTriggerEnter2D(Collider2D other) => Enter(other.gameObject);
    private void OnCollisionStay2D(Collision2D other) => Stay(other.gameObject);
    private void OnTriggerStay2D(Collider2D other) => Stay(other.gameObject);
    private void Enter(GameObject obj) 
    {
        if (string.IsNullOrEmpty(targetTag) || obj.CompareTag(targetTag)) {
            action?.Invoke(obj);
            lastActionTime = Time.time;
        }
    }
    private void Stay(GameObject obj)
    {
        if (isLooping && Time.time > lastActionTime + loopDelay && (string.IsNullOrEmpty(targetTag) || obj.CompareTag(targetTag))) 
        {
            lastActionTime = Time.time;
            action?.Invoke(obj);
        }
    }
    [System.Serializable]
    public class EventAction : UnityEvent<GameObject> { }
}
