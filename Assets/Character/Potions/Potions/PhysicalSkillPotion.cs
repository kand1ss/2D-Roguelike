public class PhysicalSkillPotion : TemporaryBuffPotion
{
    private readonly CharacterSkills targetSkills;
    
    public PhysicalSkillPotion(ICharacter target, int buff, float buffDuration) : base(target, buff, buffDuration)
    {
        targetSkills = target.Skills;
        
        ApplyBuff();
    }

    public override void ApplyBuff()
    {
        targetSkills.physicalSkillLevel += buffValue;
    }

    public override void RemoveBuff()
    {
        targetSkills.physicalSkillLevel -= buffValue;
    }
}