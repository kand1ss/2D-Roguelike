using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PotionUserEnemyBehaviour : DefaultEnemyBehaviour, IPotionUser
{
    [SerializeField] private int healthValueForPotionUse;

    public CharacterPotionManager PotionManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        PotionManager = new CharacterPotionManager(this);
    }

    protected override void Start()
    {
        base.Start();
        
        PotionManager.SetPotion(new HealPotion(this, 5, 5f));
    }

    private void FixedUpdate()
    {
        var health = StatsManager.CurrentHealth;
        
        if(health < healthValueForPotionUse && PotionManager.CanUsePotion())
        {
            var potionEffect = PotionManager.GetChosenPotionEffectType();
            if(!EffectManager.HasEffectType(potionEffect))
                PotionManager.UsePotion();
        }
    }
}