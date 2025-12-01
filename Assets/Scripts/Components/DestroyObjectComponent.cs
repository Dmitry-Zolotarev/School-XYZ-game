using UnityEngine;

public class DestroyObjectComponent : MonoBehaviour
{
    [SerializeField] private float latency = 0.1f;
    [SerializeField] private bool destroyAtSpawn = false;
    private void Start()
    {
        if (destroyAtSpawn) DestroyObject();
    }
    public void DestroyObject()
    {
        Destroy(gameObject, latency);
    }
}
