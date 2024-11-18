using UnityEngine;
using UnityEngine.AI;

public interface IEnemyAI
{
    NavMeshAgent Agent { get; }
    
    Transform transform { get; }
}