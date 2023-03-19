using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Abstract_Types.Scriptable_Objects
{
    public abstract class SpellTargetSelecterScriptableObject : ScriptableObject
    {
        public abstract ISpellTargetSelector GetImplementationObject();
    }
}