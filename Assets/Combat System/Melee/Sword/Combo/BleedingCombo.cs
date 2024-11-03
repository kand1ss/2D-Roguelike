

using System.Collections.Generic;

public class BleedingCombo : Combo
{
    private readonly ICharacter comboInitiator;

    public BleedingCombo(ICharacter comboInitiator) : base()
    {
        this.comboInitiator = comboInitiator;
    }
    protected override void InitCombo()
    {
        ExpectedAttackSequence.Add(SwordAttackType.Strong);
        ExpectedAttackSequence.Add(SwordAttackType.Strong);
        ExpectedAttackSequence.Add(SwordAttackType.Weak);
    }

    public override void UseCombo(IEnumerable<ICharacter> entities)
    {
        foreach (var entity in entities)
        {
            if (entity is not ICharacterEffectSusceptible entityEffectSusceptible)
                return;
            
            entityEffectSusceptible.EffectManager.ApplyEffect(
                new BleedingEffect(entityEffectSusceptible, 3f, 6f, 4f));
        }
    }
}