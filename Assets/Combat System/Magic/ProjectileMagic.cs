using UnityEngine;

public class ProjectileMagic : Magic
{
    public override void CastSpell(int spellIndex)
    {
        Debug.Log(Spells[spellIndex].spellName);

        InstantiateSpellProjectile(Spells[spellIndex].projectilePrefab);
    }

    private static void InstantiateSpellProjectile(ProjectileBase spellProjectile)
    {
        Vector3 cursorPosition = InputService.Instance.GetCursorPositionInWorldPoint();
        Vector2 projectileDir = cursorPosition - Player.Instance.transform.position;

        Vector3 castPosition = Player.Instance.transform.position;

        if (Player.Instance.WeaponController.ChosenWeapon is Staff staff)
            castPosition = staff.GetCastComponent().magicInstantiateTransform.position;

        ProjectileBase projectile = Instantiate(spellProjectile, castPosition, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        float angle = Mathf.Atan2(projectileDir.y, projectileDir.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        rb.velocity = projectileDir.normalized * projectile.ProjectileSpeed;
    }
}
