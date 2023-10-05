using Common.Abstract_Bases;
using UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Presenter;
using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Game_Level_Item.Prefab_Provider
{
    [CreateAssetMenu(fileName = "Game Level Item Prefab Provider",
        menuName = ScriptableObjectsMenuDirectories.UIPrefabProvidersDirectory + "Game Level Item Prefab Provider",
        order = 0)]
    public class GameLevelItemPrefabProvider : PrefabProviderScriptableObjectBase, IGameLevelItemPrefabProvider
    {
        [SerializeField] private GameLevelItemPresenter _prefab;
        public override GameObject Prefab => _prefab.gameObject;
    }
}