using System.Collections;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    
    private CinemachineVirtualCamera cinemachine;
    private CinemachineBasicMultiChannelPerlin perlin;

    private void Awake()
    {
        cinemachine = GetComponent<CinemachineVirtualCamera>();
        perlin = cinemachine.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        
        Instance = this;
    }

    public void Shake(float shakeTime, float shakeIntensity)
    {
        StartCoroutine(ShakeCamera(shakeTime, shakeIntensity));
    }

    private IEnumerator ShakeCamera(float shakeTime, float shakeIntensity)
    {
        perlin.m_AmplitudeGain = shakeIntensity;
        
        yield return new WaitForSeconds(shakeTime);
        
        perlin.m_AmplitudeGain = 0f;
    }
}