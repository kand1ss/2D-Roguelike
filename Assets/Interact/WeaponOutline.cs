using UnityEngine;
using Zenject;

public class WeaponOutline : MonoBehaviour, IInteractable
{
    [SerializeField] private WeaponBase weaponPrefab;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = weaponPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public void Initiate(WeaponBase weapon)
    {
        weaponPrefab = weapon;
        spriteRenderer.sprite = weaponPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    public void Interact(ICharacter interactInitiator)
    {
        if (interactInitiator is IHasWeapon withWeapon)
        {
            Instantiate(this).Initiate(withWeapon.WeaponController.ChosenWeapon);
            withWeapon.WeaponController.SetWeapon(weaponPrefab);
        }
        
        Destroy(gameObject);
    }
}