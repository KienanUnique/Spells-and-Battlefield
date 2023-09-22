using System;

namespace Common.Spawn.Spawn_Trigger
{
    public interface ISpawnTrigger
    {
        public event Action SpawnRequired;
        public bool IsSpawnRequired { get; }
    }
}