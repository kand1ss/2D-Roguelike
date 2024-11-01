using UnityEngine;

public interface IInputProvider
{
    void Initialize();
    Vector2 GetMovementVector();
    void DisableMovement();
    ButtonsInputService ButtonsController { get; }
}