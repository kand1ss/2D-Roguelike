using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SwordAttackManager))]
[RequireComponent(typeof(SwordCollisionManager))]
[RequireComponent(typeof(SwordComboManager))]
public class Sword : MeleeWeapon, IChargeableWeapon
{
    private ICharacter weaponOwner;
    private IInputProvider inputProvider;

    private SwordVisual swordVisual;

    private SwordCollisionManager collisionManager;
    private SwordComboManager comboManager;
    private SwordAttackManager attackManager;

    public ChargeHandler ChargeHandle => attackManager;

    public SwordCollisionManager GetCollisionManager() => collisionManager;
    public SwordComboManager GetComboManager() => comboManager;
    public SwordAttackManager GetAttackManager() => attackManager;

    [Inject]
    private void Construct([InjectOptional] IInputProvider input, ICharacter weaponOwner)
    {
        inputProvider = input;
        this.weaponOwner = weaponOwner;
    }

    private void SetupEventHandlers()
    {
        collisionManager.OnEntityEnterCollision += comboManager.AddEntityToCombo;
        collisionManager.OnEntityExitCollision += comboManager.RemoveEntityFromCombo;

        collisionManager.OnEntityEnterCollision += attackManager.StrikeDamage;

        attackManager.OnWeaponAttack += collisionManager.EnableAttackCollision;
        attackManager.OnWeaponAttack += comboManager.SetLastRegisteredAttack;

        swordVisual.OnAttackAnimationEnds += collisionManager.DisableWeaponCollision;
        swordVisual.OnAttackAnimationEnds += comboManager.SetAttackRegisteredFalse;
    }

    private void DetachEventHandlers()
    {
        collisionManager.OnEntityEnterCollision -= comboManager.AddEntityToCombo;
        collisionManager.OnEntityExitCollision -= comboManager.RemoveEntityFromCombo;

        collisionManager.OnEntityEnterCollision -= attackManager.StrikeDamage;

        attackManager.OnWeaponAttack -= collisionManager.EnableAttackCollision;
        attackManager.OnWeaponAttack -= comboManager.SetLastRegisteredAttack;

        swordVisual.OnAttackAnimationEnds -= collisionManager.DisableWeaponCollision;
        swordVisual.OnAttackAnimationEnds -= comboManager.SetAttackRegisteredFalse;
    }

    private void InitializeComponents()
    {
        swordVisual = GetComponentInChildren<SwordVisual>();

        collisionManager = GetComponent<SwordCollisionManager>();
        comboManager = GetComponent<SwordComboManager>();
        attackManager = GetComponent<SwordAttackManager>();
        
        attackManager.InitializeComponent();
        collisionManager.InitializeComponent();
        
        swordVisual.InitVisual();
    }

    private void FinalizeComponents()
    {
        swordVisual.FinalizeVisual();
        swordVisual = null;
        
        attackManager.FinalizeComponent();
        collisionManager.FinalizeComponent();
        comboManager.FinalizeComponent();
        
        collisionManager = null;
        comboManager = null;
        attackManager = null;
    }

    public override void InitWeapon()
    {
        InitializeComponents();
        SetupEventHandlers();

        if (inputProvider != null)
            inputProvider.ButtonsController.WeaponInput.OnUseWeaponPressed += ChargeAttack;

        comboManager.InitiateComboAttack(new PushCombo(weaponOwner));
        comboManager.InitiateComboAttack(new BleedingCombo(weaponOwner));
        comboManager.InitiateComboAttack(new StoneStanceCombo(weaponOwner));
        comboManager.InitiateComboAttack(new WindStanceCombo(weaponOwner, this));
    }

    public override void DetachWeapon()
    {
        DetachEventHandlers();

        if (inputProvider != null)
            inputProvider.ButtonsController.WeaponInput.OnUseWeaponPressed -= ChargeAttack;

        FinalizeComponents();
    }

    public override void UseWeapon()
    {
        attackManager.WeakAttack();
    }

    public void ChargeAttack()
    {
        attackManager.StartChargingStrongAttack();
    }
}