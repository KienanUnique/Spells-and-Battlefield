using UnityEngine;

namespace Systems.Scene_Switcher.Scene_Data.Game_Level_Data
{
    [CreateAssetMenu(menuName = ScriptableObjectsMenuDirectories.ScenesDirectory + "Game Level Data",
        fileName = "Game Level Data", order = 0)]
    public class GameLevelData : SceneData, IGameLevelData
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _nameToShow;

        public string NameToShow => _nameToShow;
        public Sprite Icon => _icon;
    }
}