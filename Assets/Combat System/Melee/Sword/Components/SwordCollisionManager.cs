using UnityEngine;
using UnityEngine.Events;

public class SwordCollisionManager : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D strongAttackCollision;
    [SerializeField] private SwordCollisionHandler strongAttackHandler;
    [SerializeField] private PolygonCollider2D weakAttackCollision;
    [SerializeField] private SwordCollisionHandler weakAttackHandler;
    
    public event UnityAction<ICharacter> OnEntityEnterCollision;
    public event UnityAction<ICharacter> OnEntityExitCollision;
    

    private void Awake()
    {
        DisableWeaponCollision();
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
        if (!collision.TryGetComponent(out ICharacter entity)) 
            return;
        
        CinemachineShake.Instance.Shake(0.2f, 1.1f);
        
        Debug.Log("Sword collide");

        OnEntityEnterCollision?.Invoke(entity);
    }
    private void HandleTriggerExit(Collider2D collision)
    {
        if (!collision.TryGetComponent(out ICharacter entity)) 
            return;
        
        OnEntityExitCollision?.Invoke(entity);
    }
}