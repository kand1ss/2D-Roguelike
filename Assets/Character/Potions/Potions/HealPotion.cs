public class HealPotion : PermanentBuffPotion
{
    public HealPotion(ICharacter target, int buff) : base(target, buff)
    {
    }

    public override void ApplyBuff()
    {
        var targetStats = target.StatsManager;
        targetStats.CurrentHealth += buffValue;
    }
}