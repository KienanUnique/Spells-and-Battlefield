using Enemies.Prefab_Provider;
using Enemies.Setup;
using UnityEngine;

namespace Enemies.Spawn.Data_For_Spawn
{
    [CreateAssetMenu(fileName = "Enemy Data For Spawn Marker",
        menuName = ScriptableObjectsMenuDirectories.EnemiesDirectory +
                   "Enemy Data For Spawn Marker", order = 0)]
    public class EnemyDataForSpawnMarker : ScriptableObject, IEnemyDataForSpawnMarker
    {
        [SerializeField] private EnemyPrefabProvider _prefabProvider;
        [SerializeField] private EnemySettings _settings;

        public IEnemySettings Settings => _settings;
        public IEnemyPrefabProvider PrefabProvider => _prefabProvider;
    }
}