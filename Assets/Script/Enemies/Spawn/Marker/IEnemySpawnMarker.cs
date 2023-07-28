using Common.Abstract_Bases.Spawn_Markers_System.Markers;
using Enemies.Spawn.Data_For_Spawn;

namespace Enemies.Spawn.Marker
{
    public interface IEnemySpawnMarker : ISpawnMarker
    {
        public IEnemyDataForSpawnMarker DataForSpawn { get; }
    }
}