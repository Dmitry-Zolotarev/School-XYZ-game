using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(EntityController))]
public class PlayerInput : MonoBehaviour
{
    private EntityController player;
    private void Start()
    {
        player = GetComponent<EntityController>();
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
}
