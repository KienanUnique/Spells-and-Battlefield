using System.Collections.Generic;
using Common.Abstract_Bases.Factories;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Trigger;
using Interfaces;
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

        public IEnemy Create(IEnemyDataForSpawnMarker dataForSpawnMarker, List<IEnemyTargetTrigger> enemyTargetTriggers,
            IPositionDataForInstantiation positionDataForInstantiation)
        {
            return Create(dataForSpawnMarker, enemyTargetTriggers, positionDataForInstantiation.SpawnPosition,
                positionDataForInstantiation.SpawnRotation);
        }

        public IEnemy Create(IEnemyDataForSpawnMarker dataForSpawnMarker,
            List<IEnemyTargetTrigger> enemyTargetTriggers, Vector3 spawnPosition,
            Quaternion spawnRotation)
        {
            var enemySetup =
                InstantiatePrefabForComponent<IEnemySetup>(dataForSpawnMarker.PrefabProvider, spawnPosition,
                    spawnRotation);
            enemySetup.SetDataForInitialization(dataForSpawnMarker.Settings, dataForSpawnMarker.ItemToDrop,
                enemyTargetTriggers);
            return enemySetup.InitializedEnemy;
        }
    }
}