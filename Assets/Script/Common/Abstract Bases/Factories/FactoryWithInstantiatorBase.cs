using UnityEngine;
using Zenject;

namespace Common.Abstract_Bases.Factories
{
    public abstract class FactoryWithInstantiatorBase
    {
        private readonly IInstantiator _instantiator;
        private readonly Transform _parentTransform;

        protected FactoryWithInstantiatorBase(IInstantiator instantiator, Transform parentTransform)
        {
            _instantiator = instantiator;
            _parentTransform = parentTransform;
        }

        protected GameObject InstantiatePrefab(IPrefabProvider prefabProvider, Vector3 spawnPosition,
            Quaternion spawnRotation)
        {
            return InstantiatePrefab(prefabProvider.Prefab, spawnPosition, spawnRotation);
        }

        protected GameObject InstantiatePrefab(GameObject prefab, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            return _instantiator.InstantiatePrefab(prefab, spawnPosition, spawnRotation, _parentTransform);
        }

        protected TComponent InstantiatePrefabForComponent<TComponent>(IPrefabProvider prefabProvider,
            Vector3 spawnPosition, Quaternion spawnRotation)
        {
            return InstantiatePrefabForComponent<TComponent>(prefabProvider.Prefab, spawnPosition, spawnRotation);
        }

        protected TComponent InstantiatePrefabForComponent<TComponent>(GameObject prefab,
            Vector3 spawnPosition, Quaternion spawnRotation)
        {
            return _instantiator.InstantiatePrefabForComponent<TComponent>(prefab, spawnPosition, spawnRotation,
                _parentTransform);
        }
    }
}