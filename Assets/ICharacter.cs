using UnityEngine;

public interface ICharacter
{
     public float CurrentHealth { get; }

     CharacterResists Resists { get; }
     
     Transform transform { get; }
     Rigidbody2D rigidBody { get; }
     void TakeDamage(float damage);
}