using Common.Abstract_Bases.Factories;
using Systems.In_Game_Systems.Prefab_Provider;
using UnityEngine;
using Zenject;

namespace Systems.In_Game_Systems.Factory
{
    public class InGameSystemsFactory : FactoryWithInstantiatorBase, IInGameSystemsFactory
    {
        private readonly IInGameSystemsPrefabProvider _inGameSystemsPrefabProvider;

        public InGameSystemsFactory(IInstantiator instantiator, Transform defaultParentTransform,
            IInGameSystemsPrefabProvider inGameSystemsPrefabProvider) : base(instantiator, defaultParentTransform)
        {
            _inGameSystemsPrefabProvider = inGameSystemsPrefabProvider;
        }

        public void Create()
        {
            InstantiatePrefab(_inGameSystemsPrefabProvider, Vector3.zero, Quaternion.identity);
        }
    }
}