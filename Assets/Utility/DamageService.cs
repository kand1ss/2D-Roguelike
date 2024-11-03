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
        var distanceMultiplier = GetDistanceMultiplier(attacker, target);

        int resultDamage = CalculateFinalDamage(
            weaponDamage, targetResistModifier, attackerSkillMultiplier, distanceMultiplier);

        Debug.Log("-------- Damage Service ---------");
        Debug.Log($"Target Physical Resist: {(1 - targetResists.GetPhysicalDamageResistance) * 100}%");
        Debug.Log($"Target Magical Resist: {(1 - targetResists.GetMagicalDamageResistance) * 100}%");
        Debug.Log($"Attack Distance: {distanceMultiplier}");
        Debug.Log($"Calculated Damage: {resultDamage}");
        
        target.StatsManager.TakeDamage(resultDamage);
    }

    public static void SendDamageByEffect(ICharacterEffectSusceptible target, float minDamage, float maxDamage)
    {
        var effectDamage = Mathf.FloorToInt(Random.Range(minDamage, maxDamage));
        
        target.StatsManager.TakeDamage(effectDamage);
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

    private static float GetDistanceMultiplier(ICharacter attacker, ICharacter target)
    {
        var distance = Vector2.Distance(attacker.transform.position, target.transform.position);
        return distance <= 1 ? 1 : 1 + 0.35f * Mathf.Log(distance);
    }

    private static int CalculateFinalDamage(float weaponDamage, float resistModifier, float skillMultiplier, float distance)
    {
        return Mathf.FloorToInt(weaponDamage * resistModifier * skillMultiplier * distance);
    }
}