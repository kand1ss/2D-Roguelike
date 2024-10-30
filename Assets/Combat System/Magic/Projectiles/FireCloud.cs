using UnityEngine;

public class FireCloud : ProjectileBase, IStrikeDamage
{
    [SerializeField] private float fireMinDamage = 1f;
    [SerializeField] private float fireMaxDamage = 2f;
    [SerializeField] private float burnDuration = 4f;

    public float BaseMinDamageAmount => fireMinDamage;
    public float BaseMaxDamageAmount => fireMaxDamage;

    public DamageType CurrentDamageType => DamageType.Magical;

    private void DisableProjectileCollision()
    {
        ProjectileCollision.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProjectileImpact();

        // ADD - тут должна быть реализована логика нанесения урона
        Debug.Log($"Cloud Projectile: Collision Enter");
    }
}
