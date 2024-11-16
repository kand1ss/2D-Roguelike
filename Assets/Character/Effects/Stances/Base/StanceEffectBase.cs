public abstract class StanceEffectBase : BuffEffectBase
{
    protected readonly int debuffValue;
    
    protected StanceEffectBase(ICharacterEffectSusceptible effectTarget, float buffValue, int debuffValue, float duration) : base(effectTarget, buffValue, duration)
    {
        this.debuffValue = debuffValue;
    }
}