using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Factory;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Prefab_Provider;
using UnityEngine;
using Zenject;

namespace Systems.Installers.Concrete_Types.Menus.Main_Menu
{
    public class MainMenuFactoriesInstaller : MonoInstaller
    {
        [SerializeField] private GameLevelItemPrefabProvider _gameLevelItemPrefabProvider;

        public override void InstallBindings()
        {
            InstallUIFactorySystems();
        }

        private void InstallUIFactorySystems()
        {
            var factory = new GameLevelItemFactory(Container, _gameLevelItemPrefabProvider);
            Container.Bind<IGameLevelItemFactory>().FromInstance(factory).AsSingle();
        }
    }
}