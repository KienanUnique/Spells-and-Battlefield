﻿using System.Collections.Generic;
using Common.Abstract_Bases.Disableable;
using Common.Event_Invoker_For_Action_Animations;
using Enemies.Character;
using Enemies.Look;
using Enemies.Movement;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Enemies.Visual;
using Interfaces;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Factory;
using Settings.Enemies;

namespace Enemies.Setup
{
    public struct EnemyControllerSetupData : IEnemyBaseSetupData
    {
        public EnemyControllerSetupData(IEnemyStateMachineAI stateMachineAI, IPickableItemDataForCreating itemToDrop,
            IEnemyMovement movement, List<IDisableable> itemsNeedDisabling, IIdHolder idHolder,
            GeneralEnemySettings generalEnemySettings, IPickableItemsFactory itemsFactory,
            IEnemyTargetFromTriggersSelector targetFromTriggersSelector, IEnemyLook look,
            IEventInvokerForActionAnimations eventInvokerForAnimations, IEnemyVisual visual,
            IEnemyCharacter character)
        {
            SetStateMachineAI = stateMachineAI;
            SetItemToDrop = itemToDrop;
            SetMovement = movement;
            SetItemsNeedDisabling = itemsNeedDisabling;
            SetIdHolder = idHolder;
            SetGeneralEnemySettings = generalEnemySettings;
            SetPickableItemsFactory = itemsFactory;
            SetTargetFromTriggersSelector = targetFromTriggersSelector;
            SetLook = look;
            SetEventInvokerForAnimations = eventInvokerForAnimations;
            SetVisual = visual;
            SetCharacter = character;
        }

        public IEnemyStateMachineAI SetStateMachineAI { get; }
        public IPickableItemDataForCreating SetItemToDrop { get; }
        public IEnemyMovement SetMovement { get; }
        public List<IDisableable> SetItemsNeedDisabling { get; }
        public IIdHolder SetIdHolder { get; }
        public GeneralEnemySettings SetGeneralEnemySettings { get; }
        public IPickableItemsFactory SetPickableItemsFactory { get; }
        public IEnemyTargetFromTriggersSelector SetTargetFromTriggersSelector { get; }
        public IEnemyLook SetLook { get; }
        public IEventInvokerForActionAnimations SetEventInvokerForAnimations { get; }
        public IEnemyVisual SetVisual { get; }
        public IEnemyCharacter SetCharacter { get; }
    }
}