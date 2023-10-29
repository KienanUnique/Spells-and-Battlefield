using System;
using System.Collections.Generic;
using System.Linq;
using Enemies.Loot_Dropper.Generator.Unlockable_Items_List.Unlockable_Item;
using Systems.Scenes_Controller.Game_Level_Loot_Unlocker;
using UnityEngine;

namespace Enemies.Loot_Dropper.Generator.Unlockable_Items_List
{
    [Serializable]
    public class UnlockableItemsList<TItem>
    {
        [SerializeField] private List<UnlockableItem<TItem>> _items;

        public List<TItem> GetUnlockedItems(IGameLevelLootUnlocker unlocker)
        {
            return _items.Where(item => item.IsUnlocked(unlocker))
                         .Select(unlockableItem => unlockableItem.Item)
                         .ToList();
        }
    }
}