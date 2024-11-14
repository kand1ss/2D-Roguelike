using UnityEngine;

public class HealPotion : TimedBuffPotion
{
    public override Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/HealEffect");
    public override Sprite PotionSprite => Resources.Load<Sprite>("Sprites/Potions/HealPotion");


    public HealPotion(ICharacterEffectSusceptible target, int buff, float buffDuration) : base(target, buff, buffDuration)
    {
        EffectApplied += ApplyEffect;
    }

    public override void ApplyEffect()
    {
        var targetStats = target.StatsManager;
        targetStats.CurrentHealth += buffValue;
    }
}