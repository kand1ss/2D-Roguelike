using System.Collections.Generic;

public class ProtectionCombo : Combo
{
    private ICharacter comboInitiator;
    
    public ProtectionCombo(ICharacter comboInitiator) : base()
    {
        this.comboInitiator = comboInitiator;
    }
    
    protected override void InitCombo()
    {
        ExpectedAttackSequence.Add(SwordAttackType.Strong);
        ExpectedAttackSequence.Add(SwordAttackType.Weak);
        ExpectedAttackSequence.Add(SwordAttackType.Strong);
    }

    public override void UseCombo(IEnumerable<ICharacter> entities)
    {
        if(comboInitiator is ICharacterEffectSusceptible characterEffectSusceptible)
        characterEffectSusceptible.EffectManager.ApplyEffect(
            new PhysicalProtectionEffect(characterEffectSusceptible, 10, 6f));
    }
}