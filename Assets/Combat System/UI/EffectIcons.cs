using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Zenject;

public class EffectIcons : MonoBehaviour
{
    private Entity containerOwner;
    
    [SerializeField] private GameObject effectIconPrefab;
    private GameObject effectIconsContainer;
    
    readonly List<GameObject> activeEffectIcons = new List<GameObject>();

    [Inject]
    private void Construct(Entity containerOwner)
    {
        this.containerOwner = containerOwner;
    }

    private void Awake()
    {
        effectIconsContainer = gameObject;
    }

    private void Start()
    {
        containerOwner.EffectManager.OnEffectAdded += AddEffectIcon;
        containerOwner.EffectManager.OnEffectRemoved += RemoveEffectIcon;
    }

    private void OnDestroy()
    {
        containerOwner.EffectManager.OnEffectAdded -= AddEffectIcon;
        containerOwner.EffectManager.OnEffectRemoved -= RemoveEffectIcon;
    }

    private void AddEffectIcon(IEffect effect)
    {
        GameObject newIconPrefab = Instantiate(effectIconPrefab, effectIconsContainer.transform);
        newIconPrefab.GetComponent<Image>().sprite = effect.EffectIcon;
        
        activeEffectIcons.Add(newIconPrefab);
    }

    private void RemoveEffectIcon(IEffect effect)
    {
        GameObject iconToRemove = activeEffectIcons
            .Find(icon => icon.GetComponent<Image>().sprite == effect.EffectIcon);
        
        if (iconToRemove)
        {
            activeEffectIcons.Remove(iconToRemove);
            Destroy(iconToRemove);
        }
    }
}
