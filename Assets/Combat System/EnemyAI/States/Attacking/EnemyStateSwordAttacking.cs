using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSwordAttacking : FsmAttackingState
{
    private readonly IEnemyWithWeapon enemyWithWeapon;
    private readonly Sword enemySword;

    private int attackIndexPointer;
    private IList<SwordAttackType> comboAttackList;

    public EnemyStateSwordAttacking(Enemy enemy, Fsm fsm, Player player, float attackDistance, float attackInterval) :
        base(enemy, fsm, player, attackDistance, attackInterval)
    {
        enemyWithWeapon = enemy as IEnemyWithWeapon;
        enemySword = enemyWithWeapon?.WeaponController.ChosenWeapon as Sword;
    }

    public override void Enter()
    {
        Debug.Log("Attacking State: [ENTER]");
        SelectNewCombo();
    }

    private void SelectNewCombo()
    {
        var comboList = enemySword.GetComboManager().ComboController.GetActiveComboList();
        var comboCount = comboList.Count;

        if (comboCount == 0)
            return;

        var comboIndex = Random.Range(0, comboCount);
        comboAttackList = comboList[comboIndex].GetAttackSequence;

        attackIndexPointer = 0;
    }

    public override void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            ExecuteComboAttack(enemySword);
            attackTimer = attackInterval;
        }
        
        enemy.agent.SetDestination(target.transform.position);

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
    }
}