using UnityEngine;
using UnityEngine.Events;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private UnityEvent afterPick;
    private bool isPicked;
    private void FixedUpdate() => transform.Rotate(0, rotationSpeed, 0);
    public void PickItem(GameObject other)
    {
        var inventory = other.GetComponent<Inventory>();
        if (!isPicked && inventory != null && inventory.PickItem(item))
        {
            isPicked = true;
            afterPick.Invoke();
        }
    }
}
