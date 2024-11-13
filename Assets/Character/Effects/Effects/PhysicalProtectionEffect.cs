using UnityEngine;

public class PhysicalProtectionEffect : BuffEffectBase
{
    public override EffectType EffectType => EffectType.Buff;
    public override Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/PhysicalProtectionEffect");

    private readonly CharacterResists strikerResists;

    public PhysicalProtectionEffect(ICharacterEffectSusceptible effectStriker, int buffValue, float duration) : base(effectStriker, buffValue, duration)
    {
        strikerResists = effectStriker.Resists;
        
        ApplyEffect();
    }

    public override void ApplyEffect()
    {
        strikerResists.PhysicalResistance += buffBonus;
    }

    public override void RemoveEffect()
    {
        strikerResists.PhysicalResistance -= buffBonus;
        effectStriker.EffectManager.RemoveEffect(this);
    }
}