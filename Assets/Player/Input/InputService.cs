using UnityEngine;
using UnityEngine.Events;

public class InputService : MonoBehaviour
{
    public static InputService Instance { get; private set; }

    private PlayerInputSystem playerInputSystem;
    public PlayerInputSystem PlayerInput => playerInputSystem;

    private void Awake()
    {
        playerInputSystem = new PlayerInputSystem();
        playerInputSystem.Enable();

        Instance = this;
    }

    public Vector2 GetMovementVector()
    {
        return playerInputSystem.Player.Move.ReadValue<Vector2>();
    }
    
    public void DisableMovement()
    {
        playerInputSystem.Player.Move.Disable();
    }

    public Vector2 GetCursorPositionInWorldPoint()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return cursorPosition;
    }
}
