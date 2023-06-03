using Common.Abstract_Bases.Spawn_Markers_System.Markers;
using Enemies.Prefab_Provider;
using UnityEngine;

namespace Enemies.Marker
{
    public class EnemySpawnMarker : SpawnMarkerBase, IEnemySpawnMarker
    {
        [SerializeField] private EnemyPrefabProvider _objectToSpawn;
        public IEnemyPrefabProvider ObjectToSpawn => _objectToSpawn;
    }
}