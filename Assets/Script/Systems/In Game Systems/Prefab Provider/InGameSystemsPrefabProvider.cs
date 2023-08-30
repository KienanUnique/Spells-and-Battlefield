using Common.Abstract_Bases;
using Systems.In_Game_Systems.Game_Controller;
using UnityEngine;

namespace Systems.In_Game_Systems.Prefab_Provider
{
    [CreateAssetMenu(fileName = "In Game Systems Prefab Provider",
        menuName = ScriptableObjectsMenuDirectories.SystemsPrefabProvidersDirectory + "In Game Systems Prefab Provider",
        order = 0)]
    public class InGameSystemsPrefabProvider : PrefabProviderScriptableObjectBase, IInGameSystemsPrefabProvider
    {
        [SerializeField] private GameController _prefab;
        public override GameObject Prefab => _prefab.gameObject;
    }
}