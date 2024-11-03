

using UnityEngine;

public class BurningEffect : TimedEffect
{
    private readonly ICharacterEffectSusceptible target;
    
    private readonly float minDamage;
    private readonly float maxDamage;
    
    public BurningEffect(ICharacterEffectSusceptible target, float minDamage, float maxDamage, float duration) : base(duration)
    {
        this.target = target;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
    }
    
    public override void ApplyEffect()
    {
        Debug.Log($"{target.GetType().Name}: Burning effect");
        DamageService.SendDamageByEffect(target, minDamage, maxDamage);
    }

    public override void RemoveEffect()
    {
        target.EffectManager.RemoveEffect(this);
    }
}