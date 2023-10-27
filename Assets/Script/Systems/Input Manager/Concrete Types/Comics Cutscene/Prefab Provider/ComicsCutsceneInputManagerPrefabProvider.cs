using Common.Abstract_Bases;
using UnityEngine;

namespace Systems.Input_Manager.Concrete_Types.Comics_Cutscene.Prefab_Provider
{
    [CreateAssetMenu(fileName = "Comics Cutscene Input Manager Prefab Provider",
        menuName = ScriptableObjectsMenuDirectories.SystemsPrefabProvidersDirectory +
                   "Comics Cutscene Input Manager Prefab Provider", order = 0)]
    public class ComicsCutsceneInputManagerPrefabProvider : PrefabProviderScriptableObjectBase
    {
        [SerializeField] private ComicsCutsceneInputManager _prefab;
        public override GameObject Prefab => _prefab.gameObject;
    }
}