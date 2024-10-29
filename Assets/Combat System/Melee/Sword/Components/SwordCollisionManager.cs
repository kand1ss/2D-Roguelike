using UnityEngine;
using UnityEngine.Events;

public class SwordCollisionManager : MonoBehaviour
{
    private PolygonCollider2D strongAttackCollision;
    private SwordCollisionHandler strongAttackHandler;
    private PolygonCollider2D weakAttackCollision;
    private SwordCollisionHandler weakAttackHandler;


    public event UnityAction<Entity> OnEntityEnterCollision;
    public event UnityAction<Entity> OnEntityExitCollision;

    private void Awake()
    {
        InitiateCollision();
        DisableWeaponCollision();
    }

    private void InitiateCollision()
    {
        strongAttackHandler = transform.Find("StrongAttack").GetComponent<SwordCollisionHandler>();
        weakAttackHandler = transform.Find("WeakAttack").GetComponent<SwordCollisionHandler>();
        strongAttackCollision = transform.Find("StrongAttack").GetComponent<PolygonCollider2D>();
        weakAttackCollision = transform.Find("WeakAttack").GetComponent<PolygonCollider2D>();
    }

    public void InitializeComponent()
    {
        strongAttackHandler.HandleCollisionEnter += HandleTriggerEnter;
        weakAttackHandler.HandleCollisionEnter += HandleTriggerEnter;
        strongAttackHandler.HandleCollisionExit += HandleTriggerExit;
        weakAttackHandler.HandleCollisionExit += HandleTriggerExit;
    }

    public void FinalizeComponent()
    {
        strongAttackHandler.HandleCollisionEnter -= HandleTriggerEnter;
        weakAttackHandler.HandleCollisionEnter -= HandleTriggerEnter;
        strongAttackHandler.HandleCollisionExit -= HandleTriggerExit;
        weakAttackHandler.HandleCollisionExit -= HandleTriggerExit;
    }

    public void DisableWeaponCollision()
    {
        strongAttackCollision.enabled = false;
        weakAttackCollision.enabled = false;
    }

    public void EnableAttackCollision(SwordAttackType attackType)
    {
        switch (attackType)
        {
            case SwordAttackType.Weak:
                weakAttackCollision.enabled = true;
                break;
            case SwordAttackType.Strong:
                strongAttackCollision.enabled = true;
                break;
        }
    }

    private void HandleTriggerEnter(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Entity entity)) 
            return;
        
        Debug.Log("Sword collide");

        OnEntityEnterCollision?.Invoke(entity);
    }
    private void HandleTriggerExit(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Entity entity)) 
            return;
        
        OnEntityExitCollision?.Invoke(entity);
    }
}