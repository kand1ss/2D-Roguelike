using UnityEngine;

public class FireProjectile : ProjectileBase, IStrikeDamage
{
    [SerializeField] private float projectileMinDamage = 4f;
    [SerializeField] private float projectileMaxDamage = 8f;

    public float BaseMinDamageAmount => projectileMinDamage;
    public float BaseMaxDamageAmount => projectileMaxDamage;

    public DamageType CurrentDamageType => DamageType.Magical;

    private bool isTargetHit = false;

    protected override void Start()
    {
        base.Start();

        ProjectileVisual.OnHitAnimationEnds += DestroyProjectile;
        OnProjectileImpact += OnProjectileHits;
    }

    private void OnDestroy()
    {
        ProjectileVisual.OnHitAnimationEnds -= DestroyProjectile;
        OnProjectileImpact -= OnProjectileHits;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ProjectileBase projectile))
            return;

        if (collision.TryGetComponent(out ICharacter attackTarget))
        {
            if (attackTarget == ProjectileSender)
                return;

            if (isTargetHit)
                return;
            
            CinemachineShake.Instance.Shake(0.2f, 0.7f);
            DamageService.SendDamageToTarget(ProjectileSender ,attackTarget, this);
            
            isTargetHit = true;
        }
        
        ProjectileImpact();
    }
}
