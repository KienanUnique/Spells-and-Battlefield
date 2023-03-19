using Spells.Implementations_Interfaces;
using UnityEngine;

namespace Spells.Abstract_Types.Scriptable_Objects
{
    public abstract class SpellTriggerScriptableObject : ScriptableObject
    {
        public abstract ISpellTrigger GetImplementationObject();
    }
}