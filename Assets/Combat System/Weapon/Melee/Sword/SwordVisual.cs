using UnityEngine;
using UnityEngine.Events;

public class SwordVisual : WeaponVisualBase
{
    private const string STRONG_ATTACK = "StrongAttack";
    private const string WEAK_ATTACK = "WeakAttack";
    private const string ATTACK_SPEED = "AttackSpeed";

    [SerializeField] private Sword sword;
    private TrailRenderer trail;

    public event UnityAction OnAttackAnimationEnds;

    private void Awake()
    {
        base.Awake();
        
        trail = GetComponentInChildren<TrailRenderer>();
        trail.emitting = false;
    }

    public void InitVisual()
    {
        sword.GetAttackManager().OnWeaponAttack += AttackAnimation;
    }
    public void FinalizeVisual()
    {
        sword.GetAttackManager().OnWeaponAttack -= AttackAnimation;
    }

    private void AttackAnimation(SwordAttackType attackType)
    {
        trail.emitting = true;
        if (attackType == SwordAttackType.Strong)
        {
            Animator.SetTrigger(STRONG_ATTACK);
            Animator.SetFloat(ATTACK_SPEED, sword.GetAttackManager().CurrentStrongAttackSpeed);
        }
            
        if (attackType == SwordAttackType.Weak)
        {
            Animator.SetTrigger(WEAK_ATTACK);
            Animator.SetFloat(ATTACK_SPEED, sword.CurrentAttackSpeed);
        }
    }
    private void OnAttackEnds()
    {
        OnAttackAnimationEnds?.Invoke();
        trail.emitting = false;
    }
}
