﻿using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PushCombo : Combo
{
    private readonly ICharacter comboInitiator;
    private float pushPower = 14f;

    public PushCombo(ICharacter comboInitiator) : base()
    {
        this.comboInitiator = comboInitiator;
    }

    protected override void InitCombo()
    {
        ExpectedAttackSequence.Add(SwordAttackType.Weak);
        ExpectedAttackSequence.Add(SwordAttackType.Weak);
        ExpectedAttackSequence.Add(SwordAttackType.Strong);
    }

    public override void UseCombo(IEnumerable<ICharacter> entities)
    {
        foreach (var entity in entities)
        {
            Vector3 cursorPosition;
            
            if(comboInitiator is Player)
                cursorPosition = CoordinateManager.GetCursorPositionInWorldPoint();
            else
                cursorPosition = entity.transform.position;
            
            Vector2 pushDirection = (cursorPosition - comboInitiator.transform.position).normalized;
            
            if(entity is Player entityPlayer)
                entityPlayer.DisableMovement(1.3f);

            entity.rigidBody.AddForce(pushDirection * pushPower, ForceMode2D.Impulse);
        }
    }
}