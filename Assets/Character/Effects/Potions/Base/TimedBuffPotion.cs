using UnityEngine;

public abstract class TimedBuffPotion : TimedEffect, IPotion
{
    protected readonly ICharacterEffectSusceptible effectTarget;
    protected readonly CharacterSkills targetSkills;

    protected readonly int buffValue;
    public int BuffValue => buffValue;
    
    public EffectType EffectType => EffectType.Buff;
    
    public abstract Sprite EffectIcon { get; }
    public abstract Sprite PotionSprite { get; }
    

    protected TimedBuffPotion(ICharacterEffectSusceptible effectTarget, int buff, float buffDuration) : base(buffDuration)
    {
        this.effectTarget = effectTarget;
        targetSkills = effectTarget.Skills;
        
        buffValue = buff;

        EffectRemoved += RemoveEffect;
    }

    public abstract void ApplyEffect();

    public void RemoveEffect()
    {
        RemoveBuff();
        effectTarget.EffectManager.RemoveEffect(this);
    }
    protected virtual void RemoveBuff() {}
}