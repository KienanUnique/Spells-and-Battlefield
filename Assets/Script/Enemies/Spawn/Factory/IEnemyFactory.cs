using System.Collections.Generic;
using Enemies.Setup;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Trigger;
using UnityEngine;

namespace Enemies.Spawn.Factory
{
    public interface IEnemyFactory
    {
        public void Create(IEnemyDataForSpawnMarker dataForSpawnMarker, List<IEnemyTargetTrigger> enemyTargetTriggers, Vector3 spawnPosition, Quaternion spawnRotation);
    }
}