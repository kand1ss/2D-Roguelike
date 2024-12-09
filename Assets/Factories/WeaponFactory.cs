using UnityEngine;
using Zenject;

public class WeaponFactory : PlaceholderFactory<WeaponBase, Transform, WeaponBase>
{
    private readonly DiContainer _container;
    
    public WeaponFactory(DiContainer container)
    {
        _container = container;
    }

    public override WeaponBase Create(WeaponBase prefab, Transform parent)
    {
        var weaponInstance = _container.InstantiatePrefabForComponent<WeaponBase>(prefab);
        
        weaponInstance.transform.SetParent(parent);
        weaponInstance.transform.localPosition = new Vector3(0.452f, -0.093f, 0);
        weaponInstance.transform.localRotation = Quaternion.Euler(0, 0, 132.322f);
        
        if(weaponInstance != null)
            Debug.Log("Weapon Instantiated");
        
        return weaponInstance;
    }
}