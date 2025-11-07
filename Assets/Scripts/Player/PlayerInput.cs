using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
{
    private PlayerController player;
    private void Start()
    {
        player = GetComponent<PlayerController>();
    }
    public void InputAD(InputAction.CallbackContext context) 
    {
        player.SetDirection(context.ReadValue<float>());
    }
    public void InputJump(InputAction.CallbackContext context)
    {
        if (context.performed) player.Jump();
    }
}
