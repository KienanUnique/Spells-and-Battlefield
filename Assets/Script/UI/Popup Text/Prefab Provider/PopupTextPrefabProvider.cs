using Common.Abstract_Bases;
using UnityEngine;

namespace UI.Popup_Text.Prefab_Provider
{
    [CreateAssetMenu(
        menuName = ScriptableObjectsMenuDirectories.PopupTextProvidersDirectory + "Popup Text Prefab Provider",
        fileName = "Popup Text Prefab Provider", order = 0)]
    public class PopupTextPrefabProvider : PrefabProviderScriptableObjectBase, IPopupTextPrefabProvider
    {
        [SerializeField] private PopupTextController _prefab;
        public override GameObject Prefab => _prefab.gameObject;
    }
}