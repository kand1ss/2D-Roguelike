using UnityEngine;
using UnityEngine.Events;

public interface IEffect
{
    void ApplyEffect();
    void RemoveEffect(); 
    
    EffectType EffectType { get; }
    Sprite EffectIcon { get; }
}