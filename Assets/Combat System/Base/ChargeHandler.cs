using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class ChargeHandler : MonoBehaviour, IChargeable
{
    private bool isCharging;
    public bool IsCharging => isCharging;
    
    [SerializeField] private ActionProgressBar progressBar;
    
    public event UnityAction<float> OnChargeAttackStart;
    public event UnityAction OnChargeAttackStopped;
    public event UnityAction OnChargeAttackCompleted;
        
    private Coroutine holdingCoroutine;

    private void Start()
    {
        OnChargeAttackStart += progressBar.ShowProgressBar;
        OnChargeAttackStopped += progressBar.HideProgressBar;
    }

    private void OnDestroy()
    {
        OnChargeAttackStart -= progressBar.ShowProgressBar;
        OnChargeAttackStopped -= progressBar.HideProgressBar;
    }

    public void StartCharging(float holdTime)
    {
        if (isCharging)
            return;
        if (holdingCoroutine != null)
            return;
        
        holdingCoroutine = StartCoroutine(ChargeRoutine(holdTime));
    }

    public void StopCharging()
    {
        if (holdingCoroutine != null)
        {
            StopCoroutine(holdingCoroutine);
            holdingCoroutine = null;
        }
        isCharging = false;
        OnChargeAttackStopped?.Invoke();
    }

    private IEnumerator ChargeRoutine(float holdTime)
    {
        isCharging= true;
        float chargingHoldTimer = 0f;

        OnChargeAttackStart?.Invoke(holdTime);
        while (chargingHoldTimer < holdTime)
        {
            chargingHoldTimer += Time.deltaTime;

            if (chargingHoldTimer >= holdTime)
            {
                OnChargeAttackCompleted?.Invoke();
                break;
            }

            yield return null;
        }

        StopCharging();
    }
}