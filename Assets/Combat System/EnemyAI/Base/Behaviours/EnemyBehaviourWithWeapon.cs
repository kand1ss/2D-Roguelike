using UnityEngine;

public abstract class EnemyBehaviourWithWeapon : DefaultEnemyBehaviour, IEnemyWithWeapon
{
    [field: SerializeField] public EnemyWeaponController WeaponController { get; private set; }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        WeaponController.ChosenWeapon.DetachWeapon();
    }
}