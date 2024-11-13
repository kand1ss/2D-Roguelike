using UnityEngine;

public abstract class BuffEffectBase : TimedEffect, IEffect
{
    protected ICharacterEffectSusceptible effectStriker;

    protected int buffBonus;

    public abstract EffectType EffectType { get; }
    public abstract Sprite EffectIcon { get; }
    

    public BuffEffectBase(ICharacterEffectSusceptible effectStriker, int buffValue, float duration) : base(duration)
    {
        this.effectStriker = effectStriker;
        buffBonus = buffValue;
        
        EffectRemoved += RemoveEffect;
    }

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
}