using System.Collections.Generic;
using UnityEngine;

public class EnemyStateSwordAttacking : FsmAttackingState
{
    private int attackIndexPointer;
    private IList<SwordAttackType> comboAttackList;

    public EnemyStateSwordAttacking(Enemy enemy, Fsm fsm, Player player, float attackDistance, float attackInterval) : base(enemy, fsm, player, attackDistance, attackInterval)
    {
    }

    public override void Enter()
    {
        Debug.Log("Attacking State: [ENTER]");
        SelectNewCombo();
    }

    public override void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            if (enemy is IEnemyWithWeapon enemyWithWeapon)
            {
                if (enemyWithWeapon.WeaponController.ChosenWeapon is Sword enemySword)
                    ExecuteComboAttack(enemySword);
            }

            attackTimer = attackInterval;
        }

        enemy.agent.SetDestination(target.transform.position);

        ChasingStateTransition();
    }

    private void SelectNewCombo()
    {
        if (enemy is IEnemyWithWeapon enemyWithWeapon &&
            enemyWithWeapon.WeaponController.ChosenWeapon is Sword enemyWithSword)
        {
            var comboList = enemyWithSword.GetComboManager().ComboController.GetActiveComboList();
            var comboCount = comboList.Count;


            if (comboList == null || comboCount == 0)
                return;

            var comboIndex = Random.Range(0, comboCount);

            Debug.LogWarning($"Selected Combo: {comboList[comboIndex].GetType().Name}");

            comboAttackList = comboList[comboIndex].GetAttackSequence;

            attackIndexPointer = 0;
        }
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