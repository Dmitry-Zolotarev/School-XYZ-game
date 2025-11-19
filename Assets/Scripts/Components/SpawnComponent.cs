using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    public Transform spawnPosition;
    public EntityController controller;
    public Vector3 spawnOffset = Vector3.zero;
    public GameObject prefab;
    public float shootForce = 10;
    public void Spawn()
    {
        var spawnedObject = Instantiate(prefab, spawnPosition.position, Quaternion.identity);
        spawnedObject.transform.localScale = spawnPosition.transform.lossyScale;
        spawnedObject.transform.position += spawnOffset;
        spawnedObject.SetActive(true);
        Launch(spawnedObject);
    }
    private void Launch(GameObject obj)
    {
        var rigidbody = obj.GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            Vector2 shootDirection = Vector2.right * transform.localScale.x + Vector2.up / 5f;
            rigidbody.AddForce(shootDirection * shootForce, ForceMode2D.Impulse);
        }
    }
}
