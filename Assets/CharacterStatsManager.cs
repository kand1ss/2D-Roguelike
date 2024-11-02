using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CharacterStatsManager
{
    public UnityAction<float> OnHealthChanged;
    
    [SerializeField] private int maxHealth;
    public int MaxHealth => maxHealth;
    
    private int currentHealth;

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (value <= maxHealth)
                currentHealth = value;
        }
    }

    public CharacterStatsManager()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        OnHealthChanged?.Invoke(CurrentHealth);
    }
}