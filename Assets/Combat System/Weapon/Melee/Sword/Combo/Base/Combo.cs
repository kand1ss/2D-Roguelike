using System.Collections.Generic;
using UnityEngine;

public abstract class Combo
{
    protected readonly List<SwordAttackType> ExpectedAttackSequence = new List<SwordAttackType>();
    public List<SwordAttackType> GetAttackSequence => ExpectedAttackSequence;

    protected Combo()
    {
        InitCombo();
    }

    protected abstract void InitCombo();

    public abstract void UseCombo(IEnumerable<ICharacter> entities);
    public void UseCombo(ICharacter entity)
    {
        UseCombo(new ICharacter[] { entity });
    }
}