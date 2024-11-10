using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity, IEnemyAI
{
    protected Fsm fsm;
    
    private NavMeshAgent navMeshAgent;
    public NavMeshAgent agent => navMeshAgent;
    
    [SerializeField] protected Player player;
    public float DistanceToPlayer => Vector3.Distance(player.transform.position, transform.position);


    protected override void Awake()
    {
        base.Awake();
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        
        fsm = new Fsm();
    }

    protected override void Update()
    {
        base.Update();
        fsm.Update();
    }
}