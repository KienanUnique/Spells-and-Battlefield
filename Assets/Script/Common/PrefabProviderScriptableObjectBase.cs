using Spells.Implementations_Interfaces.Implementations;
using UnityEngine;

namespace Common
{
    public abstract class PrefabProviderScriptableObjectBase : ScriptableObject, ISpellPrefabProvider
    {
        public abstract GameObject Prefab { get; }
    }
}