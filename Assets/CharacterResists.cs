using UnityEngine;

[System.Serializable]
public class CharacterResists
{
    [Range(0f, 80f)]
    public float physicalDamageResistance;
    [Range(0f, 80f)]
    public float magicalDamageResistance;
    
    public float GetPhysicalDamageResistance => (1 - physicalDamageResistance / 100);
    public float GetMagicalDamageResistance => (1 - magicalDamageResistance / 100);
}