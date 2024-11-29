using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ComboSequenceController))]
public class SwordComboManager : MonoBehaviour
{
    private ComboSequenceController comboController;

    public bool attackRegistered = false;

    private void Awake()
    {
        comboController = GetComponent<ComboSequenceController>();
    }

    public void InitiateComboAttack(Combo combo)
    {
        comboController.AddCombo(combo);
    }

    public void FinalizeComponent()
    {
        comboController.ClearRegisteredCombos();
    }

    public void SetLastRegisteredAttack(SwordAttackType attackType)
    {
        comboController.lastAttackType = attackType;
    }

    public void SetAttackRegisteredFalse()
    {
        attackRegistered = false;
    }

    public IList<Combo> GetActiveComboList() => comboController.GetActiveComboList();
    public void ClearLastRegisteredAttacks() => comboController.ClearLastRegisteredAttacks();

    public void AddEntityToCombo(ICharacter entity)
    {
        comboController.AddEntityInList(entity);

        if (attackRegistered)
            return;

        comboController.RegisterAttack(comboController.lastAttackType);
        attackRegistered = true;
    }

    public void RemoveEntityFromCombo(ICharacter entity)
    {
        comboController.RemoveEntityFromList(entity);
    }
}