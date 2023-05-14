using UnityEngine;

namespace Spells.Abstract_Types.Scriptable_Objects
{
    public abstract class ImplementationObjectProviderScriptableObject<TPartInterface> : ScriptableObject,
        IImplementationObjectProvider<TPartInterface>
    {
        public abstract TPartInterface GetImplementationObject();
    }
}