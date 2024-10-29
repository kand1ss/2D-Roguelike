using UnityEngine;

public class StaffVisual : WeaponVisualBase
{
    private const string CHARGE_CAST = "ChargeCast";
    private const string CAST = "Cast";

    [SerializeField] private Staff staff;

    private void Awake()
    {
        base.InitiateFields();
    }

    private void Start()
    {
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
        staff.GetCastComponent().OnChargeAttackStart += StartChargeCastAnimation;
        staff.GetCastComponent().OnChargeAttackCompleted += CastAnimation;

        ActiveWeapon.Instance.OnWeaponChanged += ResetAnimation;
        ButtonsInputService.Instance.OnUseWeaponCanceled += StopChargeCastAnimation;
    }

    private void UnsubscribeFromEvents()
    {
        staff.GetCastComponent().OnChargeAttackStart -= StartChargeCastAnimation;
        staff.GetCastComponent().OnChargeAttackCompleted -= CastAnimation;

        ActiveWeapon.Instance.OnWeaponChanged -= ResetAnimation;
        ButtonsInputService.Instance.OnUseWeaponCanceled -= StopChargeCastAnimation;
    }

    private void ResetAnimation()
    {
        transform.localRotation = InitialRotation;
        transform.localPosition = InitialPosition;
    }

    private void StartChargeCastAnimation(float arg0)
    {
        Animator.SetBool(CHARGE_CAST, true);
    }
    private void StopChargeCastAnimation()
    {
        Animator.SetBool(CHARGE_CAST, false);
    }

    private void CastAnimation()
    {
        Animator.SetBool(CHARGE_CAST, false);
        Animator.SetTrigger(CAST);
    }

}
