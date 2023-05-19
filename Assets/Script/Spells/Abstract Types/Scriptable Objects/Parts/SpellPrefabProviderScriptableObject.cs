using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Abstract_Types.Scriptable_Objects.Parts
{
    public abstract class SpellPrefabProviderScriptableObject : ScriptableObject, ISpellPrefabProvider
    {
        public abstract GameObject Prefab { get; }
    }
}