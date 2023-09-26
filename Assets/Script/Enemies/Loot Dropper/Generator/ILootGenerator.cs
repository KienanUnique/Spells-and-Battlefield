using System.Collections.Generic;
using Pickable_Items.Data_For_Creating;
using Systems.Scene_Switcher.Current_Game_Level_Information;

namespace Enemies.Loot_Dropper.Generator
{
    public interface ILootGenerator
    {
        public IReadOnlyList<IPickableItemDataForCreating> GetLoot(IGameLevelLootUnlocker unlocker);
    }
}