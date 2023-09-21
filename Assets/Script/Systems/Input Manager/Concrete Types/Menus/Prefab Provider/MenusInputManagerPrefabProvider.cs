using Common.Abstract_Bases;
using UnityEngine;

namespace Systems.Input_Manager.Concrete_Types.Menus.Prefab_Provider
{
    [CreateAssetMenu(fileName = "Menus Input Manager Prefab Provider",
        menuName = ScriptableObjectsMenuDirectories.SystemsPrefabProvidersDirectory +
                   "Menus Input Manager Prefab Provider", order = 0)]
    public class MenusInputManagerPrefabProvider : PrefabProviderScriptableObjectBase
    {
        [SerializeField] private MenusInputManager _prefab;
        public override GameObject Prefab => _prefab.gameObject;
    }
}