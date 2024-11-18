using UnityEngine;
using Zenject;
using Cinemachine;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private Player player;
    
    public override void InstallBindings()
    {
        Container.Bind<Player>()
            .FromInstance(player)
            .AsSingle();
    }
}
