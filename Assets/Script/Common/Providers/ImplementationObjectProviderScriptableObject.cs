using UnityEngine;

namespace Common.Providers
{
    public abstract class ImplementationObjectProviderScriptableObject<TImplementation> : ScriptableObject,
        IImplementationObjectProvider<TImplementation>
    {
        public abstract TImplementation GetImplementationObject();
    }
}