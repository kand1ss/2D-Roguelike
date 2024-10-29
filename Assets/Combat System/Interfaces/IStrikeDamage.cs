
public interface IStrikeDamage
{
    float BaseMinDamageAmount { get; }
    float BaseMaxDamageAmount { get; }

    DamageType CurrentDamageType { get; }
}
