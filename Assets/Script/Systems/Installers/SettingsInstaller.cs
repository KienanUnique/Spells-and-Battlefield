using Settings;
using Settings.Enemies;
using Settings.Puzzles.Mechanisms;
using Settings.Puzzles.Triggers;
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

        [Header("UI")] [SerializeField] private GeneralUIAnimationSettings _generalUIAnimationSettings;
        [SerializeField] private SpellPanelSettings _spellPanelSettings;

        [Header("Puzzles")] [SerializeField] private PlateSettings _plateSettings;
        [SerializeField] private MovingPlatformsSettings _movingPlatformSettings;
        [SerializeField] private ExtendableObjectsSettings _extendableObjectsSettings;

        public override void InstallBindings()
        {
            BindGeneralSettings();
            BindEnemiesSettings();
            BindUISettings();
            BindPlayerSettings();
            BindPuzzlesSettings();
        }

        private void BindPuzzlesSettings()
        {
            Container
                .Bind<PlateSettings>()
                .FromInstance(_plateSettings)
                .AsSingle();

            Container
                .Bind<MovingPlatformsSettings>()
                .FromInstance(_movingPlatformSettings)
                .AsSingle();
            
            Container
                .Bind<ExtendableObjectsSettings>()
                .FromInstance(_extendableObjectsSettings)
                .AsSingle();
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