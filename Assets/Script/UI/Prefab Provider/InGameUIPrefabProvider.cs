using Common.Abstract_Bases;
using UI.Managers.Concrete_Types.In_Game;
using UnityEngine;

namespace UI.Prefab_Provider
{
    [CreateAssetMenu(fileName = "In Game UI Prefab Provider",
        menuName = ScriptableObjectsMenuDirectories.SystemsPrefabProvidersDirectory + "In Game UI Prefab Provider",
        order = 0)]
    public class InGameUIPrefabProvider : PrefabProviderScriptableObjectBase
    {
        [SerializeField] private InGameManagerUI _prefab;
        public override GameObject Prefab => _prefab.gameObject;
    }
}