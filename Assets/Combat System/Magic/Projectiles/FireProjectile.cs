using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static IStrikeDamage;

public class FireProjectile : ProjectileBase, IStrikeDamage
{
    [SerializeField] private float projectileMinDamage = 10f;
    [SerializeField] private float projectileMaxDamage = 10f;

    public float BaseMinDamageAmount => projectileMinDamage;
    public float BaseMaxDamageAmount => projectileMaxDamage;

    public DamageType CurrentDamageType => DamageType.Magical;

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
            
        ProjectileImpact();

        // ADD - ��� ������ ���� ����������� ������ ��������� �����
    }

    protected override void OnProjectileHits()
    {
        ProjectileRb.velocity = Vector2.zero;
        ProjectileRb.angularVelocity = 0f;

        TurnSpellParticles(false);
    }
}
