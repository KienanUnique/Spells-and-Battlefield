using System;
using System.Collections.Generic;
using Enemies.Loot_Dropper.Generator.Chance_Item;
using ModestTree;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Data_For_Creating.Scriptable_Object;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.Loot_Dropper.Generator
{
    [Serializable]
    public class LootGenerator : ILootGenerator
    {
        [Header("Always dropped loot")]
        [SerializeField]
        private List<PickableItemScriptableObjectBase> _alwaysDroppedItems;

        [Header("Loot dropped with certain chance")]
        [SerializeField]
        private ChanceDroppedItemsSection _chanceDroppedItemsSection;

        public IReadOnlyList<IPickableItemDataForCreating> GetLoot()
        {
            var loot = new List<IPickableItemDataForCreating>();
            loot.AddRange(_alwaysDroppedItems);

            if (_chanceDroppedItemsSection.ChanceDroppedItems.IsEmpty() ||
                _chanceDroppedItemsSection.MaximumInclusiveCount == 0)
            {
                return loot;
            }

            var totalChance = 0f;
            _chanceDroppedItemsSection.ChanceDroppedItems.ForEach(item => totalChance += item.ChanceCoefficient);

            int countOfChanceDroppedItems = Random.Range(_chanceDroppedItemsSection.MinimumInclusiveCount,
                _chanceDroppedItemsSection.MaximumInclusiveCount + 1);
            for (var i = 0; i < countOfChanceDroppedItems; i++)
            {
                float randomValue = Random.Range(0f, totalChance);
                var chanceSum = 0f;

                foreach (LootItemWithChance item in _chanceDroppedItemsSection.ChanceDroppedItems)
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
            [SerializeField] private List<LootItemWithChance> _chanceDroppedItems;

            [SerializeField] [Min(0)] private int _minimumInclusiveCount;

            [SerializeField] [Min(0)] private int _maximumInclusiveCount;

            public int MaximumInclusiveCount => _maximumInclusiveCount;
            public int MinimumInclusiveCount => _minimumInclusiveCount;
            public List<LootItemWithChance> ChanceDroppedItems => _chanceDroppedItems;
        }
    }
}