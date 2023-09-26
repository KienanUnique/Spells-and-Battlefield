using Common.Readonly_Transform;
using Enemies.Loot_Dropper.Generator;
using Pickable_Items.Factory;
using Systems.Scene_Switcher.Current_Game_Level_Information;
using UnityEngine;

namespace Enemies.Loot_Dropper.Provider
{
    [CreateAssetMenu(fileName = "Loot Dropper Provider",
        menuName = ScriptableObjectsMenuDirectories.EnemiesDirectory + "Loot Dropper Provider", order = 0)]
    public class LootDropperProvider : ScriptableObject, ILootDropperProvider
    {
        [SerializeField] private LootGenerator _lootGenerator;

        public ILootDropper GetImplementation(IPickableItemsFactory pickableItemsFactory,
            IReadonlyTransform itemsSpawnPoint, IGameLevelLootUnlocker gameLevelLootUnlocker)
        {
            return new LootDropper(_lootGenerator, pickableItemsFactory, itemsSpawnPoint, gameLevelLootUnlocker);
        }
    }
}