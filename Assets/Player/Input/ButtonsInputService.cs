using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class ButtonsInputService : MonoBehaviour
{
    public static ButtonsInputService Instance {  get; private set; }

    public event UnityAction OnUseWeapon;
    public event UnityAction OnUseWeaponCanceled;

    public event UnityAction OnSpellSwap;
    public event UnityAction OnMagicSwap;
    public event UnityAction OnWeaponSwap;

    public event UnityAction OnStrongAttack;
    public event UnityAction OnStrongAttackCanceled;
    
    private bool isAttackButtonHold = false;
    private bool isStrongAttackButtonHold = false;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SubscribeToEvents();
    }
    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }


    private void Update()
    {
        StrongAttackButtonCheck();
        AttackButtonCheck();
    }

    private void StrongAttackButtonCheck()
    {
        if(isStrongAttackButtonHold)
            OnStrongAttack?.Invoke();
    }
    private void AttackButtonCheck()
    {
        if(isAttackButtonHold)
            OnUseWeapon?.Invoke();
    }
    private void SubscribeToEvents()
    {
        InputService.Instance.PlayerInput.Player.UseWeapon.performed += UseWeapon;
        InputService.Instance.PlayerInput.Player.UseWeapon.canceled += CancelUseWeapon;

        InputService.Instance.PlayerInput.Player.Sword_StrongAttack.performed += StartStrongAttack;
        InputService.Instance.PlayerInput.Player.Sword_StrongAttack.canceled += StopStrongAttack;

        InputService.Instance.PlayerInput.Player.SwapSpell.performed += SwapSpell;
        InputService.Instance.PlayerInput.Player.SwapMagic.performed += SwapMagic;
        InputService.Instance.PlayerInput.Player.SwapWeapon.performed += SwapWeapon;
    }

    private void UnsubscribeFromEvents()
    {
        InputService.Instance.PlayerInput.Player.UseWeapon.performed -= UseWeapon;
        InputService.Instance.PlayerInput.Player.UseWeapon.canceled -= CancelUseWeapon;

        InputService.Instance.PlayerInput.Player.Sword_StrongAttack.performed -= StartStrongAttack;
        InputService.Instance.PlayerInput.Player.Sword_StrongAttack.canceled -= StopStrongAttack;

        InputService.Instance.PlayerInput.Player.SwapSpell.performed -= SwapSpell;
        InputService.Instance.PlayerInput.Player.SwapMagic.performed -= SwapMagic;
        InputService.Instance.PlayerInput.Player.SwapWeapon.performed -= SwapWeapon;
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
        OnStrongAttackCanceled?.Invoke();
        isStrongAttackButtonHold = false;
    }
    
    private void SwapSpell(InputAction.CallbackContext callbackContext) => OnSpellSwap?.Invoke();
    private void SwapMagic(InputAction.CallbackContext callbackContext) => OnMagicSwap?.Invoke();
    private void SwapWeapon(InputAction.CallbackContext callbackContext) => OnWeaponSwap?.Invoke();
}