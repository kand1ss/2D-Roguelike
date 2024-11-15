using UnityEngine;

public class BleedingEffect : DamageEffectBase
{
    public override EffectType EffectType => EffectType.Bleeding;
    public override Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/BleedingEffect");

    public BleedingEffect(ICharacterEffectSusceptible effectTarget, float minDamage, float maxDamage, float duration) : base(effectTarget, minDamage, maxDamage, duration)
    {
    }
}