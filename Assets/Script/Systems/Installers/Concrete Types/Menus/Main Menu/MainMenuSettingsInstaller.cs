using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Game_Level_Item.View.Settings;
using UI.Concrete_Scenes.Main_Menu.View.With_Camera_Movement.Settings;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.Menus.Main_Menu
{
    [CreateAssetMenu(fileName = "Main Menu Settings Installer",
        menuName = ScriptableObjectsMenuDirectories.InstallersDirectory + "Main Menu Settings Installer")]
    public class MainMenuSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GameLevelItemViewSettings _gameLevelItemViewSettings;
        [SerializeField] private CameraMovementInMenuSceneSettings _cameraMovementInMenuSceneSettings;

        public override void InstallBindings()
        {
            InstallGameLevelItemViewSettings();
            InstallCameraMovementInMenuSceneSettings();
        }

        private void InstallGameLevelItemViewSettings()
        {
            Container.Bind<IGameLevelItemViewSettings>().FromInstance(_gameLevelItemViewSettings).AsSingle();
        }

        private void InstallCameraMovementInMenuSceneSettings()
        {
            Container.Bind<ICameraMovementInMenuSceneSettings>()
                     .FromInstance(_cameraMovementInMenuSceneSettings)
                     .AsSingle();
        }
    }
}