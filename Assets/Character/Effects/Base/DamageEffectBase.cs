using System;
using UnityEngine;

public abstract class DamageEffectBase : TimedEffect, IEffect
{
    private readonly ICharacterEffectSusceptible effectTarget;

    private readonly float minDamage;
    private readonly float maxDamage;
    
    public abstract EffectType EffectType { get; }
    public abstract Sprite EffectIcon { get; }
    

    protected DamageEffectBase(ICharacterEffectSusceptible effectTarget, float minDamage, float maxDamage, float duration) : base(duration)
    {
        this.effectTarget = effectTarget;
        this.minDamage = minDamage;
        this.maxDamage = maxDamage;

        EffectApplied += ApplyEffect;
        EffectRemoved += RemoveEffect;
    }

    public virtual void ApplyEffect()
    {
        Debug.Log($"{effectTarget}: {this.GetType().Name}");
        DamageService.SendDamageByEffect(effectTarget, minDamage, maxDamage);
    }

    public virtual void RemoveEffect()
    {
        effectTarget.EffectManager.RemoveEffect(this);
    }

    public override bool Equals(object obj)
    {
        if (obj is DamageEffectBase other)
        {
            return this.EffectType == other.EffectType && this.EffectIcon == other.EffectIcon;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(EffectType, effectTarget);
    }
}