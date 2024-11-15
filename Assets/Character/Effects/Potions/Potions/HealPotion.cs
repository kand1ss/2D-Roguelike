using UnityEngine;

public class HealPotion : TimedBuffPotion
{
    public override Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/HealEffect");
    public override Sprite PotionSprite => Resources.Load<Sprite>("Sprites/Potions/HealPotion");
    
    
    public HealPotion(ICharacterEffectSusceptible effectTarget, int buff, float buffDuration) : base(effectTarget, buff, buffDuration)
    {
        EffectAppliedByInterval += ApplyEffect;
    }

    public override void ApplyEffect()
    {
        var targetStats = effectTarget.StatsManager;
        targetStats.CurrentHealth += buffValue;
    }
}