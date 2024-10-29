using UnityEngine;

public class WindPullVisual : ProjectileVisualBase
{
    private const string TORNADO = "Tornado";

    private void Start()
    {
        projectile.OnProjectileImpact += EnterTornadoAnimation;
    }

    private void OnDestroy()
    {
        projectile.OnProjectileImpact -= EnterTornadoAnimation;
    }

    private void EnterTornadoAnimation()
    {
        Animator.SetTrigger(TORNADO);
    }
}
