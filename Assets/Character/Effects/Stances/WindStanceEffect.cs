using UnityEngine;

public class WindStanceEffect : StanceEffectBase
{
    private ICharacterEffectSusceptible target;
    
    private readonly CharacterSkills targetSkills;
    private readonly Sword targetSword;

    private readonly float defaultAttackSpeed;
    
    public override EffectType EffectType => EffectType.Stance;
    public override Sprite EffectIcon { get; }

    public WindStanceEffect(ICharacterEffectSusceptible effectTarget, Sword sword, float buffValue, int debuffValue, float duration) : base(effectTarget, buffValue, debuffValue, duration)
    {
        target = effectTarget;
        
        targetSkills = effectTarget.Skills;
        targetSword = sword;

        if (targetSword == null)
            return;
        
        defaultAttackSpeed = targetSword.CurrentAttackSpeed;
        
        ApplyEffect();
    }

    public override void ApplyEffect()
    {
        targetSword.CurrentAttackSpeed += buffBonus;
        targetSkills.PhysicalSkillLevel -= debuffValue;
    }

    public override void RemoveEffect()
    {
        targetSword.CurrentAttackSpeed = defaultAttackSpeed;
        targetSkills.PhysicalSkillLevel += debuffValue;
        
        target.EffectManager.RemoveEffect(this);
    }
}