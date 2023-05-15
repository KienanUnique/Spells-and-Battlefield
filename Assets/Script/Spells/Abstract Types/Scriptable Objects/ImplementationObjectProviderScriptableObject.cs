using UnityEngine;

namespace Spells.Abstract_Types.Scriptable_Objects
{
    public abstract class ImplementationObjectProviderScriptableObject<TImplementation> : ScriptableObject,
        IImplementationObjectProvider<TImplementation>
    {
        public abstract TImplementation GetImplementationObject();
    }
}