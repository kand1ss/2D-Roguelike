using UnityEngine;
using UnityEngine.Events;

public class SwordAttackManager : ChargeHandler
{
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

    public void InitializeComponent()
    {
        InputService.ButtonsController.WeaponInput.OnUseWeaponPressedCanceled += StopCharging;
        
        OnChargeAttackCompleted += StrongAttack;
    }

    public void FinalizeComponent()
    {
        InputService.ButtonsController.WeaponInput.OnUseWeaponPressedCanceled -= StopCharging;
        
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

    public void StartStrongAttack()
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