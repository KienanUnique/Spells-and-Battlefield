using Enemies.Setup;
using Pickable_Items.Data_For_Creating;

namespace Enemies.Spawn.Data_For_Spawn
{
    public interface IEnemyDataForSpawnMarker
    {
        public IEnemySettings Settings { get; }
        public IEnemyPrefabProvider PrefabProvider { get; }
        public IPickableItemDataForCreating ItemToDrop { get; }
    }
}