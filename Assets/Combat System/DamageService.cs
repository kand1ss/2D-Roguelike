using UnityEngine;

public static class DamageService
{
    public static void SendDamageToTarget(ICharacter attacker, ICharacter target, IStrikeDamage damageDealer)
    {
        var weaponDamage = Random.Range(
            damageDealer.BaseMinDamageAmount,
            damageDealer.BaseMaxDamageAmount);
        
        DamageType weaponDamageType = damageDealer.CurrentDamageType;
        
        CharacterResists targetResists = target.Resists;
        CharacterSkills attackerSkills = attacker.Skills;

        var attackSkill = GetAttackSkillByDamageType(attackerSkills, weaponDamageType);
        var attackerSkillMultiplier = GetAttackerSkillMultiplier(attackSkill);
        var targetResistModifier = GetTargetResistModifier(targetResists, weaponDamageType);

        int resultDamage = CalculateFinalDamage(weaponDamage, targetResistModifier, attackerSkillMultiplier);

        Debug.Log("-------- Damage Service ---------");
        Debug.Log($"Target Physical Resist: {(1 - targetResists.GetPhysicalDamageResistance) * 100}%");
        Debug.Log($"Target Magical Resist: {(1 - targetResists.GetMagicalDamageResistance) * 100}%");
        Debug.Log($"Calculated Damage: {resultDamage}");
        
        target.StatsManager.TakeDamage(resultDamage);
    }

    private static float GetAttackSkillByDamageType(CharacterSkills skills, DamageType damageType)
    {
        return damageType == DamageType.Physical ? skills.physicalSkillLevel : skills.magicalSkillLevel;
    }

    private static float GetAttackerSkillMultiplier(float skill)
    {
        return 1 + 0.1f * (skill - 1);
    }

    private static float GetTargetResistModifier(CharacterResists resists, DamageType weaponDamageType)
    {
        return weaponDamageType == DamageType.Physical ? resists.GetPhysicalDamageResistance : resists.GetMagicalDamageResistance;
    }

    private static int CalculateFinalDamage(float weaponDamage, float resistModifier, float skillMultiplier)
    {
        return Mathf.FloorToInt(weaponDamage * resistModifier * skillMultiplier);
    }
}