using Common.Abstract_Bases;
using Enemies.Controller;
using UnityEngine;

namespace Enemies.Prefab_Provider
{
    [CreateAssetMenu(fileName = "Enemy Prefab Provider",
        menuName = ScriptableObjectsMenuDirectories.EnemiesDirectory + "Enemy Prefab Provider", order = 0)]
    public class EnemyPrefabProvider : PrefabProviderScriptableObjectBase, IEnemyPrefabProvider
    {
        [SerializeField] private EnemyController _knightPrefab;
        public override GameObject Prefab => _knightPrefab.gameObject;
    }
}