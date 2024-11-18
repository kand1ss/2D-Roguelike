using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class EnemyAI : Entity, IEnemyAI
{
    protected Player player;
    protected Fsm stateMachine;

    public NavMeshAgent Agent { get; private set; }

    public float DistanceToPlayer => Vector3.Distance(player.transform.position, transform.position);

    private readonly float suspicionStateCheckInterval = 0.5f;
    private float suspicionStateCheckTimer;

    [Inject]
    private void Construct(Player player)
    {
        this.player = player;
    }

    protected override void Awake()
    {
        base.Awake();
        
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        suspicionStateCheckTimer = suspicionStateCheckInterval;
        
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
        if (stateMachine.CurrentState is not EnemyStateChasing && stateMachine.CurrentState is not FsmAttackingState)
            stateMachine.SetState<EnemyStateSuspicion>();
    }
    
    private void SuspicionStateTransition()
    {
        if (stateMachine.CurrentState is FsmAttackingState || stateMachine.CurrentState is EnemyStateChasing && !CanSeePlayer())
            stateMachine.SetState<EnemyStateSuspicion>();
    }
    
    protected bool CanSeePlayer()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 enemyPosition = transform.position;
        Vector2 directionToPlayer = (playerPosition - enemyPosition).normalized;

        RaycastHit2D hit = Physics2D.Raycast(enemyPosition, directionToPlayer, DistanceToPlayer, ~LayerMask.GetMask("Enemy"));
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
        
        HandleRaycastCheck();
    }

    private void HandleRaycastCheck()
    {
        suspicionStateCheckTimer -= Time.deltaTime;
        if (suspicionStateCheckTimer <= 0)
        {
            SuspicionStateTransition();
            suspicionStateCheckTimer = suspicionStateCheckInterval;
        }
    }
}