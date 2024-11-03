

using UnityEngine;

public class BleedingEffect : TimedEffect
{
    private readonly ICharacterEffectSusceptible target;

    private readonly float minDamage;
    private readonly float maxDamage;

    public BleedingEffect(ICharacterEffectSusceptible target, float minDamage, float maxDamage, float duration) : base(duration)
    {
        this.target = target;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }
    
    public override void ApplyEffect()
    {
        Debug.Log($"{target.GetType().Name}: Bleeding effect");
        DamageService.SendDamageByEffect(target, minDamage, maxDamage);
    }

    public override void RemoveEffect()
    {
        target.EffectManager.RemoveEffect(this);
    }
}