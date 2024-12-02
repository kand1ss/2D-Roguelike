using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public abstract class EnemyAI : Entity, IEnemyAI
{
    protected Player player;
    protected Fsm stateMachine;

    public NavMeshAgent Agent { get; private set; }

    public float DistanceToPlayer => Vector3.Distance(player.transform.position, transform.position);

    [Inject]
    private void Construct(Player player)
    {
        this.player = player;
    }

    protected override void Awake()
    {
        base.Awake();
        
        InitializeAI();
    }

    private void InitializeAI()
    {
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        
        stateMachine = new Fsm();
    }

    protected override void Start()
    {
        base.Start();
        
        StatsManager.OnTakeDamage += SwitchToSuspicionStateAfterDamage;
        
        stateMachine.AddState(new EnemyStateSuspicion(this, stateMachine, player, 5f));
    }
    
    private void SwitchToSuspicionStateAfterDamage()
    {
        if (!(stateMachine.CurrentState is EnemyStateChasing) 
            && !(stateMachine.CurrentState is FsmAttackingState)
            && !(stateMachine.CurrentState is FsmRetreatState))
            
            stateMachine.SetState<EnemyStateSuspicion>();
    }
    
    public bool CanSeePlayer()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 enemyPosition = transform.position;
        Vector2 directionToPlayer = (playerPosition - enemyPosition).normalized;

        RaycastHit2D hit = Physics2D.Raycast(enemyPosition, directionToPlayer, DistanceToPlayer, ~LayerMask.GetMask("Enemy", "Weapon"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == player.gameObject)
                return true;
        }
        
        return false;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.Update();
    }
}