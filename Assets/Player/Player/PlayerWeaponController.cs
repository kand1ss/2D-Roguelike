using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerWeaponController : WeaponControllerBase
{
    private IInputProvider inputProvider;

    [field: SerializeField] public WeaponBase firstWeapon { get; private set; }
    [field: SerializeField] public WeaponBase secondWeapon { get; private set; }

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

    public void SetFirstWeapon(WeaponBase weapon)
    {
        firstWeapon = weapon;
    }

    public void SetSecondWeapon(WeaponBase weapon)
    {
        secondWeapon = weapon;
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

    public void AttachChosenWeapon()
    {
        var weaponVisual = chosenWeapon.GetComponentInChildren<WeaponVisualBase>();
        weaponVisual.AttachPlayerEvents();
        
        chosenWeapon.gameObject.SetActive(true);
        chosenWeapon.InitWeapon();
    }
    public void DetachChosenWeapon()
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
