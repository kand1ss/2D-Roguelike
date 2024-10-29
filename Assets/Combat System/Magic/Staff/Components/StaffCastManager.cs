using UnityEngine;

[RequireComponent(typeof(Staff))]
[RequireComponent(typeof(StaffMagicSelector))]
public class StaffCastManager : ChargeHandler
{
    private Staff staff;
    private StaffMagicSelector staffMagicSelector;
    
    [SerializeField] public Transform magicInstantiateTransform;
    
    [SerializeField] private float currentCastTimeCooldown;
    public float CurrentCastTimeCooldown => currentCastTimeCooldown;

    private void Awake()
    {
        staff = GetComponent<Staff>();
        staffMagicSelector = staff.GetMagicComponent();
    }

    public void InitializeComponent()
    {
        ButtonsInputService.Instance.OnUseWeaponCanceled += StopCharging;
        
        OnChargeAttackCompleted += CastMagic;
    }
    public void FinalizeComponent()
    {
        ButtonsInputService.Instance.OnUseWeaponCanceled -= StopCharging;
        
        OnChargeAttackCompleted -= CastMagic;
    }

    public void HandleAttack()
    {
        var castTimeCooldown = CurrentCastTimeCooldown;
        var chosenSpellIndex = staffMagicSelector.ChosenSpellIndex;
        var holdTime = staffMagicSelector.CurrentMagic.Spells[chosenSpellIndex].holdTime;
        
        if(IsCharging)
            return;
        if(Time.time < castTimeCooldown + staffMagicSelector.CurrentMagic.Spells[chosenSpellIndex].castCooldown)
            return;

        StartCharging(holdTime);
    }
    
    private void CastMagic()
    {
        currentCastTimeCooldown = Time.time;
        Debug.Log("Casting magic");
        staffMagicSelector.CurrentMagic.CastSpell(staffMagicSelector.ChosenSpellIndex);
    }
}
