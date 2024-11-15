using System;
using UnityEngine;

public class SwordCollisionHandler : MonoBehaviour
{
    public Action<Collider2D> HandleCollisionEnter;
    public Action<Collider2D> HandleCollisionExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollisionEnter?.Invoke(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        HandleCollisionExit?.Invoke(collision);
    }
}
