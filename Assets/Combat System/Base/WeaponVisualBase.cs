using System;
using UnityEngine;

public class WeaponVisualBase : MonoBehaviour
{
    protected Animator Animator;

    protected Quaternion InitialRotation;
    protected Vector3 InitialPosition;

    private void Awake()
    {
        InitiateFields();
    }

    protected void InitiateFields()
    {
        InitialRotation = transform.localRotation;
        InitialPosition = transform.localPosition;

        Animator = GetComponent<Animator>();
    }
    
    public virtual void AttachPlayerEvents()
    {
        Player.Instance.WeaponController.OnWeaponChanged -= ResetAnimation;
    }
    public virtual void DetachPlayerEvents()
    {
        Player.Instance.WeaponController.OnWeaponChanged -= ResetAnimation;
    }
    
    protected void ResetAnimation()
    {
        transform.localRotation = InitialRotation;
        transform.localPosition = InitialPosition;
    }
}
