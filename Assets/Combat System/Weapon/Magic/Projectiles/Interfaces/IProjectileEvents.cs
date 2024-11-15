using UnityEngine.Events;

public interface IProjectileEvents
{
    public event UnityAction OnProjectileImpact;
    public event UnityAction OnProjectileLaunch;
    public event UnityAction OnProjectileDestroy;
}
