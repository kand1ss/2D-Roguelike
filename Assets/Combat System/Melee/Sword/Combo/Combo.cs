using System.Collections.Generic;

[System.Serializable]
public abstract class Combo
{
    protected readonly List<SwordAttackType> ExpectedAttackSequence = new List<SwordAttackType>();
    public List<SwordAttackType> GetAttackSequence => ExpectedAttackSequence;

    protected Combo()
    {
        InitCombo();
    }

    protected abstract void InitCombo();
    public abstract void UseCombo(IEnumerable<Entity> entities);
    public void UseCombo(Entity entity)
    {
        UseCombo(new Entity[] { entity });
    }
}