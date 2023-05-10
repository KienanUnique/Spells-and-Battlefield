using General_Settings_in_Scriptable_Objects;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
    public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
    {
        [SerializeField] private GroundLayerMaskSetting _groundLayerMaskSetting;
        [SerializeField] private PickableItemsSettings _pickableItemsSettings;
        [SerializeField] private UIAnimationSettings _uiAnimationSettings;
        [SerializeField] private GeneralEnemySettings _generalEnemySettings;
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private KnightSettings _knightSettings;

        public override void InstallBindings()
        {
            BindGeneralSettings();
            BindEnemiesSettings();
            BindPlayerSettings();
        }

        // private void BindPlayerSettings()
        // {
        //     Container.BindInstance(_playerSettings);
        // }
        //
        // private void BindEnemiesSettings()
        // {
        //     Container.BindInstances(_generalEnemySettings, _knightSettings);
        // }
        //
        // private void BindGeneralSettings()
        // {
        //     Container.BindInstances(_groundLayerMaskSetting, _pickableItemsSettings, _uiAnimationSettings);
        // }

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
                .Bind<UIAnimationSettings>()
                .FromInstance(_uiAnimationSettings)
                .AsSingle();
        }
    }
}