using UnityEngine;
using UnityEngine.Events;

public class SwordVisual : WeaponVisualBase
{
    private const string STRONG_ATTACK = "StrongAttack";
    private const string WEAK_ATTACK = "WeakAttack";
    private const string ATTACK_SPEED = "AttackSpeed";

    [SerializeField] private Sword sword;

    public event UnityAction OnAttackAnimationEnds;

    private void Awake()
    {
        base.InitiateFields();
    }

    private void Start()
    {
        sword.GetAttackManager().OnWeaponAttack += AttackAnimation;

        ActiveWeapon.Instance.OnWeaponChanged += ResetAnimation;
    }
    private void AttackAnimation(SwordAttackType attackType)
    {
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

    private void ResetAnimation()
    {
        transform.localRotation = InitialRotation;
        transform.localPosition = InitialPosition;
    }

    private void OnAttackEnds()
    {
        OnAttackAnimationEnds?.Invoke();
    }
}
