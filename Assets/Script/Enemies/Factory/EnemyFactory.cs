using System.Collections.Generic;
using Common.Abstract_Bases;
using Enemies.Trigger;
using UnityEngine;
using Zenject;

namespace Enemies.Factory
{
    public class EnemyFactory : FactoryWithInstantiatorBase, IEnemyFactory
    {
        public EnemyFactory(IInstantiator instantiator, Transform parentTransform) :
            base(instantiator, parentTransform)
        {
        }

        public void Create(IEnemyPrefabProvider enemyPrefabProvider, List<Trigger.IEnemyTargetTrigger> enemyTargetTriggers,
            Vector3 spawnPosition, Quaternion spawnRotation)
        {
            var enemyTriggersSettable = InstantiatePrefabForComponent<IEnemyTriggersSettable>(enemyPrefabProvider,
                spawnPosition, spawnRotation);
            enemyTriggersSettable.SetExternalEnemyTargetTriggers(enemyTargetTriggers);
        }
    }
}