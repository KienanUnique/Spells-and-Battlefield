using Common.Abstract_Bases;
using UnityEngine;

namespace Systems.Input_Manager.Concrete_Types.In_Game.Prefab_Provider
{
    [CreateAssetMenu(fileName = "In Game Input Manager Prefab Provider",
        menuName = ScriptableObjectsMenuDirectories.SystemsPrefabProvidersDirectory +
                   "In Game Input Manager Prefab Provider", order = 0)]
    public class InGameInputManagerPrefabProvider : PrefabProviderScriptableObjectBase
    {
        [SerializeField] private InGameInputManager _prefab;
        public override GameObject Prefab => _prefab.gameObject;
    }
}