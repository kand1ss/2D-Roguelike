using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Staff))]
[RequireComponent(typeof(StaffMagicSelector))]
public class StaffCastManager : ChargeHandler
{
    private IInputProvider inputProvider;
    private ICharacter weaponOwner;

    private Staff staff;
    private StaffMagicSelector staffMagicSelector;

    [SerializeField] public Transform magicInstantiateTransform;

    [SerializeField] private float castTimeCooldownRate;
    private float currentCastTimeCooldown;

    [Inject]
    private void Construct([InjectOptional] IInputProvider input, ICharacter character)
    {
        inputProvider = input;
        weaponOwner = character;
    }

    public void InitializeComponent()
    {
        staff = GetComponent<Staff>();
        staffMagicSelector = staff.GetMagicComponent();

        if (inputProvider != null)
            inputProvider.ButtonsController.WeaponInput.OnUseWeaponCanceled += StopCharging;

        OnChargeAttackCompleted += CastMagic;
    }

    public void FinalizeComponent()
    {
        staff = null;
        staffMagicSelector = null;

        if (inputProvider != null)
            inputProvider.ButtonsController.WeaponInput.OnUseWeaponCanceled -= StopCharging;

        OnChargeAttackCompleted -= CastMagic;
    }

    public void HandleAttack()
    {
        var chosenSpellIndex = staffMagicSelector.ChosenSpellIndex;
        var holdTime = staffMagicSelector.CurrentMagic.Spells[chosenSpellIndex].holdTime;

        if (IsCharging)
            return;
        if (Time.time < currentCastTimeCooldown + staffMagicSelector.CurrentMagic.Spells[chosenSpellIndex].castCooldown)
            return;
        
        currentCastTimeCooldown = Time.time + castTimeCooldownRate;

        StartCharging(holdTime);
    }

    private void CastMagic()
    {
        var chosenSpellIndex = staffMagicSelector.ChosenSpellIndex;
        var cooldownTime = staffMagicSelector.CurrentMagic.Spells[chosenSpellIndex].castCooldown;

        currentCastTimeCooldown = Time.time;
        Debug.Log("Casting magic");
        staffMagicSelector.CurrentMagic.CastSpell(chosenSpellIndex);

        if (weaponOwner is Player)
            CooldownBar.Instance.ShowProgressBar(cooldownTime);
    }
}