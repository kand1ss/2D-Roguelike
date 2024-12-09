using Zenject;
using UnityEngine;

public class EnemyInstaller : MonoInstaller
{
    private EnemyWeaponController weaponController;
    [SerializeField] private EnemyAI entity;

    public override void InstallBindings()
    {
        Container
            .Bind<EnemyAI>()
            .FromInstance(entity)
            .AsSingle();
        Container
            .BindInterfacesAndSelfTo<Entity>()
            .FromInstance(entity)
            .AsSingle();

        weaponController = entity.GetComponentInChildren<EnemyWeaponController>();
        Container
            .BindInterfacesAndSelfTo<WeaponControllerBase>()
            .FromInstance(weaponController);
        
        var progressBar = entity.GetComponentInChildren<ActionProgressBar>();
        Container
            .BindInterfacesAndSelfTo<ActionProgressBar>()
            .FromInstance(progressBar);

        Container
            .BindFactory<WeaponBase, Transform, WeaponBase, WeaponFactory>()
            .FromFactory<WeaponFactory>();
        
        if (entity is IPotionUser entityPotionUser)
        {
            Container
                .Bind<IPotionUser>()
                .FromInstance(entityPotionUser)
                .AsSingle();
        }
    }
}