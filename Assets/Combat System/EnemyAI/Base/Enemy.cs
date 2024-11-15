using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity, IEnemyAI
{
    [SerializeField] protected Player player;
    protected Fsm fsm;

    public NavMeshAgent agent { get; private set; }

    public float DistanceToPlayer => Vector3.Distance(player.transform.position, transform.position);

    protected override void Awake()
    {
        base.Awake();
        
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        fsm = new Fsm();
    }

    protected override void Update()
    {
        base.Update();
        fsm.Update();
    }
}