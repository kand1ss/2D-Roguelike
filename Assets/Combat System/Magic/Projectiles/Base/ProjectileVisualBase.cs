using UnityEngine.Events;
using UnityEngine;

public class ProjectileVisualBase : MonoBehaviour
{
    [SerializeField] protected ProjectileBase projectile;

    public event UnityAction OnHitAnimationEnds;
    protected Animator Animator;

    protected void Awake()
    {
        Animator = GetComponent<Animator>();
    }  

    protected void HitAnimationEnds()
    {
        OnHitAnimationEnds?.Invoke();
    }
}