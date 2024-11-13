using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterPotionManager
{
    public List<TemporaryBuffPotion> ActivePotionBuffs { get; private set; } = new();
    private List<TemporaryBuffPotion> buffsToRemove = new();

    private IPotion chosenPotion;

    public void UseChosenPotion()
    {
        if (chosenPotion == null)
            return;
        
        if (chosenPotion is TemporaryBuffPotion tempPotion)
            ActivePotionBuffs.Add(tempPotion);

        chosenPotion.ApplyBuff();
        Debug.LogWarning($"Used {chosenPotion.GetType().Name}");
    }

    public void RemoveBuff(TemporaryBuffPotion buffPotion)
    {
        if (ActivePotionBuffs.Contains(buffPotion))
            buffsToRemove.Add(buffPotion);
    }

    public void ReplaceChosenPotion(IPotion potion)
    {
        chosenPotion = potion;
    }

    public void UpdateTemporaryBuff()
    {
        if (ActivePotionBuffs.Count == 0)
            return;
        
        foreach (var buff in ActivePotionBuffs)
        {
            buff?.CheckPerSecond();
        }

        foreach (var removeBuff in buffsToRemove)
        {
            ActivePotionBuffs.Remove(removeBuff);
        }
        buffsToRemove.Clear();
    }

    public void InitPlayerInput(IInputProvider inputProvider)
    {
        inputProvider.ButtonsController.ActionInput.OnPotionUsed += UseChosenPotion;
    }
}