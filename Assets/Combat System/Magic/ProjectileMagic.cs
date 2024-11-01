using UnityEngine;
using Zenject;

public class ProjectileMagic : Magic
{
    private ICharacter character;
    private PlayerWeaponController weaponController;

    [Inject]
    private void Construct(ICharacter character, PlayerWeaponController weaponController)
    {
        this.character = character;
        this.weaponController = weaponController;
    }
    
    public override void CastSpell(int spellIndex)
    {
        Debug.Log(Spells[spellIndex].spellName);

        InstantiateSpellProjectile(Spells[spellIndex].projectilePrefab);
    }

    private void InstantiateSpellProjectile(ProjectileBase spellProjectile)
    {
        Vector3 cursorPosition = CoordinateManager.GetCursorPositionInWorldPoint();
        Vector2 projectileDir = cursorPosition - character.transform.position;

        Vector3 castPosition = character.transform.position;

        if (weaponController.ChosenWeapon is Staff staff)
            castPosition = staff.GetCastComponent().magicInstantiateTransform.position;

        ProjectileBase projectile = Instantiate(spellProjectile, castPosition, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        float angle = Mathf.Atan2(projectileDir.y, projectileDir.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        rb.velocity = projectileDir.normalized * projectile.ProjectileSpeed;
    }
}
