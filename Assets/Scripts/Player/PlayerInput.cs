using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    private PlayerController player;
    private Inventory inventory;
    private PauseComponent pauseComponent;
    private void Start()
    {
        player = GetComponent<PlayerController>();
        pauseComponent = GetComponent<PauseComponent>();
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
    public void Pause(InputAction.CallbackContext context)
    {
        if (context.performed) pauseComponent?.Pause();
    }
    public void MouseScroll(InputAction.CallbackContext context)
    {
        if (context.performed) inventory.ScrollItem(context.ReadValue<float>());   
    }
}
