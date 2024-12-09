using UnityEngine;

public abstract class EnemyBehaviourWithWeapon : PotionUserEnemyBehaviour, IHasWeapon
{
    [field: SerializeField] public EnemyWeaponController EnemyWeaponController { get; private set; }
    public WeaponControllerBase WeaponController => EnemyWeaponController;

    protected override void OnDestroy()
    {
        base.OnDestroy();
        EnemyWeaponController.ChosenWeapon.DetachWeapon();
    }
}