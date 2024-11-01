using System;
using UnityEngine;

public class InputService : IInputProvider
{
    public InputSystem PlayerInput { get; private set; }
    public ButtonsInputService ButtonsController { get; private set; }

    public InputService()
    {
        Initialize();
    }

    public void Initialize()
    {
        PlayerInput = new InputSystem();
        PlayerInput.Enable();
        
        ButtonsController = new ButtonsInputService(PlayerInput);
    }

    public Vector2 GetMovementVector()
    {
        return PlayerInput.Player.Move.ReadValue<Vector2>();
    }
    
    public void DisableMovement()
    {
        PlayerInput.Player.Move.Disable();
    }
}