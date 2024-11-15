using UnityEngine;

[RequireComponent(typeof(StaffCastManager))]
[RequireComponent(typeof(StaffMagicSelector))]
public class Staff : WeaponBase, IChargeableWeapon
{
    private StaffVisual visual;
    
    private StaffMagicSelector magicSelector;
    private StaffCastManager castManager;

    public ChargeHandler ChargeHandle => castManager;

    public StaffMagicSelector GetMagicComponent() => magicSelector;
    public StaffCastManager GetCastComponent() => castManager;
    
    public override void InitWeapon()
    {
        InitializeComponents();
        
        visual = GetComponentInChildren<StaffVisual>();
        visual.InitiateVisual();
    }

    private void InitializeComponents()
    {
        castManager = GetComponent<StaffCastManager>();
        magicSelector = GetComponent<StaffMagicSelector>();

        castManager.InitializeComponent();
        magicSelector.InitializeComponent();
    }

    public override void DetachWeapon()
    {
        visual.FinalizeVisual();
        visual = null;

        FinalizeComponents();
    }

    private void FinalizeComponents()
    {
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