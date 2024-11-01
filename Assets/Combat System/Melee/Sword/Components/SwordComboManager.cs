using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ComboSequenceController))]
public class SwordComboManager : MonoBehaviour
{
    private ComboSequenceController comboChecker;

    public bool attackRegistered = false;


    private void Awake()
    {
        comboChecker = GetComponent<ComboSequenceController>();
    }

    public void InitiateComboAttacks(Combo combo)
    {
        comboChecker.AddCombo(combo);
    }

    public void InitiateComboAttacks(IEnumerable<Combo> comboList)
    {
        foreach (var combo in comboList)
            comboChecker.AddCombo(combo);
    }

    public void FinalizeComponent()
    {
        comboChecker.ClearCombo();
    }

    public void SetLastRegisteredAttack(SwordAttackType attackType)
    {
        comboChecker.lastAttackType = attackType;
    }

    public void SetAttackRegisteredFalse()
    {
        attackRegistered = false;
    }

    public void AddEntityToCombo(ICharacter entity)
    {
        comboChecker.AddEntityInList(entity);

        if (attackRegistered)
            return;

        comboChecker.RegisterAttack(comboChecker.lastAttackType);
        attackRegistered = true;
    }

    public void RemoveEntityFromCombo(ICharacter entity)
    {
        comboChecker.RemoveEntityFromList(entity);
    }
}