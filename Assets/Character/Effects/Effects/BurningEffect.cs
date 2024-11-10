using UnityEngine;

public class BurningEffect : DamageEffectBase
{
    public BurningEffect(ICharacterEffectSusceptible effectTarget, float minDamage, float maxDamage, float duration) :
        base(effectTarget, minDamage, maxDamage, duration)
    {
    }

    public override EffectType EffectType => EffectType.Burning;
    public override Sprite EffectIcon => Resources.Load<Sprite>("Sprites/Effects/BurningEffect");
}