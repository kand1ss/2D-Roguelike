using Zenject;
using UnityEngine;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private Enemy entity;

    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<Enemy>()
            .FromInstance(entity)
            .AsSingle();
        Container
            .BindInterfacesAndSelfTo<Entity>()
            .FromInstance(entity)
            .AsSingle();
    }
}