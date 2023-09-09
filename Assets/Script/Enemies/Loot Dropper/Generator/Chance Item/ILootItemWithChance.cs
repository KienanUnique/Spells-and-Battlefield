using Pickable_Items.Data_For_Creating;

namespace Enemies.Loot_Dropper.Generator.Chance_Item
{
    public interface ILootItemWithChance
    {
        public IPickableItemDataForCreating Item { get; }
        public float ChanceCoefficient { get; }
    }
}