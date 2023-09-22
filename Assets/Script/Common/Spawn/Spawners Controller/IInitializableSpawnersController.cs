using System.Collections.Generic;
using Common.Spawn.Spawn_Trigger;

namespace Common.Spawn.Spawners_Controller
{
    public interface IInitializableSpawnersController
    {
        void Initialize(ICollection<ISpawnTrigger> spawnTriggers, ICollection<ISpawner> spawners);
    }
}