using System;

[Serializable]
public class PermanentBuffPotion : IPotion
{
    [NonSerialized] protected ICharacter target;
    protected int buffValue;

    public PermanentBuffPotion(ICharacter target, int buff)
    {
        this.target = target;
        buffValue = buff;
    }

    public virtual void ApplyBuff() {}
}