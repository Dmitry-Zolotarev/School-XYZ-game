using System.Collections;
using UnityEngine;

public class LevelUPLabelComponent : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2f;
    private void Start() => StartCoroutine(StartLifeTimer());
    private void Update() => transform.position += Vector3.up * Time.deltaTime;
    private IEnumerator StartLifeTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
