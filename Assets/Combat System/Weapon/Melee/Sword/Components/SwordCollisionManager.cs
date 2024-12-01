using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class SwordCollisionManager : MonoBehaviour
{
    private ICharacter weaponOwner;
    
    [SerializeField] private PolygonCollider2D strongAttackCollision;
    [SerializeField] private SwordCollisionHandler strongAttackHandler;
    [SerializeField] private PolygonCollider2D weakAttackCollision;
    [SerializeField] private SwordCollisionHandler weakAttackHandler;
    
    public event UnityAction<ICharacter> OnEntityEnterCollision;
    public event UnityAction<ICharacter> OnEntityExitCollision;

    [Inject]
    private void Construct(ICharacter character)
    {
        weaponOwner = character;
    }
    

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
        
        OnEntityEnterCollision?.Invoke(entity);
        
        // var forceDir = entity.transform.position - weaponOwner.transform.position;
        // entity.rigidBody.AddForce(forceDir * 2f, ForceMode2D.Impulse);
    }
    private void HandleTriggerExit(Collider2D collision)
    {
        if (!collision.TryGetComponent(out ICharacter entity)) 
            return;
        
        OnEntityExitCollision?.Invoke(entity);
    }
}