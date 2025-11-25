using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInputLegacy : MonoBehaviour
{
    private PlayerController player;
    private Inventory inventory;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        inventory = GetComponent<Inventory>();
        Cursor.visible = false;
    }

    private void Update()
    {
        player.SetDirection(Input.GetAxisRaw("Horizontal"));

        if (Input.GetKeyDown(KeyCode.Space)) player.Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift)) player.Dash();

        if (Input.GetKeyDown(KeyCode.E)) player.Interact();


        if (Input.GetMouseButtonDown(0)) player.Attack();


        float scroll = Input.mouseScrollDelta.y;

        if (scroll != 0) inventory.ScrollItem(scroll);


        if (Input.GetKeyDown(KeyCode.Alpha1)) inventory.SelectItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) inventory.SelectItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) inventory.SelectItem(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) inventory.SelectItem(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) inventory.SelectItem(4);
    }
}
