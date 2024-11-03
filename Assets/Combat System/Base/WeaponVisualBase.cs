using UnityEngine;
using Zenject;

public class WeaponVisualBase : MonoBehaviour
{
    protected PlayerWeaponController weaponController;
    
    protected Animator Animator;

    private Quaternion initialRotation;
    private Vector3 initialPosition;

    [Inject]
    private void Construct(PlayerWeaponController weapon)
    {
        weaponController = weapon;
    }

    protected void Awake()
    {
        Animator = GetComponent<Animator>();
    }
    
    public virtual void AttachPlayerEvents()
    {
        weaponController.OnWeaponChanged += ResetAnimation;
    }
    public virtual void DetachPlayerEvents()
    {
        weaponController.OnWeaponChanged -= ResetAnimation;
    }
    
    protected void ResetAnimation()
    {
        transform.localRotation = initialRotation;
        transform.localPosition = initialPosition;
    }
}
