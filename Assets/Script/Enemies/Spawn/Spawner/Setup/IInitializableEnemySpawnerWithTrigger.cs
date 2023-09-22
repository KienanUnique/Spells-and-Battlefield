using System.Collections.Generic;
using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Spawn.Factory;
using Enemies.Trigger;

namespace Enemies.Spawn.Spawner.Setup
{
    public interface IInitializableEnemySpawnerWithTrigger
    {
        public void Initialize(IEnemyFactory enemyFactory, List<IEnemyTargetTrigger> targetTriggers,
            IPositionDataForInstantiation positionDataForInstantiation, IEnemyDataForSpawning dataForSpawning);
    }
}