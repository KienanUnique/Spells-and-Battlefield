using Common.Abstract_Bases.Spawn_Markers_System.Markers;
using Enemies.Prefab_Provider;
using Enemies.Spawn.Data_For_Spawn;
using UnityEngine;

namespace Enemies.Spawn.Marker
{
    public class EnemySpawnMarker : SpawnMarkerBase, IEnemySpawnMarker
    {
        [SerializeField] private EnemyDataForSpawnMarker _objectToSpawn;
        public IEnemyDataForSpawnMarker DataForSpawn => _objectToSpawn;
    }
}