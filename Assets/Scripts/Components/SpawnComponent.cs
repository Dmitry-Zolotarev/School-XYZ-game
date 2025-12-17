using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    public Vector3 spawnOffset = Vector3.zero;
    public GameObject prefab;
    
    public void Spawn()
    {
        var spawnedObject = Instantiate(prefab, transform.position, Quaternion.identity);
        spawnedObject.transform.localScale = transform.lossyScale;
        spawnedObject.transform.position += spawnOffset;
        spawnedObject.SetActive(true);
    }
}
