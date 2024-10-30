using UnityEngine;

public class InputService : MonoBehaviour
{
    public static InputService Instance { get; private set; }

    public InputSystem PlayerInput { get; private set; }
    public static ButtonsInputService ButtonsController { get; private set; }

    private void Awake()
    {
        ButtonsController = GetComponent<ButtonsInputService>();
        
        PlayerInput = new InputSystem();
        PlayerInput.Enable();

        Instance = this;
    }

    public Vector2 GetMovementVector()
    {
        return PlayerInput.Player.Move.ReadValue<Vector2>();
    }
    
    public void DisableMovement()
    {
        PlayerInput.Player.Move.Disable();
    }

    public Vector2 GetCursorPositionInWorldPoint()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return cursorPosition;
    }
}