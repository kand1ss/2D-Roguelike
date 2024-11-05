

using UnityEngine;

public class BleedingEffect : TimedEffect, IEffect
{
    private readonly ICharacterEffectSusceptible target;

    private readonly float minDamage;
    private readonly float maxDamage;
    
    public EffectType EffectType => EffectType.Bleeding;
    public Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/BleedingEffect");

    public BleedingEffect(ICharacterEffectSusceptible target, float minDamage, float maxDamage, float duration) : base(duration)
    {
        this.target = target;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        
        OnEffectApplied += ApplyEffect;
        OnEffectRemoved += RemoveEffect;
    }
    
    public void ApplyEffect()
    {
        Debug.Log($"{target.GetType().Name}: Bleeding effect");
        DamageService.SendDamageByEffect(target, minDamage, maxDamage);
    }

    public void RemoveEffect()
    {
        target.EffectManager.RemoveEffect(this);
    }
}