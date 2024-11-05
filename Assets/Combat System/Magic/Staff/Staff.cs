using UnityEngine;

[RequireComponent(typeof(StaffCastManager))]
[RequireComponent(typeof(StaffMagicSelector))]
public class Staff : WeaponBase, IChargingWeapon
{
    private StaffVisual visual;
    
    private StaffMagicSelector magicSelector;
    private StaffCastManager castManager;

    public ChargeHandler ChargeHandle => castManager;

    public StaffMagicSelector GetMagicComponent() => magicSelector;
    public StaffCastManager GetCastComponent() => castManager;
    
    public override void InitWeapon()
    {
        castManager = GetComponent<StaffCastManager>();
        magicSelector = GetComponent<StaffMagicSelector>();

        castManager.InitializeComponent();
        magicSelector.InitializeComponent();
        
        visual = GetComponentInChildren<StaffVisual>();
        visual.InitiateVisual();
    }

    public override void DetachWeapon()
    {
        visual.FinalizeVisual();
        visual = null;

        castManager.FinalizeComponent();
        magicSelector.FinalizeComponent();

        castManager = null;
        magicSelector = null;
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