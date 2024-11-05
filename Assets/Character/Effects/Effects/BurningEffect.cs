

using UnityEngine;

public class BurningEffect : TimedEffect, IEffect
{
    private readonly ICharacterEffectSusceptible target;
    
    private readonly float minDamage;
    private readonly float maxDamage;

    public EffectType EffectType => EffectType.Burning;
    public Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/BurningEffect");
    
    public BurningEffect(ICharacterEffectSusceptible target, float minDamage, float maxDamage, float duration) : base(duration)
    {
        this.target = target;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;
        
        OnEffectApplied += ApplyEffect;
        OnEffectRemoved += RemoveEffect;
    }
    
    public void ApplyEffect()
    {
        Debug.Log($"{target.GetType().Name}: Burning effect");
        DamageService.SendDamageByEffect(target, minDamage, maxDamage);
    }

    public void RemoveEffect()
    {
        target.EffectManager.RemoveEffect(this);
    }
}