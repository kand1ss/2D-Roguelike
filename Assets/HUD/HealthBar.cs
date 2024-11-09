using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBar : MonoBehaviour
{
    private ICharacter player;
    
    [SerializeField] private Image healthBar;
    [SerializeField] private Image healthBackground;
    
    private Coroutine healthBackgroundCoroutine;

    [Inject]
    private void Construct(ICharacter player)
    {
        this.player = player;
    }

    private void Start()
    {
        player.StatsManager.OnHealthChanged += HealthChanged;
    }

    private void HealthChanged(float health)
    {
        healthBar.fillAmount = health / 100;

        healthBackgroundCoroutine = StartCoroutine(HealthBackgroundSubstractCoroutine());
    }

    IEnumerator HealthBackgroundSubstractCoroutine()
    {
        if(healthBackgroundCoroutine != null)
            StopCoroutine(healthBackgroundCoroutine);
        
        var duration = 0.5f;
        var startFillAmount = healthBackground.fillAmount;
        var endFillAmount = healthBar.fillAmount;
        var elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            healthBackground.fillAmount = Mathf.Lerp(startFillAmount, endFillAmount, elapsed / duration);
            yield return null;
        }

        healthBackground.fillAmount = healthBar.fillAmount;
    }
}
