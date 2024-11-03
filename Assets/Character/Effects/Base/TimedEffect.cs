
public abstract class TimedEffect : ITickableEffect
{
    private const float EffectInterval = 1f;
    
    private float remainingDuration;
    private float intervalTimer;
    
    protected TimedEffect(float duration)
    {
        remainingDuration = duration;
        intervalTimer = EffectInterval;
    }

    public void CheckPerSecond(float deltaTime)
    {
        intervalTimer -= deltaTime;
        if (intervalTimer <= 0)
        {
            remainingDuration -= 1f;
            if (remainingDuration <= 0)
            {
                RemoveEffect();
                return;
            }
            ApplyEffect();
        
            intervalTimer = EffectInterval;
        }
    }

    public abstract void ApplyEffect();
    public abstract void RemoveEffect();
}