using UnityEngine;
using UnityEngine.AI;

public class Enemy : Entity, IEnemyAI
{
    protected Fsm fsm;
    
    private NavMeshAgent navMeshAgent;
    public NavMeshAgent agent => navMeshAgent;

    private new void Awake()
    {
        base.Awake();
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        
        fsm = new Fsm();
    }
}