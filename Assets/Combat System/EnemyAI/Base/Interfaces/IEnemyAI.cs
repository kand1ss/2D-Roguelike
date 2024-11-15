using UnityEngine;
using UnityEngine.AI;

public interface IEnemyAI
{
    NavMeshAgent agent { get; }
    Transform transform { get; }
}