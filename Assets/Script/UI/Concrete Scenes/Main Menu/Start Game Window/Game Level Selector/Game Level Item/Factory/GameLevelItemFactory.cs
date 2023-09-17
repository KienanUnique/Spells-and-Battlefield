using System.Collections.Generic;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Prefab_Provider;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Setup;
using UnityEngine;
using Zenject;

namespace UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Factory
{
    public class GameLevelItemFactory : IGameLevelItemFactory
    {
        private readonly IGameLevelItemPrefabProvider _prefabProvider;
        private readonly IInstantiator _instantiator;

        public GameLevelItemFactory(IInstantiator instantiator, IGameLevelItemPrefabProvider prefabProvider)
        {
            _prefabProvider = prefabProvider;
            _instantiator = instantiator;
        }

        public ICollection<IInitializableGameLevelItem> CreateItems(IEnumerable<IGameLevelData> levelData,
            Transform parentTransform)
        {
            var items = new List<IInitializableGameLevelItem>();
            foreach (IGameLevelData currentLevelData in levelData)
            {
                var setup =
                    _instantiator.InstantiatePrefabForComponent<IGameLevelItemPresenterSetup>(_prefabProvider.Prefab,
                        parentTransform);
                setup.SetLevelData(currentLevelData);
                items.Add(setup.ItemPresenter);
            }

            return items;
        }
    }
}