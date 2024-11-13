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

    public float PhysicalResistance
    {
        get => physicalDamageResistance;
        set
        {
            if (value <= 80 && value >= 0)
                physicalDamageResistance = value;
        }
    }

    public float MagicalResistance
    {
        get => magicalDamageResistance;
        set
        {
            if (value <= 80 && value >= 0)
                magicalDamageResistance = value;
        }
    }

    public float GetPhysicalDamageResistance => (1 - physicalDamageResistance / 100);
    public float GetMagicalDamageResistance => (1 - magicalDamageResistance / 100);
}