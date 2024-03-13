using System;
using System.Collections.Generic;
using Enemies.Loot_Dropper.Generator.Chance_Item;
using Enemies.Loot_Dropper.Generator.Unlockable_Items_List;
using ModestTree;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Data_For_Creating.Scriptable_Object;
using Systems.Scenes_Controller.Game_Level_Loot_Unlocker;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.Loot_Dropper.Generator
{
    [Serializable]
    public class LootGenerator : ILootGenerator
    {
        [Header("Always dropped loot")]
        [SerializeField]
        private UnlockableItemsList<PickableItemScriptableObjectBase> _alwaysDroppedItems;

        [Header("Loot dropped with certain chance")]
        [SerializeField]
        private ChanceDroppedItemsSection _chanceDroppedItemsSection;

        public IReadOnlyList<IPickableItemDataForCreating> GetLoot(IGameLevelLootUnlocker unlocker)
        {
            var loot = new List<IPickableItemDataForCreating>();
            loot.AddRange(_alwaysDroppedItems.GetUnlockedItems(unlocker));

            var chanceDroppedItems = _chanceDroppedItemsSection.GetUnlockedChanceDroppedItems(unlocker);
            if (chanceDroppedItems.IsEmpty() || _chanceDroppedItemsSection.MaximumInclusiveCount == 0)
            {
                return loot;
            }

            var totalChance = 0f;
            chanceDroppedItems.ForEach(item => totalChance += item.ChanceCoefficient);

            var countOfChanceDroppedItems = Random.Range(_chanceDroppedItemsSection.MinimumInclusiveCount,
                _chanceDroppedItemsSection.MaximumInclusiveCount + 1);
            for (var i = 0; i < countOfChanceDroppedItems; i++)
            {
                var randomValue = Random.Range(0f, totalChance);
                var chanceSum = 0f;

                foreach (var item in chanceDroppedItems)
                {
                    chanceSum += item.ChanceCoefficient;

                    if (randomValue <= chanceSum)
                    {
                        loot.Add(item.Item);
                        break;
                    }
                }
            }

            return loot;
        }

        [Serializable]
        private class ChanceDroppedItemsSection
        {
            [SerializeField] private UnlockableItemsList<LootItemWithChance> _chanceDroppedItems;

            [SerializeField] [Min(0)] private int _minimumInclusiveCount;

            [SerializeField] [Min(0)] private int _maximumInclusiveCount;

            public int MaximumInclusiveCount => _maximumInclusiveCount;
            public int MinimumInclusiveCount => _minimumInclusiveCount;

            public List<LootItemWithChance> GetUnlockedChanceDroppedItems(IGameLevelLootUnlocker unlocker)
            {
                return _chanceDroppedItems.GetUnlockedItems(unlocker);
            }
        }
    }
}