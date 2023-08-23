using Enemies.Prefab_Provider;
using Enemies.Setup;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Data_For_Creating.Scriptable_Object;
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
        [SerializeField] private PickableItemScriptableObjectBase _itemToDrop;

        public IEnemySettings Settings => _settings;
        public IEnemyPrefabProvider PrefabProvider => _prefabProvider;
        public IPickableItemDataForCreating ItemToDrop => _itemToDrop;
    }
}