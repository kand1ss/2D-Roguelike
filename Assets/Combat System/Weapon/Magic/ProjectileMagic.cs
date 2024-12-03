using UnityEngine;
using Zenject;

public class ProjectileMagic : Magic
{
    private ICharacter character;
    private IWeaponController weaponController;

    [SerializeField] private Player player;

    [Inject]
    private void Construct(ICharacter character, IWeaponController weaponController)
    {
        this.character = character;
        this.weaponController = weaponController;
    }
    
    public override void CastSpell(int spellIndex)
    {
        InstantiateSpellProjectile(Spells[spellIndex].projectilePrefab);
    }

    private void InstantiateSpellProjectile(ProjectileBase spellProjectile)
    {
        Vector3 castPosition = character.transform.position;
        
        if (weaponController.ChosenWeapon is Staff staff)
            castPosition = staff.GetCastComponent().magicInstantiateTransform.position;

        Vector3 cursorPosition;
        
        if(character is Player)
            cursorPosition = CoordinateManager.GetCursorPositionInWorldPoint();
        else
            cursorPosition = player.transform.position;

        Vector2 projectileDir = cursorPosition - castPosition;


        ProjectileBase projectile = Instantiate(spellProjectile, castPosition, Quaternion.identity);
        projectile.ProjectileSender = character;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        float angle = Mathf.Atan2(projectileDir.y, projectileDir.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        rb.velocity = projectileDir.normalized * projectile.ProjectileSpeed;
    }
}
