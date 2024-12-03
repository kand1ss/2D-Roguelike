
[System.Serializable]
public class EnemyAISettings
{
    public float idleTime;
    
    public float chasingStartDistance;
    public float chasingSpeed;
    
    public float roamingDistanceMin;
    public float roamingDistanceMax;
    public float roamingSpeed;
    
    public float attackingStartDistance;
    public float attackInterval;
    
    public float retreatSpeed;
    public float retreatStartDistance;
}