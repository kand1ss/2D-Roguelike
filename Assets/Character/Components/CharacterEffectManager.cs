using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEffectManager
{
    private readonly List<EffectType> characterEffectResists;
    public List<IEffect> ActiveEffects { get; private set; } = new();
    
    private List<IEffect> effectsToRemove = new();

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

        if (ActiveEffects.Any(eff => eff.EffectType == EffectType.Stance && effect.EffectType == EffectType.Stance))
            return;
        
        if (!characterEffectResists.Contains(effect.EffectType) || effect.EffectType == EffectType.Buff)
        {
            ActiveEffects.Add(effect);
            effect.ApplyEffect();
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

    public bool HasEffectType(EffectType effectType)
    {
        foreach (var effect in ActiveEffects)
        {
            if (effect.EffectType == effectType)
                return true;
        }

        return false;
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
                tickableEffect.UpdatePerSecond();
        }
        
        if(effectsToRemove.Count > 0)
            Debug.LogWarning($"Effects to Remove: {effectsToRemove.Count}");

        foreach (var effect in effectsToRemove)
        {
            ActiveEffects.Remove(effect);
        }
        effectsToRemove.Clear();
    }
}