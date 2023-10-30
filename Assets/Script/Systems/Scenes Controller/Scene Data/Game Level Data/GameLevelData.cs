using UI.Concrete_Scenes.Comics_Cutscene.Comics_Data;
using UnityEngine;

namespace Systems.Scenes_Controller.Scene_Data.Game_Level_Data
{
    [CreateAssetMenu(menuName = ScriptableObjectsMenuDirectories.ScenesDirectory + "Game Level Data",
        fileName = "Game Level Data", order = 0)]
    public class GameLevelData : SceneData, IGameLevelData
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _nameToShow;
        [SerializeField] private ComicsData _comicsAfterScene;

        public string NameToShow => _nameToShow;
        public Sprite Icon => _icon;
        public IComicsData ComicsData => _comicsAfterScene;
    }
}