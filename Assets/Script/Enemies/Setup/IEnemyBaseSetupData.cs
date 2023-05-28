using System.Collections.Generic;
using Common;
using Enemies.Movement;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Interfaces;
using Pickable_Items;
using Settings;
using Spells.Spell;

namespace Enemies.Setup
{
    public interface IEnemyBaseSetupData
    {
        IEnemyStateMachineAI SetEnemyStateMachineAI { get; }
        ISpell SetSpellToDrop { get; }
        IEnemyMovement SetEnemyMovement { get; }
        List<IDisableable> SetItemsNeedDisabling { get; }
        IIdHolder SetIdHolder { get; }
        GeneralEnemySettings SetGeneralEnemySettings { get; }
        IPickableSpellsFactory SetSpellsFactory { get; }
        IEnemyTargetFromTriggersSelector SetTargetFromTriggersSelector { get; }
    }
}