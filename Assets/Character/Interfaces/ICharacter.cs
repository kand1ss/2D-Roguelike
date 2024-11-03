using UnityEngine;

public interface ICharacter
{
     CharacterResists Resists { get; }
     CharacterStatsManager StatsManager { get; }
     CharacterSkills Skills { get; }
     
     Transform transform { get; }
     Rigidbody2D rigidBody { get; }
}