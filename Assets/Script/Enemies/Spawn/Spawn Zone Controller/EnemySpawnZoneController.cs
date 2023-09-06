using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Enemies.Spawn.Spawn_Trigger;
using Enemies.Spawn.Spawn_Zone_Controller.Setup;
using Enemies.Spawn.Spawner;
using Enemies.Trigger;

namespace Enemies.Spawn.Spawn_Zone_Controller
{
    public class EnemySpawnZoneController : InitializableMonoBehaviourBase, IInitializableEnemySpawnZoneController
    {
        private List<IEnemySpawner> _spawners;
        private List<IEnemySpawnTrigger> _spawnTriggers;
        private List<IEnemyTargetTrigger> _triggerList;
        private bool _wasSpawned;

        public void Initialize(List<IEnemyTargetTrigger> triggerList, List<IEnemySpawner> spawners,
            List<IEnemySpawnTrigger> spawnTriggers)
        {
            _triggerList = triggerList;
            _spawners = spawners;
            _spawnTriggers = spawnTriggers;
            SetInitializedStatus();
            if (!_wasSpawned && _spawnTriggers.Any(trigger => trigger.IsSpawnRequired))
            {
                Spawn();
            }
        }

        protected override void SubscribeOnEvents()
        {
            if (_wasSpawned)
            {
                return;
            }

            foreach (IEnemySpawnTrigger enemySpawnTrigger in _spawnTriggers)
            {
                enemySpawnTrigger.SpawnRequired += Spawn;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            UnsubscribeFromTriggers();
        }

        private void UnsubscribeFromTriggers()
        {
            foreach (IEnemySpawnTrigger enemySpawnTrigger in _spawnTriggers)
            {
                enemySpawnTrigger.SpawnRequired -= Spawn;
            }
        }

        private void Spawn()
        {
            if (_wasSpawned)
            {
                return;
            }

            _wasSpawned = true;
            UnsubscribeFromTriggers();
            foreach (IEnemySpawner enemySpawner in _spawners)
            {
                enemySpawner.Spawn(_triggerList);
            }
        }
    }
}