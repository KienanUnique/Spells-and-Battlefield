﻿using System.Collections.Generic;
using Common.Abstract_Bases;
using Enemies.Setup;
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

        public void Create(IEnemyPrefabProvider enemyPrefabProvider, IEnemySettings settings,
            List<IEnemyTargetTrigger> enemyTargetTriggers, Vector3 spawnPosition, Quaternion spawnRotation)
        {
            var enemyTriggersSettable = InstantiatePrefabForComponent<IEnemyDataForInitializationSettable>(
                enemyPrefabProvider,
                spawnPosition, spawnRotation);
            enemyTriggersSettable.SetDataForInitialization(settings, enemyTargetTriggers);
        }
    }
}