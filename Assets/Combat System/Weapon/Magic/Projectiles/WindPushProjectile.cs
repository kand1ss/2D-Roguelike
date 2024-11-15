using UnityEngine;

public class WindPushProjectile : ProjectileBase
{
    protected override void Start()
    {
        base.Start();

        OnProjectileImpact += OnProjectileHits;
    }
    private void OnDestroy()
    {
        OnProjectileImpact -= OnProjectileHits;
    }

    protected override void OnProjectileHits()
    {
        ProjectileRb.velocity = Vector2.zero;
        ProjectileRb.angularVelocity = 0f;

        TurnSpellParticles(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Entity entity)) 
            return;
        
        Rigidbody2D entityRb = entity.GetComponent<Rigidbody2D>();

        entityRb.AddForce(ProjectileRb.velocity, ForceMode2D.Impulse);
    }
}