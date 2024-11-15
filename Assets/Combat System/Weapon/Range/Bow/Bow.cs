using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : WeaponBase, IChargeableWeapon
{
    private BowShootManager shootManager;
    private BowArrowsManager arrowsManager;
    
    public BowShootManager GetAttackManager() => shootManager;
    public BowArrowsManager GetArrowManager() => arrowsManager;
    public ChargeHandler ChargeHandle => shootManager;

    private void Awake()
    {
        shootManager = GetComponent<BowShootManager>();
        arrowsManager = GetComponent<BowArrowsManager>();
    }

    public override void UseWeapon()
    {
        shootManager.StartChargingShoot();
    }

    public override void InitWeapon()
    {
        shootManager.InitializeComponent();
    }

    public override void DetachWeapon()
    {
        shootManager.FinalizeComponent();
    }

    public void ChargeAttack()
    {
        UseWeapon();
    }
}
