using UnityEngine;

public abstract class TimedBuffPotion : TimedEffect, IPotion
{
    protected readonly ICharacterEffectSusceptible target;
    protected readonly CharacterSkills targetSkills;

    protected readonly int buffValue;
    public int BuffValue => buffValue;
    
    public EffectType EffectType => EffectType.Buff;
    public abstract Sprite EffectIcon { get; }
    public abstract Sprite PotionSprite { get; }
    

    public TimedBuffPotion(ICharacterEffectSusceptible target, int buff, float buffDuration) : base(buffDuration)
    {
        this.target = target;
        targetSkills = target.Skills;
        
        buffValue = buff;

        EffectRemoved += RemoveEffect;
    }

    public abstract void ApplyEffect();

    public void RemoveEffect()
    {
        RemoveBuff();
        target.EffectManager.RemoveEffect(this);
    }
    protected virtual void RemoveBuff() {}
}