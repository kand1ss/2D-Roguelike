using UnityEngine;

public interface IPotion : IEffect
{
    int BuffValue { get; }
    Sprite PotionSprite { get; }
}