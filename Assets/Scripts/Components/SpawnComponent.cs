using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    public Transform spawnPosition;
    public Vector3 spawnOffset = Vector3.zero;
    [HideInInspector]public GameObject prefab;
    
    public void Spawn()
    {
        var spawnedObject = Instantiate(prefab, spawnPosition.position, Quaternion.identity);
        spawnedObject.transform.localScale = spawnPosition.transform.lossyScale;
        spawnedObject.transform.position += spawnOffset;
        spawnedObject.SetActive(true);
    }
}
