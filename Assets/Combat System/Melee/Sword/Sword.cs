using UnityEngine;
using Zenject;

[RequireComponent(typeof(SwordAttackManager))]
[RequireComponent(typeof(SwordCollisionManager))]
[RequireComponent(typeof(SwordComboManager))]
public class Sword : MeleeWeapon, IChargingWeapon
{
    private ICharacter character;
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
    private void Construct(IInputProvider input, ICharacter weaponOwner)
    {
        inputProvider = input;
        character = weaponOwner;
    }


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

    public override void InitWeapon()
    {
        SetupEventHandlers();
        inputProvider.ButtonsController.WeaponInput.OnUseWeaponPressed += ChargeAttack;
        
        attackManager.InitializeComponent();
        collisionManager.InitializeComponent();
        
        comboManager.InitiateComboAttacks(new PushCombo(character));
    }

    public override void DetachWeapon()
    {
        DetachEventHandlers();
        inputProvider.ButtonsController.WeaponInput.OnUseWeaponPressed -= ChargeAttack;
        
        attackManager.FinalizeComponent();
        collisionManager.FinalizeComponent();
        
        comboManager.FinalizeComponent();
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