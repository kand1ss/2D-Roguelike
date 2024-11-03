using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectManager
{
    public List<IEffect> ActiveEffects { get; private set; } = new();
    private readonly List<IEffect> effectsToRemove = new();

    public void ApplyEffect(IEffect effect)
    {
        if(!ActiveEffects.Contains(effect))
            ActiveEffects.Add(effect);
    }

    public void RemoveEffect(IEffect effect)
    {
        effectsToRemove.Add(effect);
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
            if (effect is ITickableEffect tickableEffect)
                tickableEffect.CheckPerSecond(Time.deltaTime);
        }

        foreach (var effect in effectsToRemove)
        {
            ActiveEffects.Remove(effect);
        }
        effectsToRemove.Clear();
    }
}