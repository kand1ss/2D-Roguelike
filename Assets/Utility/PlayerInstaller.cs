using UnityEngine;
using UnityEngine.Serialization;
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
    }

    [Inject]
    private void InitializeComponents()
    {
        weaponController = playerInstance.GetComponentInChildren<PlayerWeaponController>();
        
        Container
            .BindInterfacesAndSelfTo<PlayerWeaponController>()
            .FromInstance(weaponController);
    }
}