using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : MonoBehaviour, ICharacter
{
    public float MaxHealth => maxHealth;
    [SerializeField] private float maxHealth;
    public float CurrentHealth => currentHealth;
    [SerializeField] private float currentHealth;

    [field: SerializeField] public CharacterResists Resists { get; private set; }
    
    public Rigidbody2D rigidBody { get; private set; }
    
    public void Awake()
    {
        currentHealth = maxHealth;
        
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void Update()
    {
        if(currentHealth <= 0)
            Destroy(gameObject);
    }
}