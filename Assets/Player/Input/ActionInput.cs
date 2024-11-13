using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ActionInput
{
    private InputSystem _inputSystem;
    
    public event UnityAction OnPotionUsed;

    public ActionInput(InputSystem inputSystem)
    {
        _inputSystem = inputSystem;
    }

    public void InitializeEvents()
    {
        _inputSystem.Player.UsePotion.performed += UsePotion;
    }

    public void FinalizeEvents()
    {
        _inputSystem.Player.UsePotion.performed -= UsePotion;
    }

    private void UsePotion(InputAction.CallbackContext obj)
    {
        OnPotionUsed?.Invoke();
    }
}