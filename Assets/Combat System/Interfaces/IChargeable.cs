using UnityEngine.Events;

public interface IChargeable
{
    void StartCharging(float holdTime);
    void StopCharging();
    bool IsCharging { get; }
    
    event UnityAction<float> OnChargeAttackStart;
    event UnityAction OnChargeAttackStopped;
    event UnityAction OnChargeAttackCompleted;
}