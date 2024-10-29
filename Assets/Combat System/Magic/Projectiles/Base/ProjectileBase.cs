﻿using UnityEngine;
using UnityEngine.Events;

public class ProjectileBase : MonoBehaviour, IProjectileEvents
{
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] protected float projectileRange = 8f;
    public float ProjectileSpeed => projectileSpeed;


    protected ProjectileVisualBase ProjectileVisual;

    public event UnityAction OnProjectileImpact;
    public event UnityAction OnProjectileLaunch;
    public event UnityAction OnProjectileDestroy;

    protected Collider2D ProjectileCollision;
    protected Rigidbody2D ProjectileRb;

    protected Vector3 StartPosition;


    protected virtual void Awake()
    {
        StartPosition = transform.position;

        ProjectileVisual = GetComponentInChildren<ProjectileVisualBase>();

        ProjectileCollision = GetComponent<Collider2D>();
        ProjectileRb = GetComponent<Rigidbody2D>();
    }
    protected void ProjectileImpact()
    {
        OnProjectileImpact?.Invoke();
    }
    protected virtual void Start()
    {
        OnProjectileLaunch?.Invoke();

        ProjectileVisual.OnHitAnimationEnds += DestroyProjectile;
    }

    protected void DestroyProjectile()
    {
        ProjectileVisual.OnHitAnimationEnds -= DestroyProjectile;

        OnProjectileDestroy?.Invoke();

        Destroy(gameObject);
    }
    private void Update()
    {
        CheckProjectileRange();
    }
    private void CheckProjectileRange()
    {
        float distance = Vector3.Distance(transform.position, StartPosition);
        if (distance > projectileRange)
            OnProjectileImpact?.Invoke();
    }

    protected void TurnSpellParticles(bool turn)
    {
        ParticleSystem spellParticleSystem = GetComponentInChildren<ParticleSystem>();
        var emission = spellParticleSystem.emission;
        emission.enabled = turn;
    }

    protected virtual void OnProjectileHits() { }
}
