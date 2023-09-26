using Common.Readonly_Transform;
using Pickable_Items.Factory;
using Systems.Scene_Switcher.Current_Game_Level_Information;

namespace Enemies.Loot_Dropper.Provider
{
    public interface ILootDropperProvider
    {
        public ILootDropper GetImplementation(IPickableItemsFactory pickableItemsFactory,
            IReadonlyTransform itemsSpawnPoint, IGameLevelLootUnlocker gameLevelLootUnlocker);
    }
}