using UnityEngine;

public class BleedingEffect : DamageEffectBase
{
    public BleedingEffect(ICharacterEffectSusceptible effectTarget, float minDamage, float maxDamage, float duration) : base(effectTarget, minDamage, maxDamage, duration)
    {
    }

    public override EffectType EffectType => EffectType.Bleeding;
    public override Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/BleedingEffect");
}