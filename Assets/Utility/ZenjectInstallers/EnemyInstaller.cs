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
    }
    
    [Inject]
    private void InitializeComponents()
    {
        weaponController = entity.GetComponentInChildren<EnemyWeaponController>();
        
        Container
            .BindInterfacesAndSelfTo<WeaponControllerBase>()
            .FromInstance(weaponController);
    }
}