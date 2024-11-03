using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class BowShootManager : ChargeHandler
{
    private ICharacter weaponOwner;
    private IWeaponController weaponController;

    private Bow bow;
    [SerializeField] private Transform arrowInstantiate;
    
    [SerializeField] private float shootCooldownRate;
    private float cooldownTime = 0f;
    
    [SerializeField] private float shootHoldTime;
    [SerializeField] private float arrowSpeed;

    [Inject]
    private void Construct(ICharacter character, IWeaponController weaponController)
    {
        weaponOwner = character;
        this.weaponController = weaponController;
    }

    public void InitializeComponent()
    {
        bow = GetComponent<Bow>();
        OnChargeAttackCompleted += Shoot;
    }

    public void FinalizeComponent()
    {
        bow = null;
        OnChargeAttackCompleted -= Shoot;
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
        
        InstantiateArrow(bow.GetArrowManager().ChoosenArrow);
        cooldownTime = Time.time;
        
        CooldownBar.Instance.ShowProgressBar(shootCooldownRate);
    }

    public void StartChargingShoot()
    {
        if (Time.time < cooldownTime + shootCooldownRate)
            return;
        
        StartCharging(shootHoldTime);
    }

    private void InstantiateArrow(Arrow arrow)
    {
        Vector3 cursorPosition = CoordinateManager.GetCursorPositionInWorldPoint();
        Vector2 projectileDir = cursorPosition - arrowInstantiate.position;

        Vector3 shootPosition = arrowInstantiate.position;

        Arrow projectile = Instantiate(arrow, shootPosition, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        float angle = Mathf.Atan2(projectileDir.y, projectileDir.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        rb.velocity = projectileDir.normalized * arrowSpeed;
    }
}