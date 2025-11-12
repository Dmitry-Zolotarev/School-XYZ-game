using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    private PlayerController player;
    private Inventory inventory;
    [SerializeField]private GameObject pauseMenu;
    private void Start()
    {
        player = GetComponent<PlayerController>();
        Cursor.visible = false;
    }
    public void InputAD(InputAction.CallbackContext context) 
    {
        player.SetDirection(context.ReadValue<float>());
    }
    public void InputJump(InputAction.CallbackContext context)
    {
        if (context.performed) player.Jump();
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed) player.Interact();
    }
    public void AttackClick(InputAction.CallbackContext context)
    {
        if (context.performed) player.Attack();
    }
    public void MouseScroll(InputAction.CallbackContext context)
    {
        if (context.performed) inventory.ScrollItem(context.ReadValue<float>());   
    }
}
