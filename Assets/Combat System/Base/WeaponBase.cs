using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponBase : MonoBehaviour
{
    public abstract void UseWeapon();
    public abstract void InitWeapon();
    public abstract void DetachWeapon();
}
