using UnityEngine;

namespace UI.Concrete_Scenes.Main_Menu.Concrete_Windows.Start_Game_Window.Game_Level_Selector.Game_Level_Item.View.Settings
{
    [CreateAssetMenu(fileName = "Game Level Item View Settings",
        menuName = ScriptableObjectsMenuDirectories.ConcreteUISettingsDirectory + "Game Level Item View Settings",
        order = 0)]
    public class GameLevelItemViewSettings : ScriptableObject, IGameLevelItemViewSettings
    {
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _unselectedColor;
        public Color SelectedColor => _selectedColor;
        public Color UnselectedColor => _unselectedColor;
    }
}