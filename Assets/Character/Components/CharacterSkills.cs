
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class CharacterSkills
{
    [SerializeField] private float physicalSkillLevel;
    [SerializeField] private float magicalSkillLevel;

    public float PhysicalSkillLevel
    {
        get => physicalSkillLevel;
        set
        {
            if(value > 0) 
                physicalSkillLevel = value;
            else
                physicalSkillLevel = 1;
        }
    }

    public float MagicalSkillLevel
    {
        get => magicalSkillLevel;
        set
        {
            if(value > 0) 
                magicalSkillLevel = value;
            else
                magicalSkillLevel = 1;
        }
    }
}