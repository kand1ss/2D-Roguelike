using System.Collections.Generic;

public interface ICharacterEffectSusceptible : ICharacter
{
    CharacterEffectManager EffectManager { get; }
}