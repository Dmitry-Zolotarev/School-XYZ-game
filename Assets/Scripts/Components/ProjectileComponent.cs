using System.Collections;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float shootForce = 10f, shootAngle = 15f;
    void Start()
    {
        StartCoroutine(Launch());
    }
    private IEnumerator Launch()
    {
        var rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            Vector2 shootDirection;
            float angle = shootAngle * Mathf.Deg2Rad;
            shootDirection = new Vector2(Mathf.Cos(angle) * transform.localScale.x, Mathf.Sin(angle));

            rigidbody.AddForce(shootDirection * shootForce, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.1f);
        var collider = GetComponent<Collider2D>();
        collider.isTrigger = false;
    }
}
