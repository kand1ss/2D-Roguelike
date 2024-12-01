using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStateSwordAttacking : FsmAttackingState
{
    private readonly IEnemyWithWeapon enemyWithWeapon;
    private readonly Sword enemySword;
    
    private int attackIndexPointer;
    private IList<SwordAttackType> comboAttackList;

    public EnemyStateSwordAttacking(EnemyAI enemyAI, Fsm stateMachine, Player player, float attackDistance, float attackInterval) :
        base(enemyAI, stateMachine, player, attackDistance, attackInterval)
    {
        enemyWithWeapon = enemyAI as IEnemyWithWeapon;
        enemySword = enemyWithWeapon?.WeaponController.ChosenWeapon as Sword;
    }

    public override void Enter()
    {
        Debug.Log("Attacking State: [ENTER]");
        SelectNewCombo();
    }

    private void SelectNewCombo()
    {
        var comboList = enemySword.GetComboManager().GetActiveComboList();
        var comboCount = comboList.Count;

        if (comboCount == 0)
            return;

        int comboIndex;
        bool isComboValid;

        do
        {
            comboIndex = Random.Range(0, comboCount);
            isComboValid = CheckEnemyCanUseCombo(comboIndex);
        } while (!isComboValid);
        
        comboAttackList = comboList[comboIndex].GetAttackSequence;

        attackIndexPointer = 0;
    }

    private bool CheckEnemyCanUseCombo(int comboIndex)
    {
        var comboList = enemySword.GetComboManager().GetActiveComboList();
        var chosenCombo = comboList[comboIndex];
        
        var enemyStatsManager = enemyAI.StatsManager;
        var enemyEffectManager = enemyAI.EffectManager;

        if (chosenCombo is StoneStanceCombo or WindStanceCombo)
        {
            if (enemyEffectManager.ActiveEffects.Any(effect => effect is StanceEffectBase))
                return false;
        }
        
        if (chosenCombo is PushCombo or StoneStanceCombo)
        {
            if (enemyStatsManager.CurrentHealth > enemyStatsManager.MaxHealth / 2)
                return false;
        }
        
        return true;
    }

    public override void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            ExecuteComboAttack(enemySword);
            attackTimer = attackInterval;
        }
        
        if(enemyAI.DistanceToPlayer > 1.3f)
            enemyAI.Agent.SetDestination(target.transform.position);

        ChasingStateTransition();
    }

    private void ExecuteComboAttack(Sword sword)
    {
        if (comboAttackList == null || comboAttackList.Count == 0)
            return;

        var currentAttack = comboAttackList[attackIndexPointer];

        switch (currentAttack)
        {
            case SwordAttackType.Strong:
                sword.ChargeAttack();
                break;
            case SwordAttackType.Weak:
                sword.UseWeapon();
                break;
        }

        attackIndexPointer++;

        if (attackIndexPointer == comboAttackList.Count)
            SelectNewCombo();
    }

    public override void Exit()
    {
        Debug.Log("Attacking State: [EXIT]");
        enemySword.GetComboManager().ClearLastRegisteredAttacks();
    }
}