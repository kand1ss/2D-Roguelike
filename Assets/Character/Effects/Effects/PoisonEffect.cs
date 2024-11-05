using UnityEngine;

public class PoisonEffect : TimedEffect, IEffect
{
    private readonly ICharacterEffectSusceptible target;

    private readonly float minDamage;
    private readonly float maxDamage;

    public EffectType EffectType => EffectType.Poison;
    public Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/BurningEffect");
    
    public PoisonEffect(ICharacterEffectSusceptible target, float minDamage, float maxDamage, float duration) : base(duration)
    {
        this.target = target;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;

        OnEffectApplied += ApplyEffect;
        OnEffectRemoved += RemoveEffect;
    }

    public void ApplyEffect()
    {
        Debug.Log($"{target}: Poison Effect!");
        DamageService.SendDamageByEffect(target, minDamage, maxDamage);
    }

    public void RemoveEffect()
    {
        target.EffectManager.RemoveEffect(this);
    }
}