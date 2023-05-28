using Common.Abstract_Bases.Spawn_Markers_System.Markers;
using Enemies.Prefab_Provider;

namespace Enemies.Marker
{
    public class EnemySpawnMarker : SpawnMarkerBase<EnemyPrefabProvider, IEnemyPrefabProvider>, IEnemySpawnMarker
    {
    }
}