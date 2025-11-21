using UnityEngine;

public class DestroyObjectComponent : MonoBehaviour
{
    [SerializeField] private float latency = 0.1f;
    public void DestroyObject()
    {
        Destroy(gameObject, latency);
    }
}
