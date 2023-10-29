using System.Collections.Generic;
using Pickable_Items.Data_For_Creating;
using Systems.Scenes_Controller.Game_Level_Loot_Unlocker;

namespace Enemies.Loot_Dropper.Generator
{
    public interface ILootGenerator
    {
        public IReadOnlyList<IPickableItemDataForCreating> GetLoot(IGameLevelLootUnlocker unlocker);
    }
}