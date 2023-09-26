using System;
using Systems.Scene_Switcher.Current_Game_Level_Information;
using Systems.Scene_Switcher.Scene_Data.Game_Level_Data;
using UnityEngine;

namespace Enemies.Loot_Dropper.Generator.Unlockable_Items_List.Unlockable_Item
{
    [Serializable]
    public class UnlockableItem<TItem>
    {
        [SerializeField] private TItem _item;
        [SerializeField] private GameLevelData _includingLevelStartDropping;

        public TItem Item => _item;

        public bool IsUnlocked(IGameLevelLootUnlocker unlocker)
        {
            return unlocker.IsUnlockedOnCurrentLevel(_includingLevelStartDropping);
        }
    }
}