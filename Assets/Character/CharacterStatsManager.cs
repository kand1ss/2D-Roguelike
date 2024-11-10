using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterStatsManager
{
    public UnityAction<float> OnHealthChanged;
    
    [SerializeField] private int maxHealth;
    public int MaxHealth => maxHealth;
    
    [SerializeField] private int currentHealth;

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (value <= maxHealth)
            {
                currentHealth = value;
                OnHealthChanged?.Invoke(value);
            }
        }
    }
    
    [SerializeField] private float walkingMoveSpeed = 6f;
    public float WalkingMoveSpeed
    {
        get => walkingMoveSpeed;
        set
        {
            if(value > 0)
                walkingMoveSpeed = value;
        }
    }

    public CharacterStatsManager()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
    }
}