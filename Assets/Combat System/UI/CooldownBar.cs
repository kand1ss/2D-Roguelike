using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    public static CooldownBar Instance { get; private set; }

    private CanvasGroup progressCanvasGroup;

    [SerializeField] private Image progressBar;
    [SerializeField] private Image progressBackground;

    private Coroutine progressCoroutine;

    private void Awake()
    {
        progressCanvasGroup = GetComponent<CanvasGroup>();

        Instance = this;
    }

    private void Start()
    {
        HideProgressBar();
    }

    public void ShowProgressBar(float duration)
    {
        if (!progressBar)
            return;

        if (progressCoroutine != null)
            StopProgressBar();

        progressCanvasGroup.alpha = 1f;
        progressCoroutine = StartCoroutine(FillProgressBar(duration));
    }

    private IEnumerator FillProgressBar(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            progressBar.fillAmount = timer / duration;

            if(progressBar.fillAmount == 1)
                HideProgressBar();

            yield return null;
        }
    }

    private void HideProgressBar()
    {
        if (progressBar && progressBackground)
            progressCanvasGroup.alpha = 0f;
    }

    private void StopProgressBar()
    {
        if (!progressBar)
            return;

        StopCoroutine(progressCoroutine);
        HideProgressBar();
        progressCoroutine = null;
    }
}