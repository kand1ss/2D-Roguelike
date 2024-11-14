using UnityEngine;

public class PhysicalSkillPotion : TimedBuffPotion
{
    public override Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/PhysicalSkillEffect");
    public override Sprite PotionSprite => Resources.Load<Sprite>("Sprites/Potions/PhysicalPotion");

    public PhysicalSkillPotion(ICharacterEffectSusceptible target, int buff, float buffDuration) : base(target, buff, buffDuration)
    {
    }

    public override void ApplyEffect()
    {
        targetSkills.physicalSkillLevel += buffValue;
    }

    protected override void RemoveBuff()
    {
        targetSkills.physicalSkillLevel -= buffValue;
    }
}