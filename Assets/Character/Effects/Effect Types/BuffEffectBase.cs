using UnityEngine;

public abstract class BuffEffectBase : TimedEffect, IEffect
{
    protected ICharacterEffectSusceptible effectTarget;

    protected int buffBonus;

    public abstract EffectType EffectType { get; }
    public abstract Sprite EffectIcon { get; }
    

    protected BuffEffectBase(ICharacterEffectSusceptible effectTarget, int buffValue, float duration) : base(duration)
    {
        this.effectTarget = effectTarget;
        buffBonus = buffValue;
        
        EffectRemoved += RemoveEffect;
    }

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
}