using System;
using UnityEngine;
using UnityEngine.Events;

public class EnterCollisionComponent : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private bool isLooping;
    [SerializeField] private EnterEvent action;  
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(_tag)) action?.Invoke(other.gameObject);
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if(isLooping && other.gameObject.CompareTag(_tag)) action?.Invoke(other.gameObject);
    }
    [Serializable]
    public class EnterEvent : UnityEvent<GameObject> { }
}
