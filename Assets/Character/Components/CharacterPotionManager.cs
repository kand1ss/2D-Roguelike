using UnityEngine;
using UnityEngine.Events;

public class CharacterPotionManager
{
    private const int MAX_POTION_USES_COUNT = 3;
    
    private ICharacterEffectSusceptible owner;
    
    private IPotion chosenPotion;
    public int PotionRemainingUsesNumber { get; private set; }
    
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

    public void UsePotion()
    {
        if (CanUsePotion())
        {
            PotionRemainingUsesNumber--;
            owner.EffectManager.ApplyEffect(chosenPotion);
        }
        
        if(PotionRemainingUsesNumber <= 0)
            SetPotion(null);
    }

    public bool CanUsePotion() => chosenPotion != null && PotionRemainingUsesNumber > 0;
    public void ResetPotionUsage() => PotionRemainingUsesNumber = MAX_POTION_USES_COUNT;
    
    public EffectType GetChosenPotionEffectType() => chosenPotion.EffectType;
}