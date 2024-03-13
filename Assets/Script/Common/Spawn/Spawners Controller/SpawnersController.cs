using System.Collections.Generic;
using System.Linq;
using Common.Abstract_Bases.Initializable_MonoBehaviour;
using Common.Spawn.Spawn_Trigger;

namespace Common.Spawn.Spawners_Controller
{
    public class SpawnersController : InitializableMonoBehaviourBase, IInitializableSpawnersController
    {
        private ICollection<ISpawnTrigger> _spawnTriggers;
        private ICollection<ISpawner> _spawners;
        private bool _wasSpawned;

        public void Initialize(ICollection<ISpawnTrigger> spawnTriggers, ICollection<ISpawner> spawners)
        {
            _spawnTriggers = spawnTriggers;
            _spawners = spawners;
            SetInitializedStatus();
            if (_spawnTriggers.Any(trigger => trigger.IsSpawnRequired))
            {
                TrySpawn();
            }
        }

        protected override void SubscribeOnEvents()
        {
            foreach (var spawnTrigger in _spawnTriggers)
            {
                spawnTrigger.SpawnRequired += TrySpawn;
            }
        }

        protected override void UnsubscribeFromEvents()
        {
            UnsubscribeFromTriggers();
        }

        private void UnsubscribeFromTriggers()
        {
            foreach (var spawnTrigger in _spawnTriggers)
            {
                spawnTrigger.SpawnRequired -= TrySpawn;
            }
        }

        private void TrySpawn()
        {
            if (_wasSpawned)
            {
                return;
            }

            _wasSpawned = true;
            foreach (var spawner in _spawners)
            {
                spawner.Spawn();
            }

            UnsubscribeFromTriggers();
        }
    }
}