using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using TMPro;
using UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.View.Settings;
using UI.Element.View;
using UI.Element.View.Settings;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Concrete_Scenes.Main_Menu.Start_Game_Window.Game_Level_Selector.Game_Level_Item.View
{
    public class GameLevelItemView : DefaultUIElementView, IGameLevelItemView
    {
        private readonly Image _background;
        private readonly Image _levelIcon;
        private readonly TMP_Text _levelTitle;
        private readonly IGameLevelItemViewSettings _settings;

        public GameLevelItemView(Transform mainTransform, IDefaultUIElementViewSettings defaultUIElementViewSettings,
            Image background, Image levelIcon, TMP_Text levelTitle, IGameLevelItemViewSettings settings) : base(
            mainTransform, defaultUIElementViewSettings)
        {
            _background = background;
            _levelIcon = levelIcon;
            _levelTitle = levelTitle;
            _settings = settings;
        }

        public void Appear(IGameLevelData levelData, bool isSelected)
        {
            _levelIcon.sprite = levelData.Icon;
            _levelTitle.text = levelData.NameToShow;
            _background.color = isSelected ? _settings.SelectedColor : _settings.UnselectedColor;
            base.Appear();
        }

        public void Select()
        {
            _background.color = _settings.SelectedColor;
        }

        public void Unselect()
        {
            _background.color = _settings.UnselectedColor;
        }
    }
}