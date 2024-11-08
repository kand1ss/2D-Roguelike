using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEffectManager
{
    private readonly List<EffectType> ownerEffectResists;
    
    public List<IEffect> ActiveEffects { get; private set; } = new();
    private readonly List<IEffect> effectsToRemove = new();

    public UnityAction<IEffect> OnEffectAdded;
    public UnityAction<IEffect> OnEffectRemoved;

    public CharacterEffectManager(List<EffectType> resists)
    {
        ownerEffectResists = resists;
    }

    public void ApplyEffect(IEffect effect)
    {
        if (!ownerEffectResists.Contains(effect.EffectType))
        {
            if (ActiveEffects.Contains(effect)) 
                return;
            
            ActiveEffects.Add(effect);
            OnEffectAdded?.Invoke(effect);
        }
        else
            Debug.Log($"Сущность устойчива к эффекту: {effect.EffectType}");
    }

    public void RemoveEffect(IEffect effect)
    {
        effectsToRemove.Add(effect);
        OnEffectRemoved?.Invoke(effect);
    }

    public void ClearAllEffects()
    {
        foreach (var effect in ActiveEffects)
            effect.RemoveEffect();
        
        ActiveEffects.Clear();
    }

    public void UpdateEffects()
    {
        foreach (var effect in ActiveEffects)
        {
            if (effect is TimedEffect tickableEffect)
                tickableEffect.CheckPerSecond(Time.deltaTime);
        }

        foreach (var effect in effectsToRemove)
        {
            ActiveEffects.Remove(effect);
        }
        effectsToRemove.Clear();
    }
}