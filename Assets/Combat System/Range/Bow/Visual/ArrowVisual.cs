using System;
using UnityEngine;
using UnityEngine.Events;

public class ArrowVisual : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public UnityAction OnArrowOutOfScreenBounds;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnBecameInvisible()
    {
        OnArrowOutOfScreenBounds?.Invoke();
    }
}