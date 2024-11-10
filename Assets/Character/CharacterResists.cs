using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterResists
{
    [SerializeField] public List<EffectType> ownerEffectResistances;

    [Range(0, 80)] 
    [SerializeField] private float physicalDamageResistance;

    [Range(0, 80)] 
    [SerializeField] private float magicalDamageResistance;

    public float SetPhysicalResistance
    {
        set => physicalDamageResistance = value;
    }

    public float SetMagicalResistance
    {
        set => magicalDamageResistance = value;
    }

    public float GetPhysicalDamageResistance => (1 - physicalDamageResistance / 100);
    public float GetMagicalDamageResistance => (1 - magicalDamageResistance / 100);
}