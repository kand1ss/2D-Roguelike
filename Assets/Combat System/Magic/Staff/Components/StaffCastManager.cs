using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Staff))]
[RequireComponent(typeof(StaffMagicSelector))]
public class StaffCastManager : ChargeHandler
{
    private IInputProvider inputProvider;
    
    private Staff staff;
    private StaffMagicSelector staffMagicSelector;
    
    [SerializeField] public Transform magicInstantiateTransform;
    
    [SerializeField] private float currentCastTimeCooldown;
    public float CurrentCastTimeCooldown => currentCastTimeCooldown;

    [Inject]
    private void Construct(IInputProvider input, ICharacter character)
    {
        inputProvider = input;
    }

    public void InitializeComponent()
    {
        staff = GetComponent<Staff>();
        staffMagicSelector = staff.GetMagicComponent();
        
        inputProvider.ButtonsController.WeaponInput.OnUseWeaponCanceled += StopCharging;
        
        OnChargeAttackCompleted += CastMagic;
    }
    public void FinalizeComponent()
    {
        staff = null;
        staffMagicSelector = null;
        
        inputProvider.ButtonsController.WeaponInput.OnUseWeaponCanceled -= StopCharging;
        
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
        var chosenSpellIndex = staffMagicSelector.ChosenSpellIndex;
        var cooldownTime = staffMagicSelector.CurrentMagic.Spells[chosenSpellIndex].castCooldown;
        
        currentCastTimeCooldown = Time.time;
        Debug.Log("Casting magic");
        staffMagicSelector.CurrentMagic.CastSpell(chosenSpellIndex);
        
        CooldownBar.Instance.ShowProgressBar(cooldownTime);
    }
}
