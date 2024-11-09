using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SwordAttackManager))]
[RequireComponent(typeof(SwordCollisionManager))]
[RequireComponent(typeof(SwordComboManager))]
public class Sword : MeleeWeapon, IChargingWeapon
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

    private void InitializeManagers()
    {
        swordVisual = GetComponentInChildren<SwordVisual>();

        collisionManager = GetComponent<SwordCollisionManager>();
        comboManager = GetComponent<SwordComboManager>();
        attackManager = GetComponent<SwordAttackManager>();
        
        swordVisual.InitVisual();
    }

    private void FinalizeManagers()
    {
        swordVisual.FinalizeVisual();
        swordVisual = null;
        
        collisionManager = null;
        comboManager = null;
        attackManager = null;
    }

    public override void InitWeapon()
    {
        InitializeManagers();
        SetupEventHandlers();

        if (inputProvider != null)
            inputProvider.ButtonsController.WeaponInput.OnUseWeaponPressed += ChargeAttack;

        attackManager.InitializeComponent();
        collisionManager.InitializeComponent();

        comboManager.InitiateComboAttacks(new PushCombo(weaponOwner));
        comboManager.InitiateComboAttacks(new BleedingCombo(weaponOwner));
    }

    public override void DetachWeapon()
    {
        DetachEventHandlers();

        if (inputProvider != null)
            inputProvider.ButtonsController.WeaponInput.OnUseWeaponPressed -= ChargeAttack;

        attackManager.FinalizeComponent();
        collisionManager.FinalizeComponent();
        comboManager.FinalizeComponent();

        FinalizeManagers();
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