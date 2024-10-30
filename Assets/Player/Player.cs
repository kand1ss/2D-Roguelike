using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    
    private PlayerWeaponController weaponController;
    public PlayerWeaponController WeaponController => weaponController;
    
    [SerializeField] private float moveSpeed = 6f;

    
    private void Awake()
    {
        Instance = this;
        
        weaponController = GetComponentInChildren<PlayerWeaponController>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 movementVector = InputService.Instance.GetMovementVector();

        transform.position += movementVector * (moveSpeed * Time.deltaTime);
    }
}
