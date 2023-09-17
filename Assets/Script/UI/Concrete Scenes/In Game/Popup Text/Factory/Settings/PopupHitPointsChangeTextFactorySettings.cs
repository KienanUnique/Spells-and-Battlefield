using UI.Concrete_Scenes.In_Game.Popup_Text.Prefab_Provider;
using UnityEngine;

namespace UI.Concrete_Scenes.In_Game.Popup_Text.Factory.Settings
{
    [CreateAssetMenu(fileName = "Popup Hit Points Change Text Factory Settings",
        menuName = ScriptableObjectsMenuDirectories.SystemsSettingsDirectory +
                   "Popup Hit Points Change Text Factory Settings", order = 0)]
    public class PopupHitPointsChangeTextFactorySettings : ScriptableObject, IPopupHitPointsChangeTextFactorySettings
    {
        [Min(1)] [SerializeField] private int _damageTextObjectPooledCount = 15;
        [Min(1)] [SerializeField] private int _healTextObjectPooledCount = 5;
        [SerializeField] private PopupTextPrefabProvider _damageTextPrefabProvider;
        [SerializeField] private PopupTextPrefabProvider _healTextPrefabProvider;

        public PopupTextPrefabProvider HealTextPrefabProvider => _healTextPrefabProvider;
        public PopupTextPrefabProvider DamageTextPrefabProvider => _damageTextPrefabProvider;
        public int HealTextObjectPooledCount => _healTextObjectPooledCount;
        public int DamageTextObjectPooledCount => _damageTextObjectPooledCount;
    }
}