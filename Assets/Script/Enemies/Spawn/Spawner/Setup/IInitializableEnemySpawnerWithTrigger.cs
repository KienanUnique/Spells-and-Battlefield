using Common.Abstract_Bases.Factories.Position_Data_For_Instantiation;
using Enemies.Spawn.Data_For_Spawn;
using Enemies.Spawn.Factory;

namespace Enemies.Spawn.Spawner.Setup
{
    public interface IInitializableEnemySpawnerWithTrigger
    {
        void Initialize(IEnemyFactory enemyFactory, IPositionDataForInstantiation positionDataForInstantiation,
            IEnemyDataForSpawning dataForSpawning);
    }
}