using System;
using Systems.Scenes_Controller.Game_Level_Loot_Unlocker;
using Systems.Scenes_Controller.Scene_Data.Game_Level_Data;
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