
using UnityEngine;
using UnityEngine.Events;

public abstract class TimedEffect
{
    private const float EffectApplyInterval = 1f;
    private float intervalTimer;

    private float effectDuration;
    private float remainingDuration;
    
    protected UnityAction EffectApplied;
    protected UnityAction EffectRemoved;

    protected TimedEffect(float duration)
    {
        effectDuration = duration;
        remainingDuration = duration;
        intervalTimer = EffectApplyInterval;
    }

    public void CheckPerSecond()
    {
        intervalTimer -= Time.deltaTime;
        if (intervalTimer <= 0)
        {
            remainingDuration -= 1f;
            if (remainingDuration <= 0)
            {
                remainingDuration = effectDuration;
                EffectRemoved?.Invoke();
                return;
            }
            EffectApplied?.Invoke();
        
            intervalTimer = EffectApplyInterval;
        }
    }
}