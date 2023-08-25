using UnityEngine;

namespace Systems.Scene_Switcher.Scene_Data.Game_Level_Data
{
    public interface IGameLevelData : ISceneData
    {
        public string NameToShow { get; }
        public Sprite Icon { get; }
    }
}