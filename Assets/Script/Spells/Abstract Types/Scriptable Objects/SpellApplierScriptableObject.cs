using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Abstract_Types.Scriptable_Objects
{
    public abstract class SpellApplierScriptableObject : ScriptableObject
    {
        public abstract ISpellApplier GetImplementationObject();
    }
}