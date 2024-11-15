using UnityEngine;

public class PhysicalProtectionEffect : BuffEffectBase
{
    private readonly CharacterResists strikerResists;
    
    public override EffectType EffectType => EffectType.Buff;
    public override Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/PhysicalProtectionEffect");


    public PhysicalProtectionEffect(ICharacterEffectSusceptible effectTarget, int buffValue, float duration) : base(effectTarget, buffValue, duration)
    {
        strikerResists = effectTarget.Resists;
        
        ApplyEffect();
    }

    public override void ApplyEffect()
    {
        strikerResists.PhysicalResistance += buffBonus;
    }

    public override void RemoveEffect()
    {
        strikerResists.PhysicalResistance -= buffBonus;
        effectTarget.EffectManager.RemoveEffect(this);
    }
}