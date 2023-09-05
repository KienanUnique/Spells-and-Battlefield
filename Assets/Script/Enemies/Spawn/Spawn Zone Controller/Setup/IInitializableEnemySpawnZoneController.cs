using System.Collections.Generic;
using Enemies.Spawn.Spawn_Trigger;
using Enemies.Spawn.Spawner;
using Enemies.Trigger;

namespace Enemies.Spawn.Spawn_Zone_Controller.Setup
{
    public interface IInitializableEnemySpawnZoneController
    {
        void Initialize(List<IEnemyTargetTrigger> triggerList, List<IEnemySpawner> spawners,
            List<IEnemySpawnTrigger> spawnTriggers);
    }
}