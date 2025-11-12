using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    [HideInInspector] public bool isPicked = false;
    [SerializeField] private float rotationSpeed = 1f;

    private void FixedUpdate()
    {
        if(!isPicked) transform.Rotate(0, rotationSpeed, 0);
    }
}
