using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SwordAttackManager))]
[RequireComponent(typeof(SwordCollisionManager))]
[RequireComponent(typeof(SwordComboManager))]
public class Sword : MeleeWeapon, IChargingWeapon
{
    private SwordVisual swordVisual;

    private SwordCollisionManager collisionManager;
    private SwordComboManager comboManager;
    private SwordAttackManager attackManager;

    public ChargeHandler ChargeHandle => attackManager;

    public SwordCollisionManager GetCollisionManager() => collisionManager;
    public SwordComboManager GetComboManager() => comboManager;
    public SwordAttackManager GetAttackManager() => attackManager;


    private void Awake()
    {
        swordVisual = GetComponentInChildren<SwordVisual>();

        collisionManager = GetComponent<SwordCollisionManager>();
        comboManager = GetComponent<SwordComboManager>();
        attackManager = GetComponent<SwordAttackManager>();
    }

    private void SetupEventHandlers()
    {
        collisionManager.OnEntityEnterCollision += comboManager.AddEntityToCombo;
        collisionManager.OnEntityExitCollision += comboManager.RemoveEntityFromCombo;
        attackManager.OnWeaponAttack += collisionManager.EnableAttackCollision;
        attackManager.OnWeaponAttack += comboManager.SetLastRegisteredAttack;

        swordVisual.OnAttackAnimationEnds += collisionManager.DisableWeaponCollision;
        swordVisual.OnAttackAnimationEnds += comboManager.SetAttackRegisteredFalse;
    }
    private void DetachEventHandlers()
    {
        collisionManager.OnEntityEnterCollision -= comboManager.AddEntityToCombo;
        collisionManager.OnEntityExitCollision -= comboManager.RemoveEntityFromCombo;
        attackManager.OnWeaponAttack -= collisionManager.EnableAttackCollision;
        attackManager.OnWeaponAttack -= comboManager.SetLastRegisteredAttack;

        swordVisual.OnAttackAnimationEnds -= collisionManager.DisableWeaponCollision;
        swordVisual.OnAttackAnimationEnds -= comboManager.SetAttackRegisteredFalse;
    }

    private void Start()
    {
        SetupEventHandlers();
    }

    private void OnDestroy()
    {
        DetachEventHandlers();
    }

    public override void InitWeapon()
    {
        ButtonsInputService.Instance.OnStrongAttack += ChargeAttack;
        
        attackManager.InitializeComponent();
        collisionManager.InitializeComponent();
        
        comboManager.InitiateComboAttacks(new PushCombo());
    }

    public override void DetachWeapon()
    {
        ButtonsInputService.Instance.OnStrongAttack -= ChargeAttack;
        
        attackManager.FinalizeComponent();
        collisionManager.FinalizeComponent();
        
        comboManager.FinalizeComboAttacks();
    }

    public override void UseWeapon()
    {
        attackManager.WeakAttack();
    }

    public void ChargeAttack()
    {
        attackManager.StartStrongAttack();
    }
}