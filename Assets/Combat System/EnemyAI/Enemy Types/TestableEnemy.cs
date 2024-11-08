using UnityEngine;
using Zenject;

public class TestableEnemy : Enemy
{
    [SerializeField] private float idleTime;
    
    [SerializeField] private float roamingDistanceMin;
    [SerializeField] private float roamingDistanceMax;

    [SerializeField] private float chasingStartDistance;

    [SerializeField] private Player player;

    private new void Start()
    {
        base.Start();
        
        fsm.AddState(new EnemyStateIdle(this, fsm, idleTime));
        fsm.AddState(new EnemyStateRoaming(this, fsm, roamingDistanceMax, roamingDistanceMin));
        fsm.AddState(new EnemyStateChasing(this, fsm, player, chasingStartDistance));
        
        fsm.SetState<EnemyStateIdle>();
    }

    private new void Update()
    {
        base.Update();
        fsm.Update();
        
        TransitionsFromAnyState();
    }

    private void TransitionsFromAnyState()
    {
        ChasingStateTransition();
    }

    private void ChasingStateTransition()
    {
        var distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        
        if (distanceToPlayer <= chasingStartDistance)
            fsm.SetState<EnemyStateChasing>();
    }
}