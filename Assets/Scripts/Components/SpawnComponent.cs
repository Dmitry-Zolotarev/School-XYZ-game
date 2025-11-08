using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Vector3 spawnOffset = Vector3.zero;
    public GameObject prefab;
    public void Spawn()
    {
        var spawnedObject = Instantiate(prefab, spawnPosition.position, Quaternion.identity);
        spawnedObject.SetActive(true);
        spawnedObject.transform.localScale = spawnPosition.transform.lossyScale;
        spawnedObject.transform.position += spawnOffset;
    }
}
