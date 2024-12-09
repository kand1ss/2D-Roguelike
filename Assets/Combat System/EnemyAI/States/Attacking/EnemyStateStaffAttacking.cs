using System.Linq;
using UnityEngine;

public class EnemyStateStaffAttacking : FsmAttackingState
{
    private readonly Staff enemyStaff;
    
    public EnemyStateStaffAttacking(EnemyAI enemyAI, Fsm stateMachine, Player player) : base(enemyAI, stateMachine, player)
    {
        if(enemyAI is IHasWeapon enemyWithWeapon)
            enemyStaff = enemyWithWeapon?.WeaponController.ChosenWeapon as Staff;
        else
            Debug.LogWarning("Not Has Weapon");
    }

    public override void Enter()
    {
        Debug.Log("Attacking State: [ENTER]");
        enemyAI.Agent.ResetPath();
    }

    public override void Update()
    {
        base.Update();
        
        UseWeaponByInterval();
        AdjustMovementByDistance();

        RetreatStateTransition();
    }

    private void AdjustMovementByDistance()
    {
        var stopMovingDistance = enemySettings.attackingStartDistance - 0.5f;
        if(enemyAI.DistanceToPlayer > stopMovingDistance)
            enemyAI.Agent.SetDestination(target.transform.position);
        else
            enemyAI.Agent.ResetPath();
    }

    private void UseWeaponByInterval()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            SelectSpellByDistance();
            enemyStaff.UseWeapon();
            
            attackTimer = attackInterval;
        }
    }
    
    private void SelectSpellByDistance()
    {
        var spells = enemyStaff.GetMagicComponent().CurrentMagic.Spells;
        var distanceToPlayer = enemyAI.DistanceToPlayer;
        
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
    
    private void RetreatStateTransition()
    {
        var retreatDistance = enemySettings.retreatStartDistance;
        if (enemyAI.DistanceToPlayer <= retreatDistance && enemyAI.CanSeePlayer())
            StateMachine.SetState<FsmRetreatState>();
    }

    public override void Exit()
    {
        Debug.Log("Attacking State: [EXIT]");
    }
}