using UnityEngine;
using Zenject;

public class StaffVisual : WeaponVisualBase
{
    private IInputProvider inputProvider;
    
    private const string CHARGE_CAST = "ChargeCast";
    private const string CAST = "Cast";

    [SerializeField] private Staff staff;

    [Inject]
    private void Construct(IInputProvider input)
    {
        inputProvider = input;
    }

    public void InitiateVisual()
    {
        staff.GetCastComponent().OnChargeAttackStart += StartChargeCastAnimation;
        staff.GetCastComponent().OnChargeAttackCompleted += CastAnimation;
    }

    public void FinalizeVisual()
    {
        staff.GetCastComponent().OnChargeAttackStart -= StartChargeCastAnimation;
        staff.GetCastComponent().OnChargeAttackCompleted -= CastAnimation;
    }

    public override void AttachPlayerEvents()
    {
        base.AttachPlayerEvents();
        
        inputProvider.ButtonsController.WeaponInput.OnUseWeaponCanceled += StopChargeCastAnimation;
    }
    public override void DetachPlayerEvents()
    {
        base.DetachPlayerEvents();
        
        inputProvider.ButtonsController.WeaponInput.OnUseWeaponCanceled -= StopChargeCastAnimation;
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
