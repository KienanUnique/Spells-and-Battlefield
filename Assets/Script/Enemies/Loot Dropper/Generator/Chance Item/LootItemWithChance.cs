using System;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Data_For_Creating.Scriptable_Object;
using UnityEngine;

namespace Enemies.Loot_Dropper.Generator.Chance_Item
{
    [Serializable]
    public class LootItemWithChance : ILootItemWithChance
    {
        [SerializeField] private PickableItemScriptableObjectBase _item;
        [SerializeField] private float _chanceCoefficient;

        public IPickableItemDataForCreating Item => _item;
        public float ChanceCoefficient => _chanceCoefficient;
    }
}