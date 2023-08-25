using Settings;
using Settings.Enemies;
using Settings.Puzzles.Mechanisms;
using Settings.Puzzles.Triggers;
using Settings.UI;
using Settings.UI.Spell_Panel;
using Systems.Scene_Switcher;
using UI.Bar.View.Concrete_Types.Bar_View_With_Additional_Display_Of_Changes;
using UI.Bar.View.Concrete_Types.Filling_Bar;
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
        [SerializeField] private PopupTextSettings _popupTextSettings;

        [SerializeField]
        private BarViewWithAdditionalDisplayOfChangesSettings _barViewWithAdditionalDisplayOfChangesSettings;

        [SerializeField] private FillingBarSettings _fillingBarSettings;

        [Header("Puzzles")] [SerializeField] private PlateSettings _plateSettings;
        [SerializeField] private MovingPlatformsSettings _movingPlatformSettings;
        [SerializeField] private ExtendableObjectsSettings _extendableObjectsSettings;

        [Header("Scenes")] [SerializeField] private ScenesSettings _scenesSettings;

        public override void InstallBindings()
        {
            BindSceneSettings();
            BindGeneralSettings();
            BindEnemiesSettings();
            BindUISettings();
            BindPlayerSettings();
            BindPuzzlesSettings();
        }

        private void BindSceneSettings()
        {
            Container
                .Bind<IScenesSettings>()
                .FromInstance(_scenesSettings)
                .AsSingle();
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
            Container
                .Bind<PopupTextSettings>()
                .FromInstance(_popupTextSettings)
                .AsSingle();
            Container
                .Bind<BarViewWithAdditionalDisplayOfChangesSettings>()
                .FromInstance(_barViewWithAdditionalDisplayOfChangesSettings)
                .AsSingle();
            Container
                .Bind<FillingBarSettings>()
                .FromInstance(_fillingBarSettings)
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