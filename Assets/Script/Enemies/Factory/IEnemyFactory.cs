using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Factory
{
    public interface IEnemyFactory
    {
        void Create(IEnemyPrefabProvider enemyPrefabProvider, List<IEnemyTargetTrigger> enemyTargetTriggers,
            Vector3 spawnPosition, Quaternion spawnRotation);
    }
}