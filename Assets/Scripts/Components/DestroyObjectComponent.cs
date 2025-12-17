using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DestroyObjectComponent : MonoBehaviour
{
    [SerializeField] private float latency = 0.1f;
    [SerializeField] private bool destroyAtSpawn = false;
    [SerializeField] private UnityEvent onDestroy;
    private void Start()
    {
        if (destroyAtSpawn) DestroyObject();
    }
    public void DestroyObject() => StartCoroutine(DestroyObjectCoroutine());
    private IEnumerator DestroyObjectCoroutine()
    {
        yield return new WaitForSeconds(latency);
        Destroy(gameObject);
        onDestroy?.Invoke();
    }
}
