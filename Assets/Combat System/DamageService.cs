using UnityEngine;

public static class DamageService
{
    public static void SendDamage(ICharacter attacker, ICharacter attackTarget, IStrikeDamage damageStriker)
    {
        var weaponDamage = Random.Range(
            damageStriker.BaseMinDamageAmount,
            damageStriker.BaseMaxDamageAmount);
        
        DamageType strikerDamageType = damageStriker.CurrentDamageType;
        
        CharacterResists targetResists = attackTarget.Resists;
        var targetPhysicalResist = targetResists.GetPhysicalDamageResistance;
        var targetMagicalResist = targetResists.GetMagicalDamageResistance;
        
        Debug.Log($"Target Physical Resist: {targetPhysicalResist}");
        Debug.Log($"Target Magical Resist: {targetMagicalResist}");

        int resultDamage = Mathf.FloorToInt(
            weaponDamage * (
                strikerDamageType == DamageType.Physical ? targetPhysicalResist : targetMagicalResist));
        
        Debug.Log($"Calculated Damage: {resultDamage}");
        
        attackTarget.TakeDamage(resultDamage);
    }
    public static void SendDamage(ICharacter attackTarget, IStrikeDamage damageStriker)
    {
        var weaponDamage = Random.Range(
            damageStriker.BaseMinDamageAmount,
            damageStriker.BaseMaxDamageAmount);
        
        DamageType strikerDamageType = damageStriker.CurrentDamageType;
        
        CharacterResists targetResists = attackTarget.Resists;
        var targetPhysicalResist = targetResists.GetPhysicalDamageResistance;
        var targetMagicalResist = targetResists.GetMagicalDamageResistance;
        
        Debug.Log($"Target Physical Resist: {1 - targetPhysicalResist}");
        Debug.Log($"Target Magical Resist: {1 - targetMagicalResist}");

        int resultDamage = Mathf.FloorToInt(
            weaponDamage * (
                strikerDamageType == DamageType.Physical ? targetPhysicalResist : targetMagicalResist));
        
        Debug.Log($"Calculated Damage: {resultDamage}");
        
        attackTarget.TakeDamage(resultDamage);
    }
}