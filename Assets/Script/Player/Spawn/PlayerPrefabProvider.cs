using Common.Abstract_Bases;
using UnityEngine;

namespace Player.Spawn
{
    [CreateAssetMenu(
        menuName = ScriptableObjectsMenuDirectories.PickableItemsProvidersDirectory + "Player Prefab Provider",
        fileName = "Player Prefab Provider", order = 0)]
    public class PlayerPrefabProvider : PrefabProviderScriptableObjectBase
    {
        [SerializeField] private PlayerController _playerPrefab;
        public override GameObject Prefab => _playerPrefab.gameObject;
    }
}