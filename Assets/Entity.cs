using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour, ICharacter
{
    [field: SerializeField] public CharacterResists Resists { get; private set; }
    [field: SerializeField] public CharacterStatsManager StatsManager { get; set; }
    [field: SerializeField] public CharacterSkills Skills { get; private set; }
    
    public Rigidbody2D rigidBody { get; private set; }
    
    public void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StatsManager.OnHealthChanged += CheckIsDeath;
        StatsManager.CurrentHealth = StatsManager.MaxHealth;
    }

    private void CheckIsDeath(float currentHealth)
    {
        if(currentHealth <= 0)
            Destroy(gameObject);
    }
}