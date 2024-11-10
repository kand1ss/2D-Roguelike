using System;
using UnityEngine;

public class EnemyWeaponController : WeaponControllerBase
{
    [SerializeField] private Player player;

    private void Awake()
    {
        ChosenWeapon.InitWeapon();
    }

    protected override Vector2 GetFollowDirectionTarget()
    {
        return player.transform.position;
    }
}