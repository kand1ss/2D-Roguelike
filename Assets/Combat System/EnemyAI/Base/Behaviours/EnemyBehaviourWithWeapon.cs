using UnityEngine;

public abstract class EnemyBehaviourWithWeapon : DefaultEnemyBehaviour, IEnemyWithWeapon
{
    [field: SerializeField] public EnemyWeaponController WeaponController { get; private set; }
    
    [SerializeField] protected float attackingStartDistance;
    [SerializeField] protected float attackInterval;

    protected override void OnDestroy()
    {
        base.OnDestroy();
        WeaponController.ChosenWeapon.DetachWeapon();
    }
    
    protected override void CheckTransitionsFromAnyState()
    {
        base.CheckTransitionsFromAnyState();
        
        AttackingStateTransition();
    }

    protected abstract void AttackingStateTransition();
}