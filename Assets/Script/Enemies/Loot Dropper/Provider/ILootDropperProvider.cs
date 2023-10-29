using Common.Readonly_Transform;
using Pickable_Items.Factory;
using Systems.Scenes_Controller.Game_Level_Loot_Unlocker;

namespace Enemies.Loot_Dropper.Provider
{
    public interface ILootDropperProvider
    {
        public ILootDropper GetImplementation(IPickableItemsFactory pickableItemsFactory,
            IReadonlyTransform itemsSpawnPoint, IGameLevelLootUnlocker gameLevelLootUnlocker);
    }
}