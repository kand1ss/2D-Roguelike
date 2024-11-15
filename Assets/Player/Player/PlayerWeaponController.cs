using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerWeaponController : WeaponControllerBase
{
    private IInputProvider inputProvider;

    [SerializeField] private WeaponBase firstWeapon;
    [SerializeField] private WeaponBase secondWeapon;

    [Inject]
    private void Construct(IInputProvider input)
    {
        inputProvider = input;
    }

    private void Awake()
    {
        chosenWeapon = firstWeapon;
    }

    private void Start()
    {
        InitializeInputEvents();
        AttachChosenWeapon();
    }
    
    private void OnDestroy()
    {
        FinalizeInputEvents();
    }

    private void InitializeInputEvents()
    {
        inputProvider.ButtonsController.WeaponInput.OnWeaponSwap += SwapCurrentWeapon;
        inputProvider.ButtonsController.WeaponInput.OnUseWeapon += UseChosenWeapon;
    }
    private void FinalizeInputEvents()
    {
        inputProvider.ButtonsController.WeaponInput.OnWeaponSwap -= SwapCurrentWeapon;
        inputProvider.ButtonsController.WeaponInput.OnUseWeapon -= UseChosenWeapon;
    }
    
    private void SwapCurrentWeapon()
    {
        if (chosenWeapon is IChargeableWeapon chargingWeapon && chargingWeapon.ChargeHandle.IsCharging == true)
            return;

        DetachChosenWeapon();
        chosenWeapon = (chosenWeapon == firstWeapon) ? secondWeapon : firstWeapon;
        AttachChosenWeapon();

        WeaponChanged();

        Debug.Log($"Оружие изменено: {chosenWeapon.GetType().Name}");
    }

    private void AttachChosenWeapon()
    {
        var weaponVisual = chosenWeapon.GetComponentInChildren<WeaponVisualBase>();
        weaponVisual.AttachPlayerEvents();
        
        chosenWeapon.gameObject.SetActive(true);
        chosenWeapon.InitWeapon();
    }
    private void DetachChosenWeapon()
    {
        var weaponVisual = chosenWeapon.GetComponentInChildren<WeaponVisualBase>();
        weaponVisual.DetachPlayerEvents();
        
        chosenWeapon.gameObject.SetActive(false);
        chosenWeapon.DetachWeapon();
    }

    protected override Vector2 GetFollowDirectionTarget()
    {
        return CoordinateManager.GetCursorPositionInWorldPoint();
    }
}
