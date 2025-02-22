﻿using UnityEngine;

public abstract class MeleeWeapon : WeaponBase, IStrikeDamage
{
    [SerializeField] protected float baseMinDamageAmount;
    [SerializeField] protected float baseMaxDamageAmount;

    [SerializeField] protected float currentAttackSpeed;

    public float BaseMinDamageAmount
    {
        get => baseMinDamageAmount;
        protected set => baseMinDamageAmount = value;
    }

    public float BaseMaxDamageAmount
    {
        get => baseMaxDamageAmount;
        protected set => baseMaxDamageAmount = value;
    }

    public DamageType CurrentDamageType { get; protected set; } = DamageType.Physical;

    public float CurrentAttackSpeed
    {
        get => currentAttackSpeed;
        set
        {
            if (value > 0)
                currentAttackSpeed = value;
        }
    }

    public float DamageAmount => Random.Range(BaseMinDamageAmount, BaseMaxDamageAmount);
}