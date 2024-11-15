using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ComboSequenceController))]
public class SwordComboManager : MonoBehaviour
{
    public ComboSequenceController ComboController { get; private set; }

    public bool attackRegistered = false;

    private void Awake()
    {
        ComboController = GetComponent<ComboSequenceController>();
    }

    public void InitiateComboAttack(Combo combo)
    {
        ComboController.AddCombo(combo);
    }

    public void FinalizeComponent()
    {
        ComboController.ClearCombo();
    }

    public void SetLastRegisteredAttack(SwordAttackType attackType)
    {
        ComboController.lastAttackType = attackType;
    }

    public void SetAttackRegisteredFalse()
    {
        attackRegistered = false;
    }

    public void AddEntityToCombo(ICharacter entity)
    {
        ComboController.AddEntityInList(entity);

        if (attackRegistered)
            return;

        ComboController.RegisterAttack(ComboController.lastAttackType);
        attackRegistered = true;
    }

    public void RemoveEntityFromCombo(ICharacter entity)
    {
        ComboController.RemoveEntityFromList(entity);
    }
}