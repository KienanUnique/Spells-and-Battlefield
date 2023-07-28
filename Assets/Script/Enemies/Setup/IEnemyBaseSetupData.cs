using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Disableable;
using Enemies.Character;
using Enemies.Look;
using Enemies.Movement;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Visual;
using Enemies.Visual.Event_Invoker_For_Animations;
using Interfaces;
using Pickable_Items;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Factory;
using Settings;
using Settings.Enemy;
using Spells.Spell;

namespace Enemies.Setup
{
    public interface IEnemyBaseSetupData
    {
        IEnemyStateMachineAI SetStateMachineAI { get; }
        IPickableItemDataForCreating SetItemToDrop { get; }
        IEnemyMovement SetMovement { get; }
        List<IDisableable> SetItemsNeedDisabling { get; }
        IIdHolder SetIdHolder { get; }
        GeneralEnemySettings SetGeneralEnemySettings { get; }
        IPickableItemsFactory SetPickableItemsFactory { get; }
        IEnemyTargetFromTriggersSelector SetTargetFromTriggersSelector { get; }
        IEnemyLook SetLook { get; }
        IEnemyEventInvokerForAnimations SetEventInvokerForAnimations { get; }
        public IEnemyVisual SetVisual { get; }
        public IEnemyCharacter SetCharacter { get; }
    }
}