using System;

namespace Enemies.Spawn.Spawner
{
    public interface IEnemyDeathTrigger
    {
        public event Action SpawnedEnemyDied;
        public bool IsSpawnedEnemyDied { get; }
    }
}