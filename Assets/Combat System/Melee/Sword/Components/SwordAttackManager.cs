using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SwordAttackManager : ChargeHandler
{
    private ICharacter attacker;
    private IInputProvider inputProvider;

    // CHANGE
    private PlayerWeaponController playerWeapon;
    
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
    private void Construct(IInputProvider input, ICharacter character, PlayerWeaponController playerWeapon)
    {
        inputProvider = input;
        attacker = character;

        this.playerWeapon = playerWeapon;
    }

    public void InitializeComponent()
    {
        inputProvider.ButtonsController.WeaponInput.OnUseWeaponPressedCanceled += StopCharging;
        
        OnChargeAttackCompleted += StrongAttack;
    }

    public void FinalizeComponent()
    {
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
        CooldownBar.Instance.ShowProgressBar(weakAttackCooldownRate);
    }

    public void StrikeDamage(ICharacter attackTarget)
    {
        if (!playerWeapon.ChosenWeapon is Sword)
            return;
        
        Sword weapon = (Sword)playerWeapon.ChosenWeapon;
        
        DamageService.SendDamage(attacker, attackTarget, weapon);
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
        
        CooldownBar.Instance.ShowProgressBar(strongAttackCooldownRate);
    }
}