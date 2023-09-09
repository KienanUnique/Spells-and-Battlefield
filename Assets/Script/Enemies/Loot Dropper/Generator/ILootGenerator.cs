using System.Collections.Generic;
using Pickable_Items.Data_For_Creating;

namespace Enemies.Loot_Dropper.Generator
{
    public interface ILootGenerator
    {
        public IReadOnlyList<IPickableItemDataForCreating> GetLoot();
    }
}