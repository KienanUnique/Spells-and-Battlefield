using Common.Settings.Ground_Layer_Mask;
using Enemies.General_Settings;
using Enemies.Visual.Dissolve_Effect_Controller.Settings;
using Pickable_Items.Settings;
using Player.Settings;
using Puzzles.Mechanisms.Extendable_Object.Settings;
using Puzzles.Mechanisms.Moving_Platforms.Settings;
using Puzzles.Mechanisms_Triggers.Concrete_Types.Plate.Settings;
using Spells.Spell_Types_Settings;
using Systems.In_Game_Systems.Post_Processing_Controller.Settings;
using Systems.In_Game_Systems.Time_Controller.Settings;
using Systems.Input_Manager.Concrete_Types.In_Game.Settings;
using UI.Concrete_Scenes.In_Game.Bar.View.Concrete_Types.Bar_View_With_Additional_Display_Of_Changes.Settings;
using UI.Concrete_Scenes.In_Game.Bar.View.Concrete_Types.Filling_Bar.Settings;
using UI.Concrete_Scenes.In_Game.Damage_Indicator.Settings;
using UI.Concrete_Scenes.In_Game.Popup_Text.Settings;
using UI.Concrete_Scenes.In_Game.Spells_Panel.Settings;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.In_Game
{
    [CreateAssetMenu(fileName = "In Game Settings Installer",
        menuName = ScriptableObjectsMenuDirectories.InstallersDirectory + "In Game Settings Installer")]
    public class InGameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GroundLayerMaskSetting _groundLayerMaskSetting;
        [SerializeField] private PickableItemsSettings _pickableItemsSettings;
        [SerializeField] private SpellTypesSetting _spellTypesSetting;
        [SerializeField] private PlayerSettings _playerSettings;

        [SerializeField] private GeneralEnemySettings _generalEnemySettings;

        [SerializeField] private SpellPanelSettings _spellPanelSettings;
        [SerializeField] private PopupTextSettings _popupTextSettings;
        [SerializeField] private DamageIndicatorElementSettings _damageIndicatorElementSettings;

        [SerializeField]
        private BarViewWithAdditionalDisplayOfChangesSettings _barViewWithAdditionalDisplayOfChangesSettings;

        [SerializeField] private FillingBarSettings _fillingBarSettings;

        [SerializeField] private PlateSettings _plateSettings;
        [SerializeField] private MovingPlatformsSettings _movingPlatformSettings;
        [SerializeField] private ExtendableObjectsSettings _extendableObjectsSettings;

        [SerializeField] private TimeControllerSettings _timeControllerSettings;
        [SerializeField] private PostProcessingControllerSettings _postProcessingControllerSettings;
        [SerializeField] private InputManagerSettings _inputManagerSettings;

        [SerializeField] private DissolveEffectControllerSettings _dissolveEffectControllerSettings;

        public override void InstallBindings()
        {
            BindGeneralSettings();
            BindEnemiesSettings();
            BindUISettings();
            BindPlayerSettings();
            BindPuzzlesSettings();
            BindSystemSettings();
            BindDissolveEffectControllerSettings();
        }

        private void BindDissolveEffectControllerSettings()
        {
            Container.Bind<IDissolveEffectControllerSettings>()
                     .FromInstance(_dissolveEffectControllerSettings)
                     .AsSingle();
        }

        private void BindSystemSettings()
        {
            Container.Bind<ITimeControllerSettings>().FromInstance(_timeControllerSettings).AsSingle();

            Container.Bind<IPostProcessingControllerSettings>()
                     .FromInstance(_postProcessingControllerSettings)
                     .AsSingle();

            Container.Bind<IInputManagerSettings>().FromInstance(_inputManagerSettings).AsSingle();
        }

        private void BindPuzzlesSettings()
        {
            Container.Bind<IPlateSettings>().FromInstance(_plateSettings).AsSingle();

            Container.Bind<IMovingPlatformsSettings>().FromInstance(_movingPlatformSettings).AsSingle();

            Container.Bind<IExtendableObjectsSettings>().FromInstance(_extendableObjectsSettings).AsSingle();
        }

        private void BindPlayerSettings()
        {
            Container.Bind<IPlayerSettings>().FromInstance(_playerSettings).AsSingle();
        }

        private void BindEnemiesSettings()
        {
            Container.Bind<IGeneralEnemySettings>().FromInstance(_generalEnemySettings).AsSingle();
        }

        private void BindUISettings()
        {
            Container.Bind<ISpellPanelSettings>().FromInstance(_spellPanelSettings).AsSingle();
            Container.Bind<IPopupTextSettings>().FromInstance(_popupTextSettings).AsSingle();
            Container.Bind<IBarViewWithAdditionalDisplayOfChangesSettings>()
                     .FromInstance(_barViewWithAdditionalDisplayOfChangesSettings)
                     .AsSingle();
            Container.Bind<IFillingBarSettings>().FromInstance(_fillingBarSettings).AsSingle();
            Container.Bind<IDamageIndicatorElementSettings>().FromInstance(_damageIndicatorElementSettings).AsSingle();
        }

        private void BindGeneralSettings()
        {
            Container.Bind<IGroundLayerMaskSetting>().FromInstance(_groundLayerMaskSetting).AsSingle();

            Container.Bind<IPickableItemsSettings>().FromInstance(_pickableItemsSettings).AsSingle();

            Container.Bind<ISpellTypesSetting>().FromInstance(_spellTypesSetting).AsSingle();
        }
    }
}