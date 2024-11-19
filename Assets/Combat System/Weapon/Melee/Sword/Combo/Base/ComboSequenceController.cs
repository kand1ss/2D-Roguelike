using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ComboSequenceController : MonoBehaviour
{
    private readonly List<SwordAttackType> lastRegisteredAttacksList = new List<SwordAttackType>();
    private readonly List<Combo> activeComboList = new List<Combo>();
    private readonly List<ICharacter> currentEntitiesInCollision = new();

    public SwordAttackType lastAttackType;

    [SerializeField] private float timeUntilComboReset = 2f;
    private float comboResetTimer;

    private void Awake()
    {
        comboResetTimer = timeUntilComboReset;
    }

    private void Update()
    {
        CheckComboReset();
    }

    private void CheckComboReset()
    {
        comboResetTimer -= Time.deltaTime;
        if (comboResetTimer <= 0)
        {
            lastRegisteredAttacksList.Clear();
            comboResetTimer = timeUntilComboReset;
        }
    }

    public void AddCombo(Combo combo)
    {
        if (!activeComboList.Contains(combo))
            activeComboList.Add(combo);
    }

    public void RemoveCombo(Combo combo)
    {
        if (activeComboList.Contains(combo))
            activeComboList.Remove(combo);
    }

    public void ClearRegisteredCombos()
    {
        activeComboList.Clear();
    }

    public IList<Combo> GetActiveComboList()
    {
        return activeComboList;
    }

    public void AddEntityInList(ICharacter entity)
    {
        if (!currentEntitiesInCollision.Contains(entity))
            currentEntitiesInCollision.Add(entity);
    }

    public void RemoveEntityFromList(ICharacter entity)
    {
        if (currentEntitiesInCollision.Contains(entity))
            currentEntitiesInCollision.Remove(entity);
    }

    public void RegisterAttack(SwordAttackType attackType)
    {
        lastRegisteredAttacksList.Add(attackType);
        comboResetTimer = timeUntilComboReset;

        if (lastRegisteredAttacksList.Count == 3)
            StartCoroutine(CheckComboAfterDelay());
    }

    public void ClearLastRegisteredAttacksList()
    {
        lastRegisteredAttacksList.Clear();
    }

    private IEnumerator CheckComboAfterDelay()
    {
        yield return new WaitForSeconds(0.05f);
        CheckCombo();
    }

    private void CheckCombo()
    {
        foreach (var combo in activeComboList)
        {
            if (CompareAttackSequences(combo.GetAttackSequence, lastRegisteredAttacksList))
            {
                combo.UseCombo(currentEntitiesInCollision);
                CinemachineShake.Instance.Shake(0.2f, 2.5f);
            }
        }

        lastRegisteredAttacksList.Clear();
    }

    private bool CompareAttackSequences(List<SwordAttackType> expectedSequence, List<SwordAttackType> currentSequence)
    {
        if (expectedSequence.Count != currentSequence.Count)
            return false;

        for (int i = 0; i < expectedSequence.Count; i++)
        {
            if (expectedSequence[i] != currentSequence[i])
                return false;
        }

        return true;
    }
}