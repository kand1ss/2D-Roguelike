using UnityEngine;

public class PoisonEffect : DamageEffectBase
{
    public override EffectType EffectType => EffectType.Poison;
    public override Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/PoisonEffect");

    public PoisonEffect(ICharacterEffectSusceptible effectTarget, float minDamage, float maxDamage, float duration) : base(effectTarget, minDamage, maxDamage, duration)
    {
    }
}