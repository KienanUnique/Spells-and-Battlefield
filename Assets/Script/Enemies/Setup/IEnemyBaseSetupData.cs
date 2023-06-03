using System.Collections.Generic;
using Common;
using Enemies.Movement;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Interfaces;
using Pickable_Items;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Factory;
using Settings;
using Spells.Spell;

namespace Enemies.Setup
{
    public interface IEnemyBaseSetupData
    {
        IEnemyStateMachineAI SetEnemyStateMachineAI { get; }
        IPickableItemDataForCreating SetItemToDrop { get; }
        IEnemyMovement SetEnemyMovement { get; }
        List<IDisableable> SetItemsNeedDisabling { get; }
        IIdHolder SetIdHolder { get; }
        GeneralEnemySettings SetGeneralEnemySettings { get; }
        IPickableItemsFactory SetPickableItemsFactory { get; }
        IEnemyTargetFromTriggersSelector SetTargetFromTriggersSelector { get; }
    }
}