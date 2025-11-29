using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class PlayerInput : MonoBehaviour
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

    public void InputAD(InputAction.CallbackContext context)
    {
        if(!health.isDead) player.SetDirection(context.ReadValue<float>());
    }
    public void InputJump(InputAction.CallbackContext context)
    {
        if (context.performed && !health.isDead) player.Jump();
    }
    public void InputDash(InputAction.CallbackContext context)
    {
        if (context.performed && !health.isDead) player.Dash();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && !health.isDead) player.Interact();
    }

    public void AttackClick(InputAction.CallbackContext context)
    {
        if (context.performed && !health.isDead) player.Attack();
    }

    public void MouseScroll(InputAction.CallbackContext context)
    {
        if (context.performed) inventory.ScrollItem(context.ReadValue<float>());
    }

    public void SelectItem(InputAction.CallbackContext context)
    {
        if (context.performed) inventory.SelectItem((int)context.ReadValue<float>());
    }
}
