using Enemies;
using Enemies.Prefab_Provider;

namespace Spawn_Markers_System.Markers.Enemy_Marker
{
    public class EnemySpawnMarker : SpawnMarkerBase<EnemyPrefabProvider, IEnemyPrefabProvider>, IEnemySpawnMarker
    {
    }
}