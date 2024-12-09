using System.Collections;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, ICharacterEffectSusceptible, IPotionUser, IHasWeapon
{
    private IInputProvider inputProvider;
    
    public PlayerState PlayerState { get; private set; } = PlayerState.Idle;
    
    public CharacterPotionManager PotionManager { get; private set; }
    public CharacterEffectManager EffectManager { get; private set; }
    [field: SerializeField] public CharacterResists Resists { get; private set; }
    [field: SerializeField] public CharacterStatsManager StatsManager { get; private set; }
    [field: SerializeField] public CharacterSkills Skills { get; private set; }

    public PlayerWeaponController PlayerWeaponController { get; private set; }
    public WeaponControllerBase WeaponController => PlayerWeaponController;
    
    public Rigidbody2D rigidBody { get; private set; }

    private bool isCanMoving = true;

    [Inject]
    private void Construct(IInputProvider input)
    {
        inputProvider = input;
    }
    
    private void Awake()
    {
        EffectManager = new CharacterEffectManager(Resists.ownerEffectResistances);
        PotionManager = new CharacterPotionManager(this);
        
        rigidBody = GetComponent<Rigidbody2D>();
        PlayerWeaponController = GetComponentInChildren<PlayerWeaponController>();
    }

    private void Start()
    {
        inputProvider.ButtonsController.ActionInput.OnPotionUsed += PotionManager.UsePotion;
    }

    private void Update()
    {
        HandleMovement();
        UpdateState();
        CheckInteractableObjects();
        EffectManager.UpdateEffects();
        
        if(Input.GetKey(KeyCode.F1))
            PotionManager.SetPotion(new HealPotion(this, 9, 5f));
        if(Input.GetKey(KeyCode.F2))
            PotionManager.SetPotion(new PhysicalSkillPotion(this, 5, 5f));
    }

    private void CheckInteractableObjects()
    {
        if (!Input.GetKeyDown(KeyCode.E))
            return;
        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Ray2D ray = new Ray2D(mousePosition, Vector2.zero); 
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 2f);
        
        if (hit.collider != null)
        {
            var distance = Vector2.Distance(transform.position, hit.collider.transform.position);
            if (distance < 2f)
            {
                IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                if (interactable != null)
                    interactable.Interact(this);
            }
        }
    }

    private void HandleMovement()
    {
        if (!isCanMoving)
            return;
        
        Vector3 movementVector = inputProvider.GetMovementVector();
        var moveSpeed = StatsManager.WalkingMoveSpeed;
        
        if(Input.GetKey(KeyCode.LeftShift))
            moveSpeed *= 1.5f;
        
        Vector2 movement = transform.position + movementVector * (moveSpeed * Time.deltaTime);
        
        rigidBody.MovePosition(movement);
    }

    public void DisableMovement(float duration)
    {
        StartCoroutine(DisableMovementRoutine(duration));
    }

    private IEnumerator DisableMovementRoutine(float duration)
    {
        isCanMoving = false;
        
        yield return new WaitForSeconds(duration);
        
        isCanMoving = true;
    }

    private void UpdateState()
    {
        Vector3 movementVector = inputProvider.GetMovementVector();

        PlayerState = movementVector != Vector3.zero ? PlayerState.Run : PlayerState.Idle;
    }
}