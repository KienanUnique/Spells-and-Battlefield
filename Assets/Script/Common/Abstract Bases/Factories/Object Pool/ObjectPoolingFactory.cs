using UnityEngine;
using Zenject;

namespace Common.Abstract_Bases.Factories.Object_Pool
{
    public class ObjectPoolingFactory<TItemPrefabProvider> : FactoryWithInstantiatorBase
        where TItemPrefabProvider : IPrefabProvider
    {
        
        public ObjectPoolingFactory(IInstantiator instantiator, Transform parentTransform) : base(instantiator,
            parentTransform)
        {
        }
    }
}