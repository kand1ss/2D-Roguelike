using UnityEngine;
using UnityEngine.Events;

public class CharacterPotionManager
{
    private ICharacterEffectSusceptible owner;
    private IPotion chosenPotion;
    public event UnityAction<IPotion> PotionChanged;

    public CharacterPotionManager(ICharacterEffectSusceptible owner)
    {
        this.owner = owner;
    }
    
    public void SetPotion(IPotion potion)
    {
        chosenPotion = potion;
        PotionChanged?.Invoke(potion);
    }
    
    public void UsePotion()
    {
        owner.EffectManager.ApplyEffect(chosenPotion);
    }
}