using UnityEngine;

[System.Serializable]
public class CharacterResists
{
    [SerializeField] private int resistanceCap = 80;

    [SerializeField] private float physicalDamageResistance;
    [SerializeField] private float magicalDamageResistance;

    public float SetPhysicalResistance
    {
        set
        {
            if (value <= resistanceCap)
                physicalDamageResistance = value;
        }
    }
    
    public float SetMagicalResistance
    {
        set
        {
            if (value <= resistanceCap)
                magicalDamageResistance = value;
        }
    }

    public float GetPhysicalDamageResistance => (1 - physicalDamageResistance / 100);
    public float GetMagicalDamageResistance => (1 - magicalDamageResistance / 100);
}