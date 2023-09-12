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
        public EnemyFactory(IInstantiator instantiator, Transform parentTransform) : base(instantiator, parentTransform)
        {
        }

        public IEnemy Create(IEnemyDataForSpawning dataForSpawning, List<IEnemyTargetTrigger> enemyTargetTriggers,
            IPositionDataForInstantiation positionDataForInstantiation)
        {
            return Create(dataForSpawning, enemyTargetTriggers, positionDataForInstantiation.SpawnPosition,
                positionDataForInstantiation.SpawnRotation);
        }

        public IEnemy Create(IEnemyDataForSpawning dataForSpawning, List<IEnemyTargetTrigger> enemyTargetTriggers,
            Vector3 spawnPosition, Quaternion spawnRotation)
        {
            var enemySetup =
                InstantiatePrefabForComponent<IEnemySetup>(dataForSpawning.PrefabProvider, spawnPosition,
                    spawnRotation);
            enemySetup.SetDataForInitialization(dataForSpawning.Settings, dataForSpawning.Faction,
                enemyTargetTriggers);
            return enemySetup.InitializedEnemy;
        }
    }
}