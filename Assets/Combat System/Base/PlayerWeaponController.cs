using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponController : MonoBehaviour
{
    private WeaponBase chosenWeapon;
    public WeaponBase ChosenWeapon => chosenWeapon;

    [SerializeField] private WeaponBase firstWeapon;
    [SerializeField] private WeaponBase secondWeapon;

    public event UnityAction OnWeaponChanged;

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
        InputService.ButtonsController.WeaponInput.OnWeaponSwap += ChangeCurrentWeapon;
        InputService.ButtonsController.WeaponInput.OnUseWeapon += UseWeaponAction;
    }
    private void FinalizeInputEvents()
    {
        InputService.ButtonsController.WeaponInput.OnWeaponSwap -= ChangeCurrentWeapon;
        InputService.ButtonsController.WeaponInput.OnUseWeapon -= UseWeaponAction;
    }
    
    private void UseWeaponAction() => chosenWeapon.UseWeapon();

    private void ChangeCurrentWeapon()
    {
        if (chosenWeapon is IChargingWeapon chargingWeapon && chargingWeapon.ChargeHandle.IsCharging == true)
            return;

        DetachChosenWeapon();
        chosenWeapon = (chosenWeapon == firstWeapon) ? secondWeapon : firstWeapon;
        AttachChosenWeapon();

        OnWeaponChanged?.Invoke();

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

    private void Update()
    {
        FollowMousePosition();
    }

    private void FollowMousePosition()
    {
        Vector3 mousePosition = InputService.Instance.GetCursorPositionInWorldPoint();
        Vector3 playerPosition = Player.Instance.transform.position;

        Vector3 direction = mousePosition - playerPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (mousePosition.x < playerPosition.x)
            transform.rotation = Quaternion.Euler(180, 0, -angle);
        else
            transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
