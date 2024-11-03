using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowVisual : WeaponVisualBase
{
    private Bow bow;
    
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        base.Awake();
        
        bow = GetComponentInParent<Bow>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
