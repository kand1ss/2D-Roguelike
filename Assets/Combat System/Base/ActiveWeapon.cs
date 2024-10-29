using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance { get; private set; }

    private WeaponBase chosenWeapon;
    public WeaponBase ChosenWeapon => chosenWeapon;

    [SerializeField] private WeaponBase firstWeapon;
    [SerializeField] private WeaponBase secondWeapon;

    public event UnityAction OnWeaponChanged;

    private void Awake()
    {
        chosenWeapon = firstWeapon;
        AttachChosenWeapon();

        Instance = this;
    }

    private void Start()
    {
        InitializeInputEvents();
    }
    private void OnDestroy()
    {
        FinalizeInputEvents();
    }

    private void InitializeInputEvents()
    {
        ButtonsInputService.Instance.OnWeaponSwap += ChangeCurrentWeapon;
        ButtonsInputService.Instance.OnUseWeapon += UseWeaponAction;
    }
    private void FinalizeInputEvents()
    {
        ButtonsInputService.Instance.OnWeaponSwap -= ChangeCurrentWeapon;
        ButtonsInputService.Instance.OnUseWeapon -= UseWeaponAction;
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
        chosenWeapon.gameObject.SetActive(true);
        chosenWeapon.InitWeapon();
    }
    private void DetachChosenWeapon()
    {
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
