using UnityEngine;

[RequireComponent(typeof(StaffCastManager))]
[RequireComponent(typeof(StaffMagicSelector))]
public class Staff : WeaponBase, IChargingWeapon
{
    private StaffMagicSelector magicSelector;
    
    private StaffCastManager castManager;
    public ChargeHandler ChargeHandle => castManager;

    public StaffCastManager GetCastComponent() => castManager;
    public StaffMagicSelector GetMagicComponent() => magicSelector;

    private void Awake()
    {
        castManager = GetComponent<StaffCastManager>();
        magicSelector = GetComponent<StaffMagicSelector>();
    }

    public override void InitWeapon()
    {
        castManager.InitializeComponent();
        magicSelector.InitializeComponent();
    }

    public override void DetachWeapon()
    {
        castManager.FinalizeComponent();
        magicSelector.FinalizeComponent();
    }

    public override void UseWeapon()
    {
        ChargeAttack();
    }

    public void ChargeAttack()
    {
        castManager.HandleAttack();
    }
}