using UnityEngine;

public class FireCloud : ProjectileBase, IStrikeDamage
{
    [SerializeField] private float fireMinDamage = 1f;
    [SerializeField] private float fireMaxDamage = 2f;
    [SerializeField] private float burnDuration = 4f;

    public float BaseMinDamageAmount => fireMinDamage;
    public float BaseMaxDamageAmount => fireMaxDamage;

    public DamageType CurrentDamageType => DamageType.Magical;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProjectileImpact();

        if (!collision.TryGetComponent<ICharacterEffectSusceptible>(out ICharacterEffectSusceptible character))
            return;

        if (character == ProjectileSender)
            return;
        
        character.EffectManager.ApplyEffect(
            new BurningEffect(character, fireMinDamage, fireMaxDamage, burnDuration));
    }
}
