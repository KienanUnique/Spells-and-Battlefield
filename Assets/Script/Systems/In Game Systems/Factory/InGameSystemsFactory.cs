using Common.Abstract_Bases.Factories;
using Systems.In_Game_Systems.Prefab_Provider;
using UnityEngine;
using Zenject;

namespace Systems.In_Game_Systems.Factory
{
    public class InGameSystemsFactory : FactoryWithInstantiatorBase, IInGameSystemsFactory
    {
        private readonly IInGameSystemsPrefabProvider _inGameSystemsPrefabProvider;

        public InGameSystemsFactory(IInstantiator instantiator, Transform parentTransform,
            IInGameSystemsPrefabProvider inGameSystemsPrefabProvider) : base(instantiator,
            parentTransform)
        {
            _inGameSystemsPrefabProvider = inGameSystemsPrefabProvider;
        }

        public void Create()
        {
            InstantiatePrefab(_inGameSystemsPrefabProvider, Vector3.zero, Quaternion.identity);
        }
    }
}