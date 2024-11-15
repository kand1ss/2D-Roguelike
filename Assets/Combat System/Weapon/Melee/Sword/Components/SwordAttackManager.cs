using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SwordAttackManager : ChargeHandler
{
    private ICharacter swordOwner;
    private IInputProvider inputProvider;
    private IWeaponController ownerWeaponController;

    [SerializeField] private float currentStrongAttackSpeed;

    [SerializeField] private float weakAttackCooldownRate;
    [SerializeField] private float strongAttackCooldownRate;
    private float attackCooldownTimer = 0;
    
    [SerializeField] private float strongAttackHoldTime;

    public float CurrentStrongAttackSpeed => currentStrongAttackSpeed;
    public float WeakAttackCooldownRate => weakAttackCooldownRate;
    public float StrongAttackCooldownRate => strongAttackCooldownRate;
    public float AttackCooldownTimer => attackCooldownTimer;
    public float StrongAttackHoldTime => strongAttackHoldTime;

    public event UnityAction<SwordAttackType> OnWeaponAttack;

    [Inject]
    private void Construct([InjectOptional] IInputProvider input, ICharacter character,
        IWeaponController entityWeapon)
    {
        inputProvider = input;
        swordOwner = character;

        this.ownerWeaponController = entityWeapon;
    }

    public void InitializeComponent()
    {
        if (inputProvider != null)
            inputProvider.ButtonsController.WeaponInput.OnUseWeaponPressedCanceled += StopCharging;

        OnChargeAttackCompleted += StrongAttack;
    }

    public void FinalizeComponent()
    {
        if (inputProvider != null)
            inputProvider.ButtonsController.WeaponInput.OnUseWeaponPressedCanceled -= StopCharging;

        OnChargeAttackCompleted -= StrongAttack;
    }

    public void WeakAttack()
    {
        if (Time.time < attackCooldownTimer + weakAttackCooldownRate)
            return;
        
        attackCooldownTimer = Time.time;

        OnWeaponAttack?.Invoke(SwordAttackType.Weak);
        
        if (swordOwner is Player)
            CooldownBar.Instance.ShowProgressBar(weakAttackCooldownRate);
    }

    public void StrikeDamage(ICharacter attackTarget)
    {
        var weapon = (Sword)ownerWeaponController.ChosenWeapon;

        DamageService.SendDamageToTarget(swordOwner, attackTarget, weapon);
    }

    public void StartChargingStrongAttack()
    {
        if (Time.time < AttackCooldownTimer + StrongAttackCooldownRate)
            return;

        StartCharging(StrongAttackHoldTime);
    }

    private void StrongAttack()
    {
        attackCooldownTimer = Time.time;
        OnWeaponAttack?.Invoke(SwordAttackType.Strong);

        if (swordOwner is Player) 
            CooldownBar.Instance.ShowProgressBar(strongAttackCooldownRate);
    }
}