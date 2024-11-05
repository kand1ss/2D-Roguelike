using Zenject;
using UnityEngine;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private Entity entity;

    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<Entity>()
            .FromInstance(entity)
            .AsSingle();
    }
}