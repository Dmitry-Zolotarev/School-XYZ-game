using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineConfiner2D))]
public class FindConfinerComponent : MonoBehaviour
{
    private CinemachineConfiner2D confiner;
    private int attempts = 0;

    private void Start()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
    }
    private void FixedUpdate()
    {
        if (confiner.BoundingShape2D == null && attempts == 0) 
        {
            var obj = GameObject.FindGameObjectWithTag("Confiner");
            var confinerCollider = obj.GetComponent<Collider2D>();
            if (confinerCollider != null) 
            {
                confiner.BoundingShape2D = confinerCollider;
                attempts = 0;
            } 
            else attempts++;
        }     
    }
}
