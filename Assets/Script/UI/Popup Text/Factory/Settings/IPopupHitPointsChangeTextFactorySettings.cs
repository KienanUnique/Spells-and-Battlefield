using UI.Popup_Text.Prefab_Provider;

namespace UI.Popup_Text.Factory.Settings
{
    public interface IPopupHitPointsChangeTextFactorySettings
    {
        PopupTextPrefabProvider HealTextPrefabProvider { get; }
        PopupTextPrefabProvider DamageTextPrefabProvider { get; }
        int HealTextObjectPooledCount { get; }
        int DamageTextObjectPooledCount { get; }
    }
}