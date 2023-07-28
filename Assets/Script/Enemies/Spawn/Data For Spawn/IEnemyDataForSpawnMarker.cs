using Enemies.Setup;

namespace Enemies.Spawn.Data_For_Spawn
{
    public interface IEnemyDataForSpawnMarker
    {
        public IEnemySettings Settings { get; }
        public IEnemyPrefabProvider PrefabProvider { get; }
    }
}