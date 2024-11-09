using System;
using UnityEngine;

public class EnemyWeaponController : WeaponControllerBase
{
    [SerializeField] private Player player;
    
    private Quaternion initialChosenWeaponRotation;

    private void Awake()
    {
        ChosenWeapon.InitWeapon();
        
        initialChosenWeaponRotation = ChosenWeapon.transform.rotation;
    }

    protected override Vector2 GetFollowDirectionTarget()
    {
        return player.transform.position;
    }

    public void ResetWeaponRotation()
    {
        chosenWeapon.transform.rotation = initialChosenWeaponRotation;
    }
}