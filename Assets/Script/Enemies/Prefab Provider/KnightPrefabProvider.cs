using Enemies.Concrete_Types.Knight;
using UnityEngine;

namespace Enemies.Prefab_Provider
{
    [CreateAssetMenu(fileName = "Knight Prefab Provider",
        menuName = ScriptableObjectsMenuDirectories.EnemiesPrefabProvidersDirectory + "Knight Prefab Provider",
        order = 0)]
    public class KnightPrefabProvider : EnemyPrefabProvider
    {
        [SerializeField] private KnightController _knightPrefab;
        public override GameObject Prefab => _knightPrefab.gameObject;
    }
}