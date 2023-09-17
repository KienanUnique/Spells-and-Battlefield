using UI.Concrete_Scenes.In_Game.Popup_Text.Prefab_Provider;

namespace UI.Concrete_Scenes.In_Game.Popup_Text.Factory.Settings
{
    public interface IPopupHitPointsChangeTextFactorySettings
    {
        PopupTextPrefabProvider HealTextPrefabProvider { get; }
        PopupTextPrefabProvider DamageTextPrefabProvider { get; }
        int HealTextObjectPooledCount { get; }
        int DamageTextObjectPooledCount { get; }
    }
}