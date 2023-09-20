using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.View.Settings;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.Main_Menu
{
    [CreateAssetMenu(fileName = "Main Menu Settings Installer",
        menuName = ScriptableObjectsMenuDirectories.InstallersDirectory + "Main Menu Settings Installer")]
    public class MainMenuSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GameLevelItemViewSettings _gameLevelItemViewSettings;

        public override void InstallBindings()
        {
            InstallUISettings();
        }

        private void InstallUISettings()
        {
            Container.Bind<IGameLevelItemViewSettings>().FromInstance(_gameLevelItemViewSettings).AsSingle();
        }
    }
}