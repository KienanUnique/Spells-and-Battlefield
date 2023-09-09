using Common.Readonly_Transform;
using Pickable_Items.Factory;

namespace Enemies.Loot_Dropper.Provider
{
    public interface ILootDropperProvider
    {
        ILootDropper GetImplementation(IPickableItemsFactory pickableItemsFactory,
            IReadonlyTransform itemsSpawnPoint);
    }
}