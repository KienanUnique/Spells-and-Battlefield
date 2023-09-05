using System.Collections.Generic;
using Enemies.Spawn.Spawner;

namespace Puzzles.Mechanisms_Triggers.Concrete_Types.Spawned_Enemy_Death
{
    public interface IInitializableSpawnedEnemiesDeathMechanismTrigger
    {
        void Initialize(List<IEnemyDeathTrigger> triggers);
    }
}