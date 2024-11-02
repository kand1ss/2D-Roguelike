using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ComboSequenceController : MonoBehaviour
{
    private List<SwordAttackType> lastRegisteredAttacksList = new List<SwordAttackType>();
    private List<Combo> activeComboList = new List<Combo>();
    private List<ICharacter> currentEntitiesInCollision = new();

    public SwordAttackType lastAttackType;

    [SerializeField] private float timeUntilComboCleared = 2f;
    private float timerUntilComboReset;

    private void Awake()
    {
        timerUntilComboReset = timeUntilComboCleared;
    }

    private void Update()
    {
        timerUntilComboReset -= Time.deltaTime;
        if (timerUntilComboReset <= 0)
        {
            lastRegisteredAttacksList.Clear();
            timerUntilComboReset = timeUntilComboCleared;
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

    public void ClearCombo()
    {
        activeComboList.Clear();
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
        timerUntilComboReset = timeUntilComboCleared;

        if (lastRegisteredAttacksList.Count == 3)
            StartCoroutine(CheckComboAfterDelay());
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
            if (CompareSequences(combo.GetAttackSequence, lastRegisteredAttacksList))
            {
                Debug.Log("Push Combo!");
                combo.UseCombo(currentEntitiesInCollision);
            }
        }

        lastRegisteredAttacksList.Clear();
    }

    private bool CompareSequences(List<SwordAttackType> expectedSequence, List<SwordAttackType> currentSequence)
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