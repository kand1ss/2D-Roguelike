public class FireMagicVisual : ProjectileVisualBase
{
    private const string HIT = "Hit";

    private void Start()
    {
        projectile.OnProjectileImpact += ProjectileImpactAnimation;
    }

    private void OnDestroy()
    {
        projectile.OnProjectileImpact -= ProjectileImpactAnimation;
    }

    private void ProjectileImpactAnimation()
    {
        Animator.SetTrigger(HIT);
    }
}
