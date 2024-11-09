using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SwordAttackManager : ChargeHandler
{
    private ICharacter attacker;
    private IInputProvider inputProvider;
    
    private IWeaponController attackerWeapon;

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
        attacker = character;

        this.attackerWeapon = entityWeapon;
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

        Debug.Log("Слабый удар");

        attackCooldownTimer = Time.time;

        OnWeaponAttack?.Invoke(SwordAttackType.Weak);
        
        if (attacker is Player)
            CooldownBar.Instance.ShowProgressBar(weakAttackCooldownRate);
    }

    public void StrikeDamage(ICharacter attackTarget)
    {
        var weapon = (Sword)attackerWeapon.ChosenWeapon;

        DamageService.SendDamageToTarget(attacker, attackTarget, weapon);
    }

    public void StartChargingStrongAttack()
    {
        if (Time.time < AttackCooldownTimer + StrongAttackCooldownRate)
            return;

        StartCharging(StrongAttackHoldTime);
    }

    private void StrongAttack()
    {
        Debug.Log("Сильный удар");

        OnWeaponAttack?.Invoke(SwordAttackType.Strong);
        attackCooldownTimer = Time.time;

        if (attacker is Player) 
            CooldownBar.Instance.ShowProgressBar(strongAttackCooldownRate);
    }
}