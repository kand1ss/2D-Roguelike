using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PushCombo : Combo
{
    private float pushPower = 8f;

    public PushCombo(ICharacter character) : base(character)
    {
    }

    protected override void InitCombo()
    {
        ExpectedAttackSequence.Add(SwordAttackType.Weak);
        ExpectedAttackSequence.Add(SwordAttackType.Weak);
        ExpectedAttackSequence.Add(SwordAttackType.Strong);
    }

    public override void UseCombo(IEnumerable<ICharacter> entities)
    {
        if(character == null)
            Debug.Log("Character is null");

        foreach (var entity in entities)
        {
            if (entity == null)
            {
                Debug.Log("Entity is null");
                break;
            }
            Vector3 cursorPosition = CoordinateManager.GetCursorPositionInWorldPoint();
            Vector2 pushDirection = (cursorPosition - character.transform.position).normalized;

            entity.rigidBody.AddForce(pushDirection * pushPower, ForceMode2D.Impulse);
        }
    }
}