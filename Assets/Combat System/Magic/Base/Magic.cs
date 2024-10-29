using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Magic : MonoBehaviour
{
    [SerializeField] private List<SpellConfig> spellConfigs = new List<SpellConfig>();
    public List<SpellConfig> Spells { get => spellConfigs; protected set => spellConfigs = value; }

    public abstract void CastSpell(int spellIndex);
}
