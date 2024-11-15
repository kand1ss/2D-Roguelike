using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
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

    [SerializeField] private float castTimeCooldown;
    private float castCooldownTimer;

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
        if (Time.time < castCooldownTimer + staffMagicSelector.CurrentMagic.Spells[chosenSpellIndex].castCooldown)
            return;
        
        StartCharging(holdTime);
    }

    private void CastMagic()
    {
        var chosenSpellIndex = staffMagicSelector.ChosenSpellIndex;
        var cooldownTime = staffMagicSelector.CurrentMagic.Spells[chosenSpellIndex].castCooldown;

        castCooldownTimer = Time.time + castTimeCooldown;
        staffMagicSelector.CurrentMagic.CastSpell(chosenSpellIndex);

        if (weaponOwner is Player)
            CooldownBar.Instance.ShowProgressBar(cooldownTime);
    }
}