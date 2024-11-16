using UnityEngine;

public class PhysicalSkillPotion : TimedBuffPotion
{
    public override Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/PhysicalSkillEffect");
    public override Sprite PotionSprite => Resources.Load<Sprite>("Sprites/Potions/PhysicalPotion");

    public PhysicalSkillPotion(ICharacterEffectSusceptible effectTarget, int buff, float buffDuration) : base(effectTarget, buff, buffDuration)
    {
    }

    public override void ApplyEffect()
    {
        targetSkills.PhysicalSkillLevel += buffValue;
    }

    protected override void RemoveBuff()
    {
        targetSkills.PhysicalSkillLevel -= buffValue;
    }
}