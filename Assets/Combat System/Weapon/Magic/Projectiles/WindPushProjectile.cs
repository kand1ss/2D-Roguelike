using System;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICharacter character) && character == ProjectileSender)
            return;

        if (collision.TryGetComponent(out Entity entity))
        {
            Rigidbody2D entityRb = entity.GetComponent<Rigidbody2D>();

            entityRb.AddForce(ProjectileRb.velocity, ForceMode2D.Impulse);
        }
        else
            ProjectileImpact();
    }
}