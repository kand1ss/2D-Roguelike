using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    private PlayerWeaponController weaponController;
    [SerializeField] private Player playerInstance;

    public override void InstallBindings()
    {
        Container
            .Bind<IInputProvider>()
            .To<InputService>()
            .AsSingle();
        
        Container
            .BindInterfacesAndSelfTo<Player>()
            .FromInstance(playerInstance)
            .AsSingle();
        
        weaponController = playerInstance.GetComponentInChildren<PlayerWeaponController>();
        Container
            .BindInterfacesAndSelfTo<WeaponControllerBase>()
            .FromInstance(weaponController);
        
        var progressBar = playerInstance.GetComponentInChildren<ActionProgressBar>();
        Container
            .BindInterfacesAndSelfTo<ActionProgressBar>()
            .FromInstance(progressBar);
        
        Container
            .BindFactory<WeaponBase, Transform, WeaponBase, WeaponFactory>()
            .FromFactory<WeaponFactory>();
    }
}