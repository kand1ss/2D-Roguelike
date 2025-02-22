﻿using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour, ICharacterEffectSusceptible
{
    public CharacterEffectManager EffectManager { get; private set; }
    [field: SerializeField] public CharacterResists Resists { get; private set; }
    [field: SerializeField] public CharacterStatsManager StatsManager { get; set; }
    [field: SerializeField] public CharacterSkills Skills { get; private set; }
    
    public Rigidbody2D rigidBody { get; private set; }
    
    protected virtual void Awake()
    {
        EffectManager = new CharacterEffectManager(Resists.ownerEffectResistances);
        
        rigidBody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {
        StatsManager.OnHealthChanged += CheckIsDeath;
        StatsManager.CurrentHealth = StatsManager.MaxHealth;
    }

    private void CheckIsDeath(float currentHealth)
    {
        if(currentHealth <= 0)
            Destroy(gameObject);
    }

    protected virtual void Update()
    {
        EffectManager.UpdateEffects();
    }
}