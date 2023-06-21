using System.Collections.Generic;
using Common;
using Common.Abstract_Bases.Disableable;
using Enemies.Movement;
using Enemies.State_Machine;
using Enemies.Target_Selector_From_Triggers;
using Interfaces;
using Pickable_Items.Data_For_Creating;
using Pickable_Items.Factory;
using Settings;
using Settings.Enemy;

namespace Enemies.Setup
{
    public struct EnemyBaseSetupData : IEnemyBaseSetupData
    {
        public EnemyBaseSetupData(IEnemyStateMachineAI enemyStateMachineAI, IPickableItemDataForCreating itemToDrop,
            IEnemyMovement enemyMovement, List<IDisableable> itemsNeedDisabling, IIdHolder idHolder,
            GeneralEnemySettings generalEnemySettings, IPickableItemsFactory itemsFactory,
            IEnemyTargetFromTriggersSelector targetFromTriggersSelector)
        {
            this.SetEnemyStateMachineAI = enemyStateMachineAI;
            this.SetItemToDrop = itemToDrop;
            this.SetEnemyMovement = enemyMovement;
            this.SetItemsNeedDisabling = itemsNeedDisabling;
            this.SetIdHolder = idHolder;
            this.SetGeneralEnemySettings = generalEnemySettings;
            this.SetPickableItemsFactory = itemsFactory;
            this.SetTargetFromTriggersSelector = targetFromTriggersSelector;
        }

        public IEnemyStateMachineAI SetEnemyStateMachineAI { get; }
        public IPickableItemDataForCreating SetItemToDrop { get; }
        public IEnemyMovement SetEnemyMovement { get; }
        public List<IDisableable> SetItemsNeedDisabling { get; }
        public IIdHolder SetIdHolder { get; }
        public GeneralEnemySettings SetGeneralEnemySettings { get; }
        public IPickableItemsFactory SetPickableItemsFactory { get; }
        public IEnemyTargetFromTriggersSelector SetTargetFromTriggersSelector { get; }
    }
}