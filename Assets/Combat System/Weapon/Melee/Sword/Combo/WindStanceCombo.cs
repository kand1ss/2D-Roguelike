using System.Collections.Generic;

public class WindStanceCombo : Combo
{
    private ICharacter comboInitiator;
    private Sword sword;
    
    public WindStanceCombo(ICharacter comboInitiator, Sword sword)
    {
        this.comboInitiator = comboInitiator;
        this.sword = sword;
    }
    
    protected override void InitCombo()
    {
        ExpectedAttackSequence.Add(SwordAttackType.Weak);
        ExpectedAttackSequence.Add(SwordAttackType.Strong);
        ExpectedAttackSequence.Add(SwordAttackType.Weak);
    }

    public override void UseCombo(IEnumerable<ICharacter> entities)
    {
        if(comboInitiator is ICharacterEffectSusceptible characterEffectSusceptible)
            characterEffectSusceptible.EffectManager.ApplyEffect(
                new WindStanceEffect(characterEffectSusceptible, sword, 0.35f, 5, 5f));
    }
}