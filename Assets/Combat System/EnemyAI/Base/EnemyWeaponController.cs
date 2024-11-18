using UnityEngine;
using Zenject;

public class EnemyWeaponController : WeaponControllerBase
{
    private Player player;

    [Inject]
    private void Construct(Player player)
    {
        this.player = player;
    }

    private void Awake()
    {
        ChosenWeapon.InitWeapon();
    }

    protected override Vector2 GetFollowDirectionTarget()
    {
        return player.transform.position;
    }
}