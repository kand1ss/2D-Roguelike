using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEffectManager
{
    private readonly List<EffectType> characterEffectResists;
    public List<IEffect> ActiveEffects { get; private set; } = new();
    
    private readonly List<IEffect> effectsToRemove = new();

    public UnityAction<IEffect> EffectAdded;
    public UnityAction<IEffect> EffectRemoved;

    public CharacterEffectManager(List<EffectType> resists)
    {
        characterEffectResists = resists;
    }

    public void ApplyEffect(IEffect effect)
    {
        if (ActiveEffects.Contains(effect)) 
            return;
        
        if (!characterEffectResists.Contains(effect.EffectType))
        {
            ActiveEffects.Add(effect);
            EffectAdded?.Invoke(effect);
        }
        else
            Debug.Log($"Сущность устойчива к эффекту: {effect.EffectType}");
    }

    public void RemoveEffect(IEffect effect)
    {
        effectsToRemove.Add(effect);
        EffectRemoved?.Invoke(effect);
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
                tickableEffect.CheckPerSecond();
        }

        foreach (var effect in effectsToRemove)
        {
            ActiveEffects.Remove(effect);
        }
        effectsToRemove.Clear();
    }
}