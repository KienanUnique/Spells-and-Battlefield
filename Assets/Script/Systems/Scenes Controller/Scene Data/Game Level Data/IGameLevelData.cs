using System.Collections.Generic;
using Spells.Spell;
using UI.Concrete_Scenes.Comics_Cutscene.Comics_Data;
using UnityEngine;

namespace Systems.Scenes_Controller.Scene_Data.Game_Level_Data
{
    public interface IGameLevelData : ISceneData
    {
        public string NameToShow { get; }
        public Sprite Icon { get; }
        public IComicsData ComicsData { get; }
        public IReadOnlyList<ISpell> StartSpells { get; }
    }
}