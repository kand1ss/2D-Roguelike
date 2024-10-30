using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MagicInput
{
    private InputSystem _inputSystem;
    
    public event UnityAction OnSpellSwap;
    public event UnityAction OnMagicSwap;

    public MagicInput(InputSystem inputSystem)
    {
        _inputSystem = inputSystem;
    }

    public void InitializeEvents()
    {
        _inputSystem.Player.SwapSpell.performed += SwapSpell;
        _inputSystem.Player.SwapMagic.performed += SwapMagic;
    }

    public void FinalizeEvents()
    {
        _inputSystem.Player.SwapSpell.performed -= SwapSpell;
        _inputSystem.Player.SwapMagic.performed -= SwapMagic;
    }
    
    private void SwapSpell(InputAction.CallbackContext callbackContext) => OnSpellSwap?.Invoke();
    private void SwapMagic(InputAction.CallbackContext callbackContext) => OnMagicSwap?.Invoke();
}