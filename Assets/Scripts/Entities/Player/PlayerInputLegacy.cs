using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputLegacy : MonoBehaviour
{
    [SerializeField] private GameObject statsWindow;
    private PlayerController player;
    private Inventory inventory;
    private HPComponent health;
    private void Start()
    {
        player = GetComponent<PlayerController>();
        inventory = GetComponent<Inventory>();
        health = GetComponent<HPComponent>();
        Cursor.visible = false;
    }
    private void Update()
    {
        if(health.isDead || (statsWindow != null && statsWindow.activeSelf)) return;

        player.SetDirection(Input.GetAxisRaw("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space)) player.Jump();
        if (Input.GetKeyDown(KeyCode.LeftShift)) player.Dash();
        if (Input.GetKeyDown(KeyCode.E)) player.Interact();
        if (Input.GetMouseButtonDown(0)) player.Attack();
        if (Input.mouseScrollDelta.y != 0) inventory.ScrollItem(Input.mouseScrollDelta.y);


        if (Input.GetKeyDown(KeyCode.Alpha1)) inventory.SelectItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) inventory.SelectItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) inventory.SelectItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) inventory.SelectItem(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) inventory.SelectItem(4);
    }
}
