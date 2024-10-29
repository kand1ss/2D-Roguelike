using System.Collections.Generic;
using UnityEngine;

public class PushCombo : Combo
{
    private float pushPower = 8f;
    protected override void InitCombo()
    {
        ExpectedAttackSequence.Add(SwordAttackType.Weak);
        ExpectedAttackSequence.Add(SwordAttackType.Weak);
        ExpectedAttackSequence.Add(SwordAttackType.Strong);
    }
    public override void UseCombo(IEnumerable<Entity> entities)
    {
        foreach (var entity in entities)
        {
            Vector3 cursorPosition = InputService.Instance.GetCursorPositionInWorldPoint();
            Vector2 pushDirection = (cursorPosition - Player.Instance.transform.position).normalized;
            Rigidbody2D entityRb = entity.GetComponent<Rigidbody2D>();
            entityRb.AddForce(pushDirection * pushPower, ForceMode2D.Impulse);
        }
    }
}