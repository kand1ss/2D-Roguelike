using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, ICharacterEffectSusceptible, IPotionUser
{
    private IInputProvider inputProvider;
    
    public PlayerState PlayerState { get; private set; } = PlayerState.Idle;

    public CharacterPotionManager PotionManager { get; private set; }
    public CharacterEffectManager EffectManager { get; private set; }
    [field: SerializeField] public CharacterResists Resists { get; private set; }
    [field: SerializeField] public CharacterStatsManager StatsManager { get; private set; }
    [field: SerializeField] public CharacterSkills Skills { get; private set; }

    public PlayerWeaponController WeaponController { get; private set; }
    public Rigidbody2D rigidBody { get; private set; }

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
        WeaponController = GetComponentInChildren<PlayerWeaponController>();
    }

    private void Start()
    {
        PotionManager.SetPotion(new HealPotion(this, 3, 5f));
        inputProvider.ButtonsController.ActionInput.OnPotionUsed += PotionManager.UsePotion;
    }

    private void Update()
    {
        HandleMovement();
        UpdateState();
        EffectManager.UpdateEffects();
    }

    private void HandleMovement()
    {
        Vector3 movementVector = inputProvider.GetMovementVector();
        var moveSpeed = StatsManager.WalkingMoveSpeed;
        
        if(Input.GetKey(KeyCode.LeftShift))
            moveSpeed *= 1.7f;
        
        Vector2 movement = transform.position + movementVector * (moveSpeed * Time.deltaTime);
        
        rigidBody.MovePosition(movement);
    }

    private void UpdateState()
    {
        Vector3 movementVector = inputProvider.GetMovementVector();

        PlayerState = movementVector != Vector3.zero ? PlayerState.Run : PlayerState.Idle;
    }
}