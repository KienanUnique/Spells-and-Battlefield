using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Level_Item.Settings;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.Menus.Main_Menu
{
    [CreateAssetMenu(fileName = "Main Menu Settings Installer",
        menuName = ScriptableObjectsMenuDirectories.InstallersDirectory + "Main Menu Settings Installer")]
    public class MainMenuSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GameLevelItemViewSettings _gameLevelItemViewSettings;

        public override void InstallBindings()
        {
            InstallGameLevelItemViewSettings();
        }

        private void InstallGameLevelItemViewSettings()
        {
            Container.Bind<IGameLevelItemViewSettings>().FromInstance(_gameLevelItemViewSettings).AsSingle();
        }
    }
}