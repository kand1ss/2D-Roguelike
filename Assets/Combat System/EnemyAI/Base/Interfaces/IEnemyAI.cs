using UnityEngine;
using UnityEngine.AI;

public interface IEnemyAI
{
    NavMeshAgent Agent { get; }
    EnemyAISettings AiSettings { get; }
    
    Transform transform { get; }
    bool CanSeePlayer();
    
    float DistanceToPlayer { get; }
}