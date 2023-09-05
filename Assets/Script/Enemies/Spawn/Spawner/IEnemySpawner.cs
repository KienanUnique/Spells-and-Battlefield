using System.Collections.Generic;
using Enemies.Trigger;

namespace Enemies.Spawn.Spawner
{
    public interface IEnemySpawner
    {
        void Spawn(List<IEnemyTargetTrigger> targetTriggers);
    }
}