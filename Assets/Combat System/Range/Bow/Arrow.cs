using System;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour, IStrikeDamage
{
    public ICharacter Sender;
    
    private ArrowVisual arrowVisual;
    private Rigidbody2D rigidBody;

    [SerializeField] private float arrowMinDamage;
    [SerializeField] private float arrowMaxDamage;

    public float BaseMinDamageAmount => arrowMinDamage;
    public float BaseMaxDamageAmount => arrowMaxDamage;
    public DamageType CurrentDamageType => DamageType.Physical;

    private void Awake()
    {
        arrowVisual = GetComponentInChildren<ArrowVisual>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        arrowVisual.OnArrowOutOfScreenBounds += OnDestroy;
    }

    private void OnDestroy()
    {
        arrowVisual.OnArrowOutOfScreenBounds -= OnDestroy;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnDestroy();

        if (!collision.TryGetComponent<ICharacter>(out ICharacter character))
            return;
        
        if(character is ICharacterEffectSusceptible effectSusceptible)
            effectSusceptible.EffectManager.ApplyEffect(
                new BleedingEffect(effectSusceptible, 1f, 2f, 5f));
        
        DamageService.SendDamageToTarget(Sender, character, this);
    }
}
