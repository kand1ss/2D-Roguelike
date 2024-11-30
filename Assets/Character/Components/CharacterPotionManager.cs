using UnityEngine;
using UnityEngine.Events;

public class CharacterPotionManager
{
    private const int MAX_POTION_USES_COUNT = 3;
    
    private ICharacterEffectSusceptible owner;
    
    private IPotion chosenPotion;
    private int chosenPotionUsesNumber;
    public event UnityAction<IPotion> PotionChanged;

    public CharacterPotionManager(ICharacterEffectSusceptible owner)
    {
        this.owner = owner;
    }
    
    public void SetPotion(IPotion potion)
    {
        ResetPotionUsage();
        chosenPotion = potion;

        PotionChanged?.Invoke(potion);
    }

    public void ResetPotionUsage() => chosenPotionUsesNumber = MAX_POTION_USES_COUNT;
    
    public void UsePotion()
    {
        if (chosenPotionUsesNumber > 0)
        {
            chosenPotionUsesNumber--;
            owner.EffectManager.ApplyEffect(chosenPotion);
        }
    }
}