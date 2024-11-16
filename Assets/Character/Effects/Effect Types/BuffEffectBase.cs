using System;
using UnityEngine;

public abstract class BuffEffectBase : TimedEffect, IEffect
{
    protected readonly ICharacterEffectSusceptible effectTarget;

    protected readonly float buffBonus;

    public abstract EffectType EffectType { get; }
    public abstract Sprite EffectIcon { get; }
    

    protected BuffEffectBase(ICharacterEffectSusceptible effectTarget, float buffValue, float duration) : base(duration)
    {
        this.effectTarget = effectTarget;
        buffBonus = buffValue;
        
        EffectRemoved += RemoveEffect;
    }

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
    
    public override bool Equals(object obj)
    {
        if (obj is BuffEffectBase other)
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