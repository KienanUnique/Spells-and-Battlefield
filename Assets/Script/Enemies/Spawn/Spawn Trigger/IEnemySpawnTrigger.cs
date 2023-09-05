using System;

namespace Enemies.Spawn.Spawn_Trigger
{
    public interface IEnemySpawnTrigger
    {
        public event Action SpawnRequired;
        public bool IsSpawnRequired { get; }
    }
}