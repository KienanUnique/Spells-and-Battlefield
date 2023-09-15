using Common.Abstract_Bases;
using Common.Capsule_Size_Information;
using Enemies.Controller;
using UnityEngine;

namespace Enemies.Prefab_Provider
{
    [CreateAssetMenu(fileName = "Enemy Prefab Provider",
        menuName = ScriptableObjectsMenuDirectories.EnemiesDirectory + "Enemy Prefab Provider", order = 0)]
    public class EnemyPrefabProvider : PrefabProviderScriptableObjectBase, IEnemyPrefabProvider
    {
        [SerializeField] private EnemyController _knightPrefab;
        [SerializeField] private CapsuleSizeInformation _capsuleSize;
        public override GameObject Prefab => _knightPrefab.gameObject;
        public ICapsuleSizeInformation SizeInformation => _capsuleSize;
    }
}