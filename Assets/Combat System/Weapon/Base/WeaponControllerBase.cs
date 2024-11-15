using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public abstract class WeaponControllerBase : MonoBehaviour, IWeaponController
{
    private ICharacter controllerOwner;

    [SerializeField] protected WeaponBase chosenWeapon;
    public WeaponBase ChosenWeapon => chosenWeapon;
    
    public void UseChosenWeapon() => ChosenWeapon.UseWeapon();

    public event UnityAction OnWeaponChanged;
    

    [Inject]
    private void Construct(ICharacter character)
    {
        controllerOwner = character;
    }
    
    private void Update()
    {
        FollowDirection();
    }

    private void FollowDirection()
    {
        Vector3 directionTarget = GetFollowDirectionTarget();
        Vector3 characterPosition = controllerOwner.transform.position;

        Vector3 direction = directionTarget - characterPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (directionTarget.x < characterPosition.x)
            transform.rotation = Quaternion.Euler(180, 0, -angle);
        else
            transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected abstract Vector2 GetFollowDirectionTarget();

    protected void WeaponChanged()
    {
        OnWeaponChanged?.Invoke();
    }
}