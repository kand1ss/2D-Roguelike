using UnityEngine;

public class StoneStanceEffect : StanceEffectBase
{
    private readonly CharacterResists targetResists;
    private readonly CharacterSkills targetSkills;
    
    public override EffectType EffectType => EffectType.Stance;
    public override Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/StoneStanceEffect");


    public StoneStanceEffect(ICharacterEffectSusceptible effectTarget, int buffValue, int debuffValue, float duration) : base(effectTarget, buffValue,  debuffValue, duration)
    {
        targetResists = effectTarget.Resists;
        targetSkills = effectTarget.Skills;
        
        ApplyEffect();
    }

    public override void ApplyEffect()
    {
        targetResists.PhysicalResistance += buffBonus;
        targetSkills.PhysicalSkillLevel -= debuffValue;
    }

    public override void RemoveEffect()
    {
        targetResists.PhysicalResistance -= buffBonus;
        targetSkills.PhysicalSkillLevel += debuffValue;
        
        effectTarget.EffectManager.RemoveEffect(this);
    }
}