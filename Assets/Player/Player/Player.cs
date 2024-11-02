using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, ICharacter
{
    private IInputProvider inputProvider;
    public PlayerState PlayerState { get; private set; } = PlayerState.Idle;

    [field: SerializeField] public CharacterResists Resists { get; private set; }
    [field: SerializeField] public CharacterStatsManager StatsManager { get; private set; }
    [field: SerializeField] public CharacterSkills Skills { get; private set; }
    

    private PlayerWeaponController weaponController;
    public PlayerWeaponController WeaponController => weaponController;

    public Rigidbody2D rigidBody { get; private set; }
    
    [SerializeField] private float walkingMoveSpeed = 6f;

    [Inject]
    private void Construct(IInputProvider input)
    {
        inputProvider = input;
    }
    
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        weaponController = GetComponentInChildren<PlayerWeaponController>();
    }

    private void Update()
    {
        HandleMovement();
        UpdateState();
    }

    private void HandleMovement()
    {
        Vector3 movementVector = inputProvider.GetMovementVector();
        var moveSpeed = walkingMoveSpeed;
        
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