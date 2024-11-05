
using UnityEngine.Events;

public abstract class TimedEffect
{
    private const float EffectInterval = 1f;

    private float remainingDuration;
    private float intervalTimer;

    public UnityAction OnEffectAdded;
    public UnityAction OnEffectApplied;
    public UnityAction OnEffectRemoved;

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
                OnEffectRemoved?.Invoke();
                return;
            }
            OnEffectApplied?.Invoke();
        
            intervalTimer = EffectInterval;
        }
    }
}