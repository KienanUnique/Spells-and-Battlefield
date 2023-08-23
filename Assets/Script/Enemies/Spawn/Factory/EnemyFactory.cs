using System.Collections.Generic;
using Common.Abstract_Bases.Factories;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Trigger;
using UnityEngine;
using Zenject;

namespace Enemies.Spawn.Factory
{
    public class EnemyFactory : FactoryWithInstantiatorBase, IEnemyFactory
    {
        public EnemyFactory(IInstantiator instantiator, Transform parentTransform) :
            base(instantiator, parentTransform)
        {
        }

        public void Create(IEnemyDataForSpawnMarker dataForSpawnMarker, List<IEnemyTargetTrigger> enemyTargetTriggers,
            Vector3 spawnPosition,
            Quaternion spawnRotation)
        {
            var enemyTriggersSettable = InstantiatePrefabForComponent<IEnemyDataForInitializationSettable>(
                dataForSpawnMarker.PrefabProvider,
                spawnPosition, spawnRotation);
            enemyTriggersSettable.SetDataForInitialization(dataForSpawnMarker.Settings, dataForSpawnMarker.ItemToDrop,
                enemyTargetTriggers);
        }
    }
}