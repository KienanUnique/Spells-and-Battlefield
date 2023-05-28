using System.Collections.Generic;
using Enemies.Trigger;
using UnityEngine;

namespace Enemies.Factory
{
    public interface IEnemyFactory
    {
        void Create(IEnemyPrefabProvider enemyPrefabProvider, List<Trigger.IEnemyTargetTrigger> enemyTargetTriggers,
            Vector3 spawnPosition, Quaternion spawnRotation);
    }
}