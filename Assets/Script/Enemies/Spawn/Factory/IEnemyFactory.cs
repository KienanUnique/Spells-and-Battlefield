using System.Collections.Generic;
using Enemies.Setup;
using Enemies.Trigger;
using UnityEngine;

namespace Enemies.Spawn.Factory
{
    public interface IEnemyFactory
    {
        public void Create(IEnemyPrefabProvider enemyPrefabProvider, IEnemySettings settings,
            List<IEnemyTargetTrigger> enemyTargetTriggers, Vector3 spawnPosition, Quaternion spawnRotation);
    }
}