using System.Linq;
using UnityEngine;

public class EnemyStateStaffAttacking : FsmAttackingState
{
    private IEnemyWithWeapon enemyWithWeapon;
    private Staff enemyStaff;
    
    public EnemyStateStaffAttacking(Enemy enemy, Fsm fsm, Player player, float attackDistance, float attackInterval) : base(enemy, fsm, player, attackDistance, attackInterval)
    {
        enemyWithWeapon = enemy as IEnemyWithWeapon;
        enemyStaff = enemyWithWeapon?.WeaponController.ChosenWeapon as Staff;
    }

    public override void Enter()
    {
        Debug.Log("Attacking State: [ENTER]");
    }

    public override void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            SelectSpellByDistance();
            enemyStaff.UseWeapon();
            
            attackTimer = attackInterval;
        }

        enemy.agent.SetDestination(target.transform.position);

        ChasingStateTransition();
    }
    
    private void SelectSpellByDistance()
    {
        var spells = enemyStaff.GetMagicComponent().CurrentMagic.Spells;
        var distanceToPlayer = enemy.DistanceToPlayer;
        
        var suitableSpells = 
            spells.Where(spell => spell.projectilePrefab.ProjectileRange >= distanceToPlayer).ToList();

        if (suitableSpells.Count == 0)
        {
            enemyStaff.GetMagicComponent().ChosenSpellIndex = 0;
            return;
        }
        
        suitableSpells.Sort((spell1, spell2) => 
            spell1.projectilePrefab.ProjectileRange.CompareTo(spell2.projectilePrefab.ProjectileRange));

        var chosenSpell = suitableSpells.First();
        var chosenSpellIndex = spells.IndexOf(chosenSpell);
        
        enemyStaff.GetMagicComponent().ChosenSpellIndex = chosenSpellIndex;
    }

    public override void Exit()
    {
        Debug.Log("Attacking State: [EXIT]");
    }
}