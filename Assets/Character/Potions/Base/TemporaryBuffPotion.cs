using System;
using UnityEngine;

[Serializable]
public class TemporaryBuffPotion : IPotion
{
    private const float EffectApplyInterval = 1f;
    
    [NonSerialized] protected ICharacter target;
    protected int buffValue;
    
    private float remainingDurationTimer;

    private float intervalTimer;

    public TemporaryBuffPotion(ICharacter target, int buff, float buffDuration)
    {
        this.target = target;
        buffValue = buff;
        
        remainingDurationTimer = buffDuration;
        intervalTimer = EffectApplyInterval;
    }

    public void CheckPerSecond()
    {
        intervalTimer -= Time.deltaTime;
        if (intervalTimer <= 0)
        {
            remainingDurationTimer -= 1f;
            if (remainingDurationTimer <= 0)
            {
                RemoveBuff();
                
                if(target is IPotionUser targetPotionUser)
                    targetPotionUser.PotionManager.RemoveBuff(this);
                
                return;
            }
            
            intervalTimer = EffectApplyInterval;
        }
    }

    public virtual void ApplyBuff() {}
    public virtual void RemoveBuff() {}
}