
public interface IChargeableWeapon
{
    ChargeHandler ChargeHandle { get; }
    void ChargeAttack();
}