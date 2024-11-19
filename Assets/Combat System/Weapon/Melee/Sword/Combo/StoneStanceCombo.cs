using System.Collections.Generic;

public class StoneStanceCombo : Combo
{
    private readonly ICharacter comboInitiator;

    public StoneStanceCombo(ICharacter comboInitiator) : base()
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
        if (comboInitiator is ICharacterEffectSusceptible characterEffectSusceptible)
            characterEffectSusceptible.EffectManager.ApplyEffect(
                new StoneStanceEffect(characterEffectSusceptible, 10, 5, 6f));
    }
}