using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class WeaponInput
{
    private InputSystem _inputSystem;
    
    public event UnityAction OnUseWeapon;
    public event UnityAction OnUseWeaponCanceled;
    
    public event UnityAction OnUseWeaponPressed;
    public event UnityAction OnUseWeaponPressedCanceled;
    
    public event UnityAction OnWeaponSwap;
    
    private bool isAttackButtonHold = false;
    private bool isStrongAttackButtonHold = false;

    public WeaponInput(InputSystem inputSystem)
    {
        _inputSystem = inputSystem;
    }

    public void InitializeEvents()
    {
        _inputSystem.Player.UseWeapon.performed += UseWeapon;
        _inputSystem.Player.UseWeapon.canceled += CancelUseWeapon;

        _inputSystem.Player.UseWeaponPressed.performed += StartStrongAttack;
        _inputSystem.Player.UseWeaponPressed.canceled += StopStrongAttack;
        
        _inputSystem.Player.SwapWeapon.performed += SwapWeapon;
    }

    public void FinalizeEvents()
    {
        _inputSystem.Player.UseWeapon.performed -= UseWeapon;
        _inputSystem.Player.UseWeapon.canceled -= CancelUseWeapon;

        _inputSystem.Player.UseWeaponPressed.performed -= StartStrongAttack;
        _inputSystem.Player.UseWeaponPressed.canceled -= StopStrongAttack;
        
        _inputSystem.Player.SwapWeapon.performed -= SwapWeapon;
    }
    
    public void Update()
    {
        StrongAttackButtonCheck();
        AttackButtonCheck();
    }

    private void StrongAttackButtonCheck()
    {
        if (isStrongAttackButtonHold)
            OnUseWeaponPressed?.Invoke();
    }

    private void AttackButtonCheck()
    {
        if (isAttackButtonHold)
            OnUseWeapon?.Invoke();
    }
    
    private void UseWeapon(InputAction.CallbackContext callbackContext) => isAttackButtonHold = true;

    private void CancelUseWeapon(InputAction.CallbackContext callbackContext)
    {
        OnUseWeaponCanceled?.Invoke();
        isAttackButtonHold = false;
    }

    private void StartStrongAttack(InputAction.CallbackContext callbackContext) => isStrongAttackButtonHold = true;

    private void StopStrongAttack(InputAction.CallbackContext callbackContext)
    {
        OnUseWeaponPressedCanceled?.Invoke();
        isStrongAttackButtonHold = false;
    }
    private void SwapWeapon(InputAction.CallbackContext callbackContext) => OnWeaponSwap?.Invoke();
}