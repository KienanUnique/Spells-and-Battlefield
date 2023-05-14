using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Spells.Abstract_Types.Scriptable_Objects.Parts
{
    public abstract class SpellGameObjectProviderScriptableObject : ScriptableObject, ISpellGameObjectProvider
    {
        public abstract GameObject Prefab { get; }
    }
}