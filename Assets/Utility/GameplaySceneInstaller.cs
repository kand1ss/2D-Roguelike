using UnityEngine;
using Zenject;
using Cinemachine;

public class GameplaySceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("GameplaySceneInstaller InstallBindings");
    }
}
