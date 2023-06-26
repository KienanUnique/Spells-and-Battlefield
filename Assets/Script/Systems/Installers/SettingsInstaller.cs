using Settings;
using Settings.Enemy;
using Settings.UI;
using Settings.UI.Spell_Panel;
using UnityEngine;
using Zenject;

namespace Systems.Installers
{
    [CreateAssetMenu(fileName = "Settings Installer",
        menuName = ScriptableObjectsMenuDirectories.InstallersDirectory + "Settings Installer")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        [SerializeField] private GroundLayerMaskSetting _groundLayerMaskSetting;
        [SerializeField] private PickableItemsSettings _pickableItemsSettings;
        [SerializeField] private SpellTypesSetting _spellTypesSetting;
        [SerializeField] private PlayerSettings _playerSettings;

        [Header("Enemies")] [SerializeField] private GeneralEnemySettings _generalEnemySettings;
        [SerializeField] private KnightSettings _knightSettings;

        [Header("UI")] [SerializeField] private GeneralUIAnimationSettings _generalUIAnimationSettings;
        [SerializeField] private SpellPanelSettings _spellPanelSettings;

        public override void InstallBindings()
        {
            BindGeneralSettings();
            BindEnemiesSettings();
            BindUISettings();
            BindPlayerSettings();
        }

        private void BindPlayerSettings()
        {
            Container
                .Bind<PlayerSettings>()
                .FromInstance(_playerSettings)
                .AsSingle();
        }

        private void BindEnemiesSettings()
        {
            Container
                .Bind<GeneralEnemySettings>()
                .FromInstance(_generalEnemySettings)
                .AsSingle();

            Container
                .Bind<KnightSettings>()
                .FromInstance(_knightSettings)
                .AsSingle();
        }

        private void BindUISettings()
        {
            Container
                .Bind<GeneralUIAnimationSettings>()
                .FromInstance(_generalUIAnimationSettings)
                .AsSingle();

            Container
                .Bind<SpellPanelSettings>()
                .FromInstance(_spellPanelSettings)
                .AsSingle();
        }


        private void BindGeneralSettings()
        {
            Container
                .Bind<GroundLayerMaskSetting>()
                .FromInstance(_groundLayerMaskSetting)
                .AsSingle();

            Container
                .Bind<PickableItemsSettings>()
                .FromInstance(_pickableItemsSettings)
                .AsSingle();

            Container
                .Bind<SpellTypesSetting>()
                .FromInstance(_spellTypesSetting)
                .AsSingle();
        }
    }
}